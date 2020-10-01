using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(Rigidbody2D))]
public class hitbox_damage : base_hitbox
{
    [SerializeField] protected int _damage = 0;
    [SerializeField] protected bool _impactSensitive = false;
    // * testing [self destruct tuning]
    [Tooltip("Time till self destruct (-1 off)")] [SerializeField] protected float _timer = -1f;
    [Tooltip("Time between DOT ticks (-1 off)")] [SerializeField] protected float _damageTime = -1f;
    protected float _damageTimer = 0f;
    protected Rigidbody2D _rb;
    protected List<GameObject> _targets;
    [Tooltip("Invalid targets")] [SerializeField] protected List<GameObject> _ignore = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _targets = new List<GameObject>();
    }
    protected override void Update()
    {
        base.Update();
        if (_damageTimer > 0)
            _damageTimer -= Time.deltaTime;
        else if (_damageTime != -1 && _targets.Count > 0)
        {
            // List<GameObject> toRemove = new List<GameObject>();
            foreach (GameObject target in _targets)
            {
                if (_ignore.Contains(target))
                    continue;
                // if (!target.activeSelf)
                //     toRemove.Add(target);
                if (target.layer == game_variables.Instance.LayerPlayer || target.layer == game_variables.Instance.LayerMob)
                    target.GetComponent<entity_data>().HealthDrain(_damage, _source);
                else if (target.layer == game_variables.Instance.LayerItem)
                    target.GetComponent<base_item>().HealthDrain(_damage);
                else if (target.layer == game_variables.Instance.LayerProp)
                    target.GetComponent<base_prop>()?.HealthDrain(_damage);
            }
            // foreach (GameObject target in toRemove)
            //     _targets.Remove(target);
            _damageTimer = _damageTime;
        }
        if (_timer != -1)
        {
            if (_timer > 0)
                _timer -= Time.deltaTime;
            else
                Destroy();
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _source || other.transform == transform.parent || other.gameObject.layer == game_variables.Instance.LayerChunk || (other.gameObject.layer == game_variables.Instance.LayerItem && other.GetComponent<Collider2D>().isTrigger) || _ignore.Contains(other.gameObject))
            return;
        if (_damage != 0)
        {
            if (_damageTime == -1)
            {
                // print("Trigger " + gameObject.name + ":" + _source);
                if (other.gameObject.layer == game_variables.Instance.LayerPlayer || other.gameObject.layer == game_variables.Instance.LayerMob)
                    other.GetComponent<entity_data>().HealthDrain(_damage, _source);
                else if (other.gameObject.layer == game_variables.Instance.LayerItem)
                    other.GetComponent<base_item>().HealthDrain(_damage);
                else if (other.gameObject.layer == game_variables.Instance.LayerProp)
                    other.GetComponent<base_prop>()?.HealthDrain(_damage);
                else if (other.gameObject.layer == game_variables.Instance.LayerInteract)
                    // other.GetComponent<base_interact>()?.TryAction(null);
                    // other.GetComponent<base_interact>()?.TryAction(transform);
                    other.GetComponent<base_interact>()?.TryAction(_source);
            }
            else if (other.gameObject.layer == game_variables.Instance.LayerPlayer || other.gameObject.layer == game_variables.Instance.LayerMob || other.gameObject.layer == game_variables.Instance.LayerItem || other.gameObject.layer == game_variables.Instance.LayerProp)
            {
                // * testing grace period
                if (_damageTimer <= 0f)
                    // _damageTimer = _damageTime / 2;
                    _damageTimer = .2f;
                if (!_targets.Contains(other.gameObject))
                    _targets.Add(other.gameObject);
            }
        }
        if (_impactSensitive)
            Destroy();
    }
    // void OnTriggerStay2D(Collider2D other)
    // {
    //     // print(other.gameObject.name);
    //     if (other.transform == _source || other.transform == transform.parent || _damage == 0 || _damageTime == -1 || _damageTimer > 0)
    //         return;
    //     // print("COCK");
    //     if (other.gameObject.layer == game_variables.Instance.LayerPlayer || other.gameObject.layer == game_variables.Instance.LayerMob)
    //         other.GetComponent<entity_data>().HealthDrain(_damage, _source);
    //     else if (other.gameObject.layer == game_variables.Instance.LayerItem)
    //         other.GetComponent<base_item>().HealthDrain(_damage);
    //     else if (other.gameObject.layer == game_variables.Instance.LayerProp)
    //         other.GetComponent<base_prop>()?.HealthDrain(_damage);
    //     // else if (other.gameObject.layer == game_variables.Instance.LayerInteract)
    //     //     other.GetComponent<base_interact>().HealthDrain(_damage);
    //     _damageTimer = _damageTime;
    // }
    void OnTriggerExit2D(Collider2D other)
    {
        // if (other.transform == _source || other.transform == transform.parent)
        //     return;
        if (_targets.Contains(other.gameObject))
            _targets.Remove(other.gameObject);
    }
}
