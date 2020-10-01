using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;
public class Testing : MonoBehaviour
{
    private void Update()
    {
        float startTime = Time.realtimeSinceStartup;
        // ReallyToughTask();
        // simulating ten units
        NativeList<JobHandle> jobHandleList = new NativeList<JobHandle>(Allocator.Temp);
        for (int i = 0; i < 10; i++)
        {
            JobHandle jobHandle = ReallyToughTaskJob();
            // // pauses main thread till job complete ? continue
            // jobHandle.Complete();
            jobHandleList.Add(jobHandle);
        }
        JobHandle.CompleteAll(jobHandleList);
        jobHandleList.Dispose();
        print(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
    }
    // private void ReallyToughTask()
    // {
    //     float value = 0f;
    //     for (int i = 0; i < 50000; i++)
    //     {
    //         value = math.exp10(math.sqrt(value));
    //     }
    // }
    private JobHandle ReallyToughTaskJob()
    {
        ReallyToughJob job = new ReallyToughJob();
        return job.Schedule();
    }
}
[BurstCompile]
// class : reference type | struct : value type (copy)
public struct ReallyToughJob : IJob
{
    public void Execute()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
}
