using UnityEngine;
public class MarkerAnchor : MonoBehaviour
{
    public float _radius = 1f;
    public Color _color = Color.white;
    void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
