// using UnityEngine;
// using Unity.Mathematics;
// using UnityEngine.Jobs;
// using Unity.Jobs;
// using Unity.Collections;
// using Unity.Burst;
// using System.Collections.Generic;
// public class TestingParallel : MonoBehaviour
// {
//     [SerializeField] bool useJobs;
//     [SerializeField] private Transform pfMob;
//     private List<Mob> mobList;
//     public class Mob
//     {
//         public Transform transform;
//         public float moveY;
//     }
//     private void Start() {
//         mobList = new List<Mob>();
//         for (int i = 0; i < 1000; i++)
//         {
//             Transform mobTransform = Instantiate(pfMob, new Vector3(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-5f, 5f)), Quaternion.identity);
//             mobList.Add(new Mob{
//                 transform = mobTransform,
//                 moveY = UnityEngine.Random.Range(1f, 2f)
//             });
//         }
//     }
//     private void Update()
//     {
//         float startTime = Time.realtimeSinceStartup;
//         // - SERIAL
//         // if (useJobs)
//         // {
//         //     // simulating ten units
//         //     NativeList<JobHandle> jobHandleList = new NativeList<JobHandle>(Allocator.Temp);
//         //     for (int i = 0; i < 10; i++)
//         //     {
//         //         JobHandle jobHandle = ReallyToughTaskJob();
//         //         // // pauses main thread till job complete ? continue
//         //         // jobHandle.Complete();
//         //         jobHandleList.Add(jobHandle);
//         //     }
//         //     JobHandle.CompleteAll(jobHandleList);
//         //     jobHandleList.Dispose();
//         // }
//         // else
//         // {
//         //     ReallyToughTask();
//         // }
//         // - PARALLEL
//         if (useJobs)
//         {
//             // data [? wasted memory]
//             // - default
//             NativeArray<float3> positionArray = new NativeArray<float3>(mobList.Count, Allocator.TempJob);
//             // - transform
//             TransformAccessArray transformAccessArray = new TransformAccessArray(mobList.Count);
//             NativeArray<float> moveYArray = new NativeArray<float>(mobList.Count, Allocator.TempJob);
//             // initialize
//             for (int i = 0; i < mobList.Count; i++)
//             {
//                 // - default
//                 // positionArray[i] = mobList[i].transform.position;
//                 // - transform
//                 transformAccessArray.Add(mobList[i].transform);
//                 moveYArray[i] = mobList[i].moveY;
//             }
//             // instance
//             // - default
//             // ReallyToughParallelJob reallyToughParallelJob = new ReallyToughParallelJob
//             // {
//             //     positionArray = positionArray,
//             //     moveYArray = moveYArray,
//             //     deltaTime = Time.deltaTime,
//             // };
//             // - transform
//             ReallyToughParallelJobTransform reallyToughParallelJobTransform = new ReallyToughParallelJobTransform
//             {
//                 moveYArray = moveYArray,
//                 deltaTime = Time.deltaTime,
//             };
//             // execute
//             // - default
//             // // number of indices per job
//             // JobHandle jobHandle = reallyToughParallelJob.Schedule(mobList.Count, 100);
//             // - transform
//             JobHandle jobHandle = reallyToughParallelJobTransform.Schedule(transformAccessArray);
//             jobHandle.Complete();
//             // update
//             for (int i = 0; i < mobList.Count; i++)
//             {
//                 // - default
//                 // mobList[i].transform.position = positionArray[i];
//                 mobList[i].moveY = moveYArray[i];
//             }
//             // dispose
//             // - default
//             // positionArray.Dispose();
//             // - transform
//             transformAccessArray.Dispose();
//             moveYArray.Dispose();
//         }
//         else
//         {
//             foreach (Mob mob in mobList)
//             {
//                 mob.transform.position += new Vector3(0, mob.moveY * Time.deltaTime);
//                 if (mob.transform.position.y > 5f)
//                 {
//                     mob.moveY = -math.abs(mob.moveY);
//                 }
//                 if (mob.transform.position.y < -5f)
//                 {
//                     mob.moveY = +math.abs(mob.moveY);
//                 }
//                 float value = 0f;
//                 for (int i = 0; i < 50000; i++)
//                 {
//                     value = math.exp10(math.sqrt(value));
//                 }
//             }
//         }
//         print(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
//     }
//     private void ReallyToughTask()
//     {
//         float value = 0f;
//         for (int i = 0; i < 50000; i++)
//         {
//             value = math.exp10(math.sqrt(value));
//         }
//     }
//     private JobHandle ReallyToughTaskJob()
//     {
//         ReallyToughSerialJob job = new ReallyToughSerialJob();
//         return job.Schedule();
//     }
// }
// [BurstCompile]
// // class : reference type | struct : value type (copy)
// public struct ReallyToughSerialJob : IJob
// {
//     public void Execute()
//     {
//         float value = 0f;
//         for (int i = 0; i < 50000; i++)
//         {
//             value = math.exp10(math.sqrt(value));
//         }
//     }
// }
// [BurstCompile]
// public struct ReallyToughParallelJob : IJobParallelFor
// {
//     // data being modified
//     public NativeArray<float3> positionArray;
//     public NativeArray<float> moveYArray;
//     // cant access main thread stuff
//     [ReadOnly] public float deltaTime;
//     public void Execute(int index)
//     {
//         positionArray[index] += new float3(0, moveYArray[index] * deltaTime, 0f);
//         if (positionArray[index].y > 5f)
//         {
//             moveYArray[index] = -math.abs(moveYArray[index]);
//         }
//         if (positionArray[index].y < -5f)
//         {
//             moveYArray[index] = +math.abs(moveYArray[index]);
//         }
//         float value = 0f;
//         for (int i = 0; i < 50000; i++)
//         {
//             value = math.exp10(math.sqrt(value));
//         }
//     }
// }
// [BurstCompile]
// public struct ReallyToughParallelJobTransform : IJobParallelForTransform
// {
//     // data being modified
//     public NativeArray<float> moveYArray;
//     // cant access main thread stuff
//     [ReadOnly] public float deltaTime;
//     public void Execute(int index, TransformAccess transform)
//     {
//         transform.position += new Vector3(0, moveYArray[index] * deltaTime, 0f);
//         if (transform.position.y > 5f)
//         {
//             moveYArray[index] = -math.abs(moveYArray[index]);
//         }
//         if (transform.position.y < -5f)
//         {
//             moveYArray[index] = +math.abs(moveYArray[index]);
//         }
//         float value = 0f;
//         for (int i = 0; i < 50000; i++)
//         {
//             value = math.exp10(math.sqrt(value));
//         }
//     }
// }
