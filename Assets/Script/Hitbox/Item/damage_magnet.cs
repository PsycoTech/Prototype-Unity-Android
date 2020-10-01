using UnityEngine;
// axe
public class damage_magnet : hitbox_damage
{
    [SerializeField] protected float _impulse = 1f;
    [SerializeField] protected float _distance = 1f;
    [SerializeField] protected float _magnet = 1f;
    [Tooltip("Item reference that will spawn this")] [SerializeField] protected GameObject _item = null;
    protected override void Awake()
    {
        base.Awake();
        // * testing ? start
        _rb.velocity = transform.up * _impulse;
        _timer = _distance / _impulse;
    }
    void FixedUpdate()
    {
        // _rb.velocity += (_rb.position - (Vector2)_source.position).normalized * _magnet * Time.deltaTime;
        Vector3 direction = _source.position - transform.position;
        float magnitude = Mathf.Pow(direction.magnitude, 2);
        _rb.AddForce(direction * magnitude * _magnet);
        if (_rb.velocity.magnitude > _impulse)
            _rb.velocity = _rb.velocity.normalized * _impulse;
        if (Vector2.Distance(_rb.position, _source.position) < 1f)
            Destroy();
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _source || other.transform == _item.transform || other.gameObject.layer == game_variables.Instance.LayerChunk)
            return;
        if (other.gameObject.layer == game_variables.Instance.LayerSolid || other.gameObject.layer == game_variables.Instance.LayerInteract || other.gameObject.layer == game_variables.Instance.LayerItem)
            Destroy();
        else
            base.OnTriggerEnter2D(other);
    }
    protected override void Destroy()
    {
        _item.transform.position = transform.position;
        _item.SetActive(true);
        base.Destroy();
    }
}
