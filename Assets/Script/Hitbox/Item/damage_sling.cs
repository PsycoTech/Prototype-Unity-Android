using UnityEngine;
// dagger
[RequireComponent(typeof(BoxCollider2D))]
public class damage_sling : hitbox_damage
{
    [SerializeField] protected float _distance = 1f;
    [SerializeField] protected Vector2 _fromSize = Vector2.one * .1f;
    [SerializeField] protected Vector2 _toSize = Vector2.one * .1f;
    [SerializeField] protected Vector2 _fromOffset = Vector2.zero;
    [SerializeField] protected Vector2 _toOffset = Vector2.zero;
    protected BoxCollider2D _collider;
    protected float _time;
    protected override void Awake()
    {
        base.Awake();
        // * testing ? start
        _collider = GetComponent<BoxCollider2D>();
        _rb.isKinematic = true;
        _time = _timer;
    }
    protected override void Update()
    {
        base.Update();
        _collider.size = new Vector2(Mathf.Lerp(_toSize.x, _fromSize.x, _timer / _time), Mathf.Lerp(_toSize.y, _fromSize.y, _timer / _time));
        _collider.offset = new Vector2(Mathf.Lerp(_toOffset.x, _fromOffset.x, _timer / _time), Mathf.Lerp(_toOffset.y, _fromOffset.y, _timer / _time));
    }
}
