// using UnityEngine;
// using Unity.Collections;
// using System.Collections.Generic;
// using Unity.Jobs;
// public class BuildingManager : MonoBehaviour
// {
//     [SerializeField] private List<Building> buildings;
//     private NativeArray<Building.Data> _buildingDataArray;
//     private BuildingUpdateJob _job;
//     private void Awake()
//     {
//         _buildingDataArray = new NativeArray<Building.Data>(buildings.Count, Allocator.TempJob);
//         for (int i = 0; i < buildings.Count; i++)
//         {
//             _buildingDataArray[i] = new Building.Data(buildings[i]);
//         }
//         _job = new BuildingUpdateJob
//         {
//             BuildingDataArray = _buildingDataArray
//         };
//     }
//     private void Update()
//     {
//         // length
//         // batch size
//         var jobHandle = _job.Schedule(buildings.Count, 1);
//         jobHandle.Complete();
//     }
//     private void OnDestroy()
//     {
//         _buildingDataArray.Dispose();
//     }
// }
