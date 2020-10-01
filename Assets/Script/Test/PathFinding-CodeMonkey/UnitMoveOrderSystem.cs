// using UnityEngine;
// using Unity.Entities;
// using Unity.Transforms;
// using Unity.Mathematics;
// public class UnitMoveOrderSystem : ComponentSystem
// {
//     protected override void OnUpdate()
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//             Entities.ForEach((Entity entity, ref Translation translation2D) =>
//             {
//                 // add pathfinding params
//                 EntityManager.AddComponentData(entity, new PathFindingParams {
//                     startPosition = new int2(0, 0),
//                     endPosition = new int2(4, 0),
//                 });
//             });
//         }
//     }
// }
