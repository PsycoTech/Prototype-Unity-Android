using UnityEngine;
using System.Collections.Generic;
// entity ?item
// oneWay hold ?clear ?capacity
// timer instant delay
public class base_proximity : MonoBehaviour
{
    [Tooltip("Initial state")] [SerializeField] protected bool _active = false;
    [Tooltip("Lock state change")] [SerializeField] protected bool _oneWay = false;
    [Tooltip("Delay till active")] [SerializeField] protected float _timeHold = 0f;
    [Tooltip("Delay till inactive")] [SerializeField] protected float _timeRelease = 0f;
    // [SerializeField] protected int _capacity = 1;
    protected bool _cache;
    protected float _timerHold;
    protected float _timerRelease;
    protected List<Transform> _targets;
    protected SpriteRenderer _sprite;
    protected state_proximity _state;
    void Awake()
    {
        _targets = new List<Transform>();
        _cache = _active;
        _sprite = GetComponent<SpriteRenderer>();
    }
    protected virtual void Start()
    {
        manager_proximity.Instance.Register(this);
        New();
    }
    public virtual void New()
    {
        _state = new state_proximity();
        _active = _cache;
        _timerHold = 0f;
        _timerRelease = 0f;
        if (_sprite)
            _sprite.enabled = true;
    }
    public virtual void Save()
    {
        _state.Cache = _cache;
        _state.TimerHold = _timerHold;
        _state.TimerRelease = _timerRelease;
    }
    public virtual void Load()
    {
        _cache = _state.Cache;
        _active = _cache;
        _timerHold = _state.TimerHold;
        _timerRelease = _state.TimerRelease;
    }
    protected virtual void Update()
    {
        // // * testing
        // if (_sprite)
        //     // _sprite.enabled = Vector3.Distance(transform.position, controller_player.Instance.Motor.Position) <= game_variables.Instance.RadiusSprite;
        //     _sprite.enabled = game_camera.Instance.InView(transform.position);
        if (_oneWay && _active != _cache)
            return;
        // * testing manual stream
        List<Transform> toRemove = new List<Transform>();
        foreach (Transform target in _targets)
            if (!target.gameObject.activeSelf)
                toRemove.Add(target);
        foreach (Transform target in toRemove)
            _targets.Remove(target);
        // if (_targets.Count >= _capacity)
        if (_targets.Count > 0)
        {
            _timerRelease = 0f;
            if (_timerHold < _timeHold)
                _timerHold += Time.deltaTime;
            else
                _active = !_cache;
        }
        else
        {
            _timerHold = 0f;
            if (_timerRelease < _timeRelease)
                _timerRelease += Time.deltaTime;
            else
                _active = _cache;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == game_variables.Instance.LayerChunk || _targets.Contains(other.transform) || other.isTrigger)
            return;
        _targets.Add(other.transform);
    }
    // void OnTriggerStay2D(Collider2D other)
    // {
    //     if (_targets.Contains(other.transform))
    //         return;
    //     _targets.Add(other.transform);
    // }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == game_variables.Instance.LayerChunk)
            return;
        if (_targets.Contains(other.transform))
            _targets.Remove(other.transform);
    }
}
