using UnityEngine;
// jar bullet charm
public class damage_projectile : hitbox_damage
{
    [SerializeField] protected float _impulse = 1f;
    [SerializeField] protected float _distance = 1f;
    [Tooltip("Impact force")] [SerializeField] protected float _impact = 0f;
    [Tooltip("Spawn on destroy (optional)")] [SerializeField] protected GameObject _effect = null;
    protected override void Awake()
    {
        base.Awake();
        // * testing ? start
        _rb.isKinematic = true;
        _rb.velocity = transform.up * _impulse;
        _timer = _distance / _impulse;
    }
    void OnEnable()
    {
        _rb.isKinematic = false;
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _source || other.gameObject.layer == game_variables.Instance.LayerChunk || (other.gameObject.layer == game_variables.Instance.LayerItem && other.GetComponent<Collider2D>().isTrigger))
            return;
        if (_impact > 0)
        {
            if (other.gameObject.layer == game_variables.Instance.LayerPlayer || other.gameObject.layer == game_variables.Instance.LayerMob)
                other.GetComponent<entity_motor>().AddForce((other.transform.position - transform.position).normalized * _impact);
            else if (other.gameObject.layer == game_variables.Instance.LayerItem)
                other.GetComponent<base_item>().AddForce((other.transform.position - transform.position).normalized * _impact);
        }
        base.OnTriggerEnter2D(other);
    }
    protected override void Destroy()
    {
        if (_effect)
        {
            GameObject temp = Instantiate(_effect, transform.position, transform.rotation);
            if (temp.gameObject.layer == game_variables.Instance.LayerDamage)
                temp.GetComponent<base_hitbox>().Initialize(_source);
            temp.SetActive(true);
        }
        base.Destroy();
    }
}
