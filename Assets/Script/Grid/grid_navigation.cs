using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using System;
using System.Collections.Generic;
// using System.Diagnostics;
public class grid_navigation : MonoBehaviour
{
    Queue<PathResult> results = new Queue<PathResult>();
    // job calculate path
    // grid > native
    // native > path
    public static grid_navigation Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Update()
    {
        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                PathResult result = results.Dequeue();
                result.callback(result.path, result.success);
            }
        }
    }
    // public static void PathRequest(PathData request)
    // {
    //     // Instance.PathCalculate(request, Instance.FinishedProcessing);
    //     Instance.PathCalculate(request);
    // }
    // public void FinishedProcessing(PathResult result)
    // {
    //     results.Enqueue(result);
    // }
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    // public void PathCalculate(Vector3 source, Vector3 target)
    // private void PathCalculate(PathData request, Action<PathResult> callback)
    public void PathCalculate(PathData request)
    {
        // Stopwatch sw = new Stopwatch();
        // sw.Start();
        NativeList<int2> path = new NativeList<int2>(1, Allocator.TempJob);
        NativeArray<PathNode> pathNodeArray = GridToNative(new int2((int)request.target.x, (int)request.target.y));
        PathCalculateJob pathCalculateJob = new PathCalculateJob
        {
            startPosition = new int2((int)request.source.x, (int)request.source.y),
            endPosition = new int2((int)request.target.x, (int)request.target.y),
            path = path,
            pathNodeArray = pathNodeArray,
            size = new int2(grid_map.Instance.SizeX, grid_map.Instance.SizeY),
        };
        JobHandle jobHandle = pathCalculateJob.Schedule();
        jobHandle.Complete();
        // // * testing
        // foreach (int2 position in pathCalculateJob.path)
        // {
        //     print(position);
        // }
        Vector3[] waypoints = NativeToVector3(pathCalculateJob.path);
        path.Dispose();
        pathNodeArray.Dispose();
        // callback(new PathResult(waypoints, waypoints.Length > 0, request.callback));
        results.Enqueue(new PathResult(waypoints, waypoints.Length > 0, request.callback));
        // sw.Stop();
        // if (waypoints.Length > 0)
        //     print("Path found: " + sw.ElapsedMilliseconds + " ms");
    }
    private NativeArray<PathNode> GridToNative(int2 endPosition)
    {
        GridData[,] grid = grid_map.Instance.Grid;
        NativeArray<PathNode> pathNodeArray = new NativeArray<PathNode>(grid.GetLength(0) * grid.GetLength(1), Allocator.TempJob);
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                PathNode pathNode = new PathNode();
                pathNode.x = x;
                pathNode.y = y;
                pathNode.index = CalculateIndex(x, y, grid.GetLength(0));
                pathNode.costG = int.MaxValue;
                pathNode.costH = CalculateDistanceCost(new int2(x, y), endPosition);
                pathNode.CalculateCostF();
                pathNode.isWalkable = grid[x, y].IsWalkable;
                pathNode.weight = grid[x, y].Weight;
                pathNode.indexPrev = -1;
                pathNodeArray[pathNode.index] = pathNode;
            }
        }
        return pathNodeArray;
    }
    private Vector3[] NativeToVector3(NativeArray<int2> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        foreach (int2 waypoint in path)
        {
            waypoints.Add(grid_map.Instance.IndexToWorld(waypoint.x, waypoint.y));
        }
        return PathSimplify(waypoints);
    }
    private Vector3[] PathSimplify(List<Vector3> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].x - path[i].x, path[i - 1].y - path[i].y);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i - 1]);
            }
            directionOld = directionNew;
        }
        Vector3[] temp = waypoints.ToArray();
        Array.Reverse(temp);
        return temp;
    }
    private int CalculateIndex(int x, int y, int gridWidth)
    {
        return x + y * gridWidth;
    }
    private int CalculateDistanceCost(int2 aPosition, int2 bPosition)
    {
        int xDistance = math.abs(aPosition.x - bPosition.x);
        int yDistance = math.abs(aPosition.y - bPosition.y);
        int remaining = math.abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * math.min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }
    [BurstCompile]
    private struct PathCalculateJob : IJob
    {
        public int2 startPosition;
        public int2 endPosition;
        public NativeList<int2> path;
        public NativeArray<PathNode> pathNodeArray;
        public int2 size;
        public void Execute()
        {
            // // * testing
            // int2 gridSize = new int2(134, 98);
            // NativeArray<PathNode> pathNodeArray = new NativeArray<PathNode>(gridSize.x * gridSize.y, Allocator.Temp);
            // for (int x = 0; x < gridSize.x; x++)
            // {
            //     for (int y = 0; y < gridSize.y; y++)
            //     {
            //         PathNode pathNode = new PathNode();
            //         pathNode.x = x;
            //         pathNode.y = y;
            //         pathNode.index = CalculateIndex(x, y, gridSize.x);
            //         pathNode.costG = int.MaxValue;
            //         pathNode.costH = CalculateDistanceCost(new int2(x, y), endPosition);
            //         pathNode.CalculateCostF();
            //         pathNode.isWalkable = true;
            //         // pathNode.weight = ?;
            //         pathNode.indexPrev = -1;
            //         pathNodeArray[pathNode.index] = pathNode;
            //     }
            // }
            // // * testing
            // PathNode walkablePathNode = pathNodeArray[CalculateIndex(1, 0, gridSize.x)];
            // walkablePathNode.SetIsWalkable(false);
            // pathNodeArray[CalculateIndex(1, 0, gridSize.x)] = walkablePathNode;
            // walkablePathNode = pathNodeArray[CalculateIndex(1, 1, gridSize.x)];
            // walkablePathNode.SetIsWalkable(false);
            // pathNodeArray[CalculateIndex(1, 1, gridSize.x)] = walkablePathNode;
            // walkablePathNode = pathNodeArray[CalculateIndex(1, 2, gridSize.x)];
            // walkablePathNode.SetIsWalkable(false);
            // pathNodeArray[CalculateIndex(1, 2, gridSize.x)] = walkablePathNode;
            // // burst incompatible
            // NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(new int2[] {
            //     new int2(-1, 0),    // L
            //     new int2(+1, 0),    // R
            //     new int2(0, -1),    // D
            //     new int2(0, +1),    // U
            //     new int2(-1, -1),   // LD
            //     new int2(-1, +1),   // LU
            //     new int2(+1, -1),   // RD
            //     new int2(+1, +1),   // DU
            // }, Allocator.Temp);
            // burst compatible
            NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(8, Allocator.Temp);
            neighbourOffsetArray[0] = new int2(-1, 0);  // L
            neighbourOffsetArray[1] = new int2(+1, 0);  // R
            neighbourOffsetArray[2] = new int2(0, -1);  // D
            neighbourOffsetArray[3] = new int2(0, +1);  // U
            neighbourOffsetArray[4] = new int2(-1, -1); // LD
            neighbourOffsetArray[5] = new int2(-1, +1); // LU
            neighbourOffsetArray[6] = new int2(+1, -1); // RD
            neighbourOffsetArray[7] = new int2(+1, +1); // DU
            int endNodeIndex = CalculateIndex(endPosition.x, endPosition.y, size.x);
            PathNode startNode = pathNodeArray[CalculateIndex(startPosition.x, startPosition.y, size.x)];
            startNode.costG = 0;
            startNode.CalculateCostF();
            pathNodeArray[startNode.index] = startNode;
            NativeList<int> listOpen = new NativeList<int>(Allocator.Temp);
            NativeList<int> listClosed = new NativeList<int>(Allocator.Temp);
            listOpen.Add(startNode.index);
            while (listOpen.Length > 0)
            {
                int currentNodeIndex = GetLowestCostFNodeIndex(listOpen, pathNodeArray);
                PathNode currentNode = pathNodeArray[currentNodeIndex];
                if (currentNodeIndex == endNodeIndex)
                {
                    // reached end
                    break;
                }
                // remove current node from open list
                for (int i = 0; i < listOpen.Length; i++)
                {
                    if (listOpen[i] == currentNodeIndex)
                    {
                        listOpen.RemoveAtSwapBack(i);
                        break;
                    }
                }
                listClosed.Add(currentNodeIndex);
                for (int i = 0; i < neighbourOffsetArray.Length; i++)
                {
                    int2 neighbourOffset = neighbourOffsetArray[i];
                    int2 neighbourPosition = new int2(currentNode.x + neighbourOffset.x, currentNode.y + neighbourOffset.y);
                    if (!IsPositionInsideGrid(neighbourPosition, size))
                    {
                        // neighbour not valid position
                        continue;
                    }
                    int neighbourNodeIndex = CalculateIndex(neighbourPosition.x, neighbourPosition.y, size.x);
                    if (listClosed.Contains(neighbourNodeIndex))
                    {
                        // already searched this node
                        continue;
                    }
                    PathNode neighbourNode = pathNodeArray[neighbourNodeIndex];
                    if (!neighbourNode.isWalkable)
                    {
                        // not walkable
                        continue;
                    }
                    // diagonal neighbour
                    if (Mathf.Abs(neighbourOffset[0]) + Mathf.Abs(neighbourOffset[1]) > 1)
                    {
                        // corners blocked
                        if (!(pathNodeArray[CalculateIndex(neighbourPosition.x, currentNode.y, size.x)].isWalkable || pathNodeArray[CalculateIndex(currentNode.x, neighbourPosition.y, size.x)].isWalkable))
                            continue;
                    }
                    int2 currentNodePosition = new int2(currentNode.x, currentNode.y);
                    int tentativeCostG = currentNode.costG + CalculateDistanceCost(currentNodePosition, neighbourPosition) + neighbourNode.weight;
                    // if (tentativeCostG < neighbourNode.costG)
                    if (tentativeCostG < neighbourNode.costG || !listOpen.Contains(neighbourNodeIndex))
                    {
                        neighbourNode.indexPrev = currentNodeIndex;
                        neighbourNode.costG = tentativeCostG;
                        neighbourNode.CalculateCostF();
                        pathNodeArray[neighbourNodeIndex] = neighbourNode;
                        if (!listOpen.Contains(neighbourNode.index))
                        {
                            listOpen.Add(neighbourNode.index);
                        }
                    }
                }
            }
            PathNode endNode = pathNodeArray[endNodeIndex];
            if (endNode.indexPrev == -1)
            {
                // path NOT found
                // print("het");
            }
            else
            {
                // path found
                // path = PathCalculate(pathNodeArray, endNode);
                PathCalculate(pathNodeArray, endNode);
                // NativeList<int2> path = PathCalculate(pathNodeArray, endNode);
                // // * testing
                // foreach (int2 pathPosition in path)
                // {
                //     Debug.Log(pathPosition);
                // }
                // path.Dispose();
            }
            neighbourOffsetArray.Dispose();
            listOpen.Dispose();
            listClosed.Dispose();
        }
        private int CalculateIndex(int x, int y, int gridWidth)
        {
            return x + y * gridWidth;
        }
        private int CalculateDistanceCost(int2 aPosition, int2 bPosition)
        {
            int xDistance = math.abs(aPosition.x - bPosition.x);
            int yDistance = math.abs(aPosition.y - bPosition.y);
            int remaining = math.abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * math.min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }
        // private NativeList<int2> PathCalculate(NativeArray<PathNode> pathNodeArray, PathNode endNode)
        private void PathCalculate(NativeArray<PathNode> pathNodeArray, PathNode endNode)
        {
            if (endNode.indexPrev == -1)
            {
                // path NOT found
                // return new NativeList<int2>(Allocator.Temp);
                return;
            }
            else
            {
                // path found
                // NativeList<int2> path = new NativeList<int2>(Allocator.Temp);
                path.Add(new int2(endNode.x, endNode.y));
                PathNode currentNode = endNode;
                while (currentNode.indexPrev != -1)
                {
                    PathNode nodePrev = pathNodeArray[currentNode.indexPrev];
                    path.Add(new int2(nodePrev.x, nodePrev.y));
                    currentNode = nodePrev;
                }
                // return path;
            }
        }
        private bool IsPositionInsideGrid(int2 gridPosition, int2 gridSize)
        {
            return
                gridPosition.x >= 0 &&
                gridPosition.y >= 0 &&
                gridPosition.x < gridSize.x &&
                gridPosition.y < gridSize.y;
        }
        private int GetLowestCostFNodeIndex(NativeList<int> listOpen, NativeArray<PathNode> pathNodeArray)
        {
            PathNode lowestCostPathNode = pathNodeArray[listOpen[0]];
            for (int i = 1; i < listOpen.Length; i++)
            {
                PathNode testPathNode = pathNodeArray[listOpen[i]];
                if (testPathNode.costF < lowestCostPathNode.costF)
                {
                    lowestCostPathNode = testPathNode;
                }
            }
            return lowestCostPathNode.index;
        }
    }
    // value type
    public struct PathNode
    {
        public int x;
        public int y;
        public int index;
        public int costG;
        public int costH;
        public int costF;
        public bool isWalkable;
        public int weight;
        public int indexPrev;
        public void CalculateCostF()
        {
            costF = costG + costH;
        }
        // public void SetIsWalkable(bool value)
        // {
        //     isWalkable = value;
        // }
    }
}
public struct PathData
{
    public Vector2Int source;
    public Vector2Int target;
    public Action<Vector3[], bool> callback;
    public PathData(Vector3 source, Vector3 target, Action<Vector3[], bool> callback)
    {
        this.source = grid_map.Instance.WorldToIndex(source);
        this.target = grid_map.Instance.WorldToIndex(target);
        this.callback = callback;
    }
}
public struct PathResult
{
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;
    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }
}
