using UnityEngine;
public class damage_expand : hitbox_damage
{
    protected BoxCollider2D _collider;
    [SerializeField] protected Vector2 _fromSize = Vector2.zero;
    [SerializeField] protected Vector2 _toSize = Vector2.zero;
    [SerializeField] protected Vector2 _fromOffset = Vector2.zero;
    [SerializeField] protected Vector2 _toOffset = Vector2.zero;
    protected float _cache;
    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<BoxCollider2D>();
        _cache = _timer;
    }
    protected override void Update()
    {
        base.Update();
        _collider.size = new Vector2(Mathf.Lerp(_toSize.x, _fromSize.x, _timer / _cache), Mathf.Lerp(_toSize.y, _fromSize.y, _timer / _cache));
        _collider.offset = new Vector2(Mathf.Lerp(_toOffset.x, _fromOffset.x, _timer / _cache), Mathf.Lerp(_toOffset.y, _fromOffset.y, _timer / _cache));
    }
}
