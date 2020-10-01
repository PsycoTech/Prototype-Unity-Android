// * source: sebastian lague
using UnityEngine;
using System.Collections.Generic;
// using System;
// using System.Threading;
public class map_navigation : MonoBehaviour
{
    Queue<PathResult> results = new Queue<PathResult>();
    static map_navigation Instance;
    map_navigation _navigation;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _navigation = GetComponent<map_navigation>();
    }
    void Update()
    {
        if (results.Count > 0)
        {
            int itemsInQueue = results.Count;
            lock (results)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    PathResult result = results.Dequeue();
                    result.callback(result.path, result.success);
                }
            }
        }
    }
    public static void PathRequest(PathData request)
    {
        // ThreadStart threadStart = delegate {
        //     Instance._navigation.PathCalculate(request, Instance.FinishedProcessing);
        // };
        // threadStart.Invoke();
    }
    public void FinishedProcessing(PathResult result)
    {
        lock (results)
        {
            results.Enqueue(result);
        }
    }
}
// public struct PathResult
// {
//     public Vector3[] path;
//     public bool success;
//     public Action<Vector3[], bool> callback;
//     public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
//     {
//         this.path = path;
//         this.success = success;
//         this.callback = callback;
//     }
// }
// public struct PathData
// {
//     public Vector3 source;
//     public Vector3 target;
//     public Action<Vector3[], bool> callback;
//     public PathData(Vector3 source, Vector3 target, Action<Vector3[], bool> callback)
//     {
//         this.source = source;
//         this.target = target;
//         this.callback = callback;
//     }
// }
