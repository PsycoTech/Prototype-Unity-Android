using UnityEngine;
// layer : action
public class base_action : MonoBehaviour
{
    // protected entity_data _data;
    // protected CircleCollider2D _collider;
    // // protected LineRenderer _sight;
    // void Awake()
    // {
    //     _data = transform.parent.GetComponent<entity_data>();
    //     _collider = GetComponent<CircleCollider2D>();
    //     // _sight = GetComponent<LineRenderer>();
    //     // _sight.startColor = Color.magenta;
    //     // _sight.endColor = Color.magenta;
    // }
    // void Update()
    // {
    //     if (!_data.Target)
    //     {
    //         _sight.positionCount = 0;
    //         return;
    //     }
    //     // _sight.positionCount = 2;
    //     Vector3 direction = (_data.Target.position - transform.position).normalized;
    //     // RaycastHit2D hit = Physics2D.CircleCast(transform.position, .1f, direction, _collider.radius, game_variables.Instance.ScanLayerObstruction);
    //     // _sight.SetPosition(0, transform.position);
    //     // _sight.SetPosition(1, hit ? (Vector3)hit.point : transform.position + direction * _collider.radius);
    //     // _sight.SetPosition(1, transform.position + direction * _collider.radius);
    //     // _sight.SetPosition(1, _data.Target.position);
    //     if (Physics2D.CircleCast(transform.position, .5f, direction, direction.magnitude, game_variables.Instance.ScanLayerStatic))
    //         _sight.positionCount = 0;
    //     else
    //     {
    //         _sight.positionCount = 2;
    //         _sight.SetPosition(0, transform.position);
    //         _sight.SetPosition(1, _data.Target.position);
    //         // if (direction.magnitude > _collider.radius)
    //         // {
    //         //     _sight.startColor = Color.white;
    //         //     _sight.endColor = Color.white;
    //         // }
    //         // else
    //         // {
    //         //     _sight.startColor = Color.magenta;
    //         //     _sight.endColor = Color.magenta;
    //         // }
    //     }
    // }
    // void OnTriggerStay2D(Collider2D other)
    // {
    //     // // * testing ignore misc
    //     // if (other.transform != _data.Target)
    //     //     return;
    //     // // RaycastHit2D hit = Physics2D.Raycast(transform.position, other.transform.position - transform.position, _collider.radius, game_variables.Instance.ScanLayerObstruction);
    //     // RaycastHit2D hit = Physics2D.CircleCast(transform.position, .1f, other.transform.position - transform.position, _collider.radius, game_variables.Instance.ScanLayerObstruction);
    //     // if (hit)
    //     // {
    //     //     // _sight.positionCount = 2;
    //     //     // _sight.SetPosition(0, transform.position);
    //     //     // _sight.SetPosition(1, hit.point);
    //     // }
    //     // else
    //     // {
    //     //     _data.Action(other.transform);
    //     //     // _sight.positionCount = 0;
    //     // }
    //     // clear line of sight ? buffer distance
    //     if (!Physics2D.CircleCast(transform.position, .1f, other.transform.position - transform.position, _collider.radius, game_variables.Instance.ScanLayerStatic))
    //         _data.Action(other.transform);
    //     // ? queue
    // }
}
