// // Jobs Mathematics Burst Collections
// using UnityEngine;
// public class Building : MonoBehaviour
// {
//     [SerializeField] private int floors;
//     public struct Data
//     {
//         private Unity.Mathematics.Random _random;
//         private int _tenants;
//         public int PowerUsage { get; private set; }
//         public Data (Building job)
//         {
//             _random = new Unity.Mathematics.Random(1);
//             _tenants = job.floors * _random.NextInt(50, 200);
//             PowerUsage = 0;
//         }
//         public void Update()
//         {
//             for (int i = 0; i < _tenants; i++)
//             {
//                 PowerUsage += _random.NextInt(12, 24);
//             }
//         }
//     }
// }
