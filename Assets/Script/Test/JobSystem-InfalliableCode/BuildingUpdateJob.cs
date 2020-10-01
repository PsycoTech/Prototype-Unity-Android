// using Unity.Jobs;
// using Unity.Collections;
// public struct BuildingUpdateJob : IJobParallelFor
// {
//     public NativeArray<Building> BuildingDataArray;
//     public void Execute(int index)
//     {
//         var data = BuildingDataArray[index];
//         data.Update();
//         BuildingDataArray[index] = data;
//     }
// }
