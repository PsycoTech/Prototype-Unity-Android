// using UnityEngine;
// using Unity.Mathematics;
// using Unity.Collections;
// using Unity.Jobs;
// using Unity.Burst;

// public class PathFinding : MonoBehaviour
// {
//     private const int MOVE_STRAIGHT_COST = 10;
//     private const int MOVE_DIAGONAL_COST = 14;
//     private float time = 1f;
//     private float timer = 0f;
//     private void Update()
//     {
//         if (timer < time)
//             timer += Time.deltaTime;
//         else
//         {
//             timer = 0f;
//             float startTime = Time.realtimeSinceStartup;
//             int pathFindJobCount = 76;
//             NativeArray<JobHandle> jobHandleArray = new NativeArray<JobHandle>(pathFindJobCount, Allocator.TempJob);
//             for (int i = 0; i < pathFindJobCount; i++)
//             {
//                 PathFindJob pathFindJob = new PathFindJob
//                 {
//                     startPosition = new int2(0, 0),
//                     endPosition = new int2(133, 97),
//                 };
//                 JobHandle jobHandle = pathFindJob.Schedule();
//                 jobHandleArray[i] = jobHandle;
//             }
//             JobHandle.CompleteAll(jobHandleArray);
//             jobHandleArray.Dispose();
//             print(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
//         }
//     }
//     [BurstCompile]
//     private struct PathFindJob : IJob
//     {
//         public int2 startPosition;
//         public int2 endPosition;
//         public void Execute()
//         {
//             // * testing
//             int2 gridSize = new int2(134, 98);
//             NativeArray<PathNode> pathNodeArray = new NativeArray<PathNode>(gridSize.x * gridSize.y, Allocator.Temp);
//             for (int x = 0; x < gridSize.x; x++)
//             {
//                 for (int y = 0; y < gridSize.y; y++)
//                 {
//                     PathNode pathNode = new PathNode();
//                     pathNode.x = x;
//                     pathNode.y = y;
//                     pathNode.index = CalculateIndex(x, y, gridSize.x);
//                     pathNode.costG = int.MaxValue;
//                     pathNode.costH = CalculateDistanceCost(new int2(x, y), endPosition);
//                     pathNode.CalculateCostF();
//                     pathNode.isWalkable = true;
//                     pathNode.indexPrev = -1;
//                     pathNodeArray[pathNode.index] = pathNode;
//                 }
//             }
//             // // * testing
//             // PathNode walkablePathNode = pathNodeArray[CalculateIndex(1, 0, gridSize.x)];
//             // walkablePathNode.SetIsWalkable(false);
//             // pathNodeArray[CalculateIndex(1, 0, gridSize.x)] = walkablePathNode;
//             // walkablePathNode = pathNodeArray[CalculateIndex(1, 1, gridSize.x)];
//             // walkablePathNode.SetIsWalkable(false);
//             // pathNodeArray[CalculateIndex(1, 1, gridSize.x)] = walkablePathNode;
//             // walkablePathNode = pathNodeArray[CalculateIndex(1, 2, gridSize.x)];
//             // walkablePathNode.SetIsWalkable(false);
//             // pathNodeArray[CalculateIndex(1, 2, gridSize.x)] = walkablePathNode;
//             // // burst incompatible
//             // NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(new int2[] {
//             //     new int2(-1, 0),    // L
//             //     new int2(+1, 0),    // R
//             //     new int2(0, -1),    // D
//             //     new int2(0, +1),    // U
//             //     new int2(-1, -1),   // LD
//             //     new int2(-1, +1),   // LU
//             //     new int2(+1, -1),   // RD
//             //     new int2(+1, +1),   // DU
//             // }, Allocator.Temp);
//             // burst compatible
//             NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(8, Allocator.Temp);
//             neighbourOffsetArray[0] = new int2(-1, 0);  // L
//             neighbourOffsetArray[1] = new int2(+1, 0);  // R
//             neighbourOffsetArray[2] = new int2(0, -1);  // D
//             neighbourOffsetArray[3] = new int2(0, +1);  // U
//             neighbourOffsetArray[4] = new int2(-1, -1); // LD
//             neighbourOffsetArray[5] = new int2(-1, +1); // LU
//             neighbourOffsetArray[6] = new int2(+1, -1); // RD
//             neighbourOffsetArray[7] = new int2(+1, +1); // DU
//             int endNodeIndex = CalculateIndex(endPosition.x, endPosition.y, gridSize.x);
//             PathNode startNode = pathNodeArray[CalculateIndex(startPosition.x, startPosition.y, gridSize.x)];
//             startNode.costG = 0;
//             startNode.CalculateCostF();
//             pathNodeArray[startNode.index] = startNode;
//             NativeList<int> listOpen = new NativeList<int>(Allocator.Temp);
//             NativeList<int> listClosed = new NativeList<int>(Allocator.Temp);
//             listOpen.Add(startNode.index);
//             while (listOpen.Length > 0)
//             {
//                 int currentNodeIndex = GetLowestCostFNodeIndex(listOpen, pathNodeArray);
//                 PathNode currentNode = pathNodeArray[currentNodeIndex];
//                 if (currentNodeIndex == endNodeIndex)
//                 {
//                     // reached end
//                     break;
//                 }
//                 // remove current node from open list
//                 for (int i = 0; i < listOpen.Length; i++)
//                 {
//                     if (listOpen[i] == currentNodeIndex)
//                     {
//                         listOpen.RemoveAtSwapBack(i);
//                         break;
//                     }
//                 }
//                 listClosed.Add(currentNodeIndex);
//                 for (int i = 0; i < neighbourOffsetArray.Length; i++)
//                 {
//                     int2 neighbourOffset = neighbourOffsetArray[i];
//                     int2 neighbourPosition = new int2(currentNode.x + neighbourOffset.x, currentNode.y + neighbourOffset.y);
//                     if (!IsPositionInsideGrid(neighbourPosition, gridSize))
//                     {
//                         // neighbour not valid position
//                         continue;
//                     }
//                     int neighbourNodeIndex = CalculateIndex(neighbourPosition.x, neighbourPosition.y, gridSize.x);
//                     if (listClosed.Contains(neighbourNodeIndex))
//                     {
//                         // already searched this node
//                         continue;
//                     }
//                     PathNode neighbourNode = pathNodeArray[neighbourNodeIndex];
//                     if (!neighbourNode.isWalkable)
//                     {
//                         // not walkable
//                         continue;
//                     }
//                     int2 currentNodePosition = new int2(currentNode.x, currentNode.y);
//                     int tentativeCostG = currentNode.costG + CalculateDistanceCost(currentNodePosition, neighbourPosition);
//                     if (tentativeCostG < neighbourNode.costG)
//                     {
//                         neighbourNode.indexPrev = currentNodeIndex;
//                         neighbourNode.costG = tentativeCostG;
//                         neighbourNode.CalculateCostF();
//                         pathNodeArray[neighbourNodeIndex] = neighbourNode;
//                         if (!listOpen.Contains(neighbourNode.index))
//                         {
//                             listOpen.Add(neighbourNode.index);
//                         }
//                     }
//                 }
//             }
//             PathNode endNode = pathNodeArray[endNodeIndex];
//             if (endNode.indexPrev == -1)
//             {
//                 // path NOT found
//                 // print("het");
//             }
//             else
//             {
//                 // path found
//                 NativeList<int2> path = CalculatePath(pathNodeArray, endNode);
//                 // // * testing
//                 // foreach (int2 pathPosition in path)
//                 // {
//                 //     print(pathPosition);
//                 // }
//                 path.Dispose();
//             }
//             pathNodeArray.Dispose();
//             neighbourOffsetArray.Dispose();
//             listOpen.Dispose();
//             listClosed.Dispose();
//         }
//         private NativeList<int2> CalculatePath(NativeArray<PathNode> pathNodeArray, PathNode endNode)
//         {
//             if (endNode.indexPrev == -1)
//             {
//                 // path NOT found
//                 return new NativeList<int2>(Allocator.Temp);
//             }
//             else
//             {
//                 // path found
//                 NativeList<int2> path = new NativeList<int2>(Allocator.Temp);
//                 path.Add(new int2(endNode.x, endNode.y));
//                 PathNode currentNode = endNode;
//                 while (currentNode.indexPrev != -1)
//                 {
//                     PathNode nodePrev = pathNodeArray[currentNode.indexPrev];
//                     path.Add(new int2(nodePrev.x, nodePrev.y));
//                     currentNode = nodePrev;
//                 }
//                 return path;
//             }
//         }
//         private bool IsPositionInsideGrid(int2 gridPosition, int2 gridSize)
//         {
//             return
//                 gridPosition.x >= 0 &&
//                 gridPosition.y >= 0 &&
//                 gridPosition.x < gridSize.x &&
//                 gridPosition.y < gridSize.y;
//         }
//         private int CalculateIndex(int x, int y, int gridWidth)
//         {
//             return x + y * gridWidth;
//         }
//         private int CalculateDistanceCost(int2 aPosition, int2 bPosition)
//         {
//             int xDistance = math.abs(aPosition.x - bPosition.x);
//             int yDistance = math.abs(aPosition.y - bPosition.y);
//             int remaining = math.abs(xDistance - yDistance);
//             return MOVE_DIAGONAL_COST * math.min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
//         }
//         private int GetLowestCostFNodeIndex(NativeList<int> listOpen, NativeArray<PathNode> pathNodeArray)
//         {
//             PathNode lowestCostPathNode = pathNodeArray[listOpen[0]];
//             for (int i = 1; i < listOpen.Length; i++)
//             {
//                 PathNode testPathNode = pathNodeArray[listOpen[i]];
//                 if (testPathNode.costF < lowestCostPathNode.costF)
//                 {
//                     lowestCostPathNode = testPathNode;
//                 }
//             }
//             return lowestCostPathNode.index;
//         }
//         // value type
//         public struct PathNode
//         {
//             public int x;
//             public int y;
//             public int index;
//             public int costG;
//             public int costH;
//             public int costF;
//             public bool isWalkable;
//             public int indexPrev;
//             public void CalculateCostF()
//             {
//                 costF = costG + costH;
//             }
//             public void SetIsWalkable(bool value)
//             {
//                 isWalkable = value;
//             }
//         }
//     }
// }
