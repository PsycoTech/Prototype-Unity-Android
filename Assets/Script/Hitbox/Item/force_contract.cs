using UnityEngine;
// pull
public class force_contract : hitbox_force
{
    [Tooltip("Contract from radius over time")] [SerializeField] protected float _radius = 1f;
    protected CircleCollider2D _collider;
    protected float _cache;
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _cache = _force;
    }
    protected override void Update()
    {
        _collider.radius = Mathf.Lerp(.1f, _radius, _timer / _time);
        // * testing scale force
        _force = Mathf.Lerp(0f, _cache, _timer / _time);
        base.Update();
    }
}
