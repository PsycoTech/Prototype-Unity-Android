using UnityEngine;
// bash
public class hitbox_force : base_hitbox
{
    [SerializeField] protected float _force = 0f;
    [Tooltip("Time till destroy")] [SerializeField] protected float _time = 1f;
    protected float _timer;
    protected override void Awake()
    {
        base.Awake();
        _timer = _time;
    }
    protected override void Update()
    {
        base.Update();
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else
            Destroy();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _source)
            return;
        if (other.gameObject.layer == game_variables.Instance.LayerPlayer || other.gameObject.layer == game_variables.Instance.LayerMob)
            other.GetComponent<entity_motor>().AddForce((other.transform.position - transform.position).normalized * _force);
        else if (other.gameObject.layer == game_variables.Instance.LayerItem)
            other.GetComponent<base_item>().AddForce((other.transform.position - transform.position).normalized * _force);
    }
}
