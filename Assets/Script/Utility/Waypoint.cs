using UnityEngine;
using System.Collections.Generic;
// using UnityEditor;
public class Waypoint : MonoBehaviour
{
    [SerializeField] protected float _radius = .5f;
    // [SerializeField] protected float _time;
    // [SerializeField] protected float _timer;
    // public float factor;    // brightness, etc
    // public int flag;
    [Tooltip("Directional links")] [SerializeField] protected List<Waypoint> _neighbours = new List<Waypoint>();
    // void Awake()
    // {
    //     _timer = 0;
    // }
    void OnDrawGizmos()
    {
        // if (IsEnabled)
        //     Gizmos.color = Color.cyan;
        // else
        //     Gizmos.color = Color.red;
        // Gizmos.color = new Color(0f, 0f, 0f, .5f);
        // Gizmos.color = Color.black;
        Gizmos.color = new Color(1f, 0f, 1f, 1f);
        foreach (Waypoint next in _neighbours)
            if (next)
            // {
                Gizmos.DrawLine(transform.position, next.Position);
                // Handles.DrawBezier(transform.position, next.Position, transform.position, next.Position, Gizmos.color, null, 3);
            // }
        // Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.color = new Color(1f, 0f, 1f, .5f);
        Gizmos.DrawSphere(transform.position, _radius);
    }
    // void Update()
    // {
    //     if (_timer > 0)
    //         _timer -= Time.deltaTime;
    // }
    // public void Load()
    // {
    //     // Debug.Log(gameObject.name + "\t" + _timer);
    //     _timer = 0;
    // }
    // position + random deviation
    // public Vector3 PositionRandom
    public Vector3 PositionRandom()
    {
        // get { return transform.position + transform.right * Random.Range(-1f, 1f) + transform.forward * Random.Range(-1f, 1f); }
        // _timer = _time;
        Vector3 temp = transform.up * Random.Range(-1f, 1f) + transform.right * Random.Range(-1f, 1f);
        temp = temp.magnitude > 1f ? temp.normalized * _radius : temp * _radius;
        return transform.position + temp;
    }
    // public Vector3 PositionDirection(Vector3 target)
    // {
    //     return transform.position + (transform.position - target).normalized * _radius;
    // }
    public Waypoint GetNext()
    {
    //     return null;
    // }
    // public Waypoint GetValid()
    // {
        // default
        if (_neighbours.Count == 0)
            return null;
        return _neighbours[Random.Range(0, _neighbours.Count)];
    }
    public bool IsWaypoint(Vector3 position)
    {
        return Vector3.Distance(position, transform.position) < _radius;
    }
    // public Vector3 GetPosition()
    // {
    //     // 
    // }
    public Vector3 Position
    {
        get { return transform.position; }
    }
    // public bool IsEnabled
    // {
    //     get { return _timer <= 0; }
    // }
    // public float Radius
    // {
    //     get { return _radius; }
    // }
}
