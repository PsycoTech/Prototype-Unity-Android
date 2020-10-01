using UnityEngine;
public class damage_expandCircle : hitbox_damage
{
    protected CircleCollider2D _collider;
    [SerializeField] protected float _from = .1f;
    [SerializeField] protected float _to = .1f;
    protected float _cache;
    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<CircleCollider2D>();
        _cache = _timer;
    }
    protected override void Update()
    {
        base.Update();
        _collider.radius = Mathf.Lerp(_to, _from, _timer / _cache);
    }
}
