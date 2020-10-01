using UnityEngine;
// ?multi pattern
public class base_react : MonoBehaviour
{
    // public bool _testDisable = false;
    // [Tooltip("Chunk detectable")] [SerializeField] protected bool _ignore = false;
    [Tooltip("Valid pattern")] [SerializeField] protected bool[] _match = new bool[0];
    [SerializeField] protected bool _oneWay = false;
    [Tooltip("Default state")] [SerializeField] protected bool _default = false;
    // [Tooltip("Minimum pattern match")] [SerializeField] protected int _count = 0;
    protected bool[] _signal;
    protected bool _active;
    // // * testing
    // protected Collider2D _collider;
    // protected SpriteRenderer _sprite;
    [SerializeField] protected SpriteRenderer _sprite;
    protected state_react _state;
    void Awake()
    {
        _signal = new bool [_match.Length];
        // _collider = GetComponent<Collider2D>();
        // _sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        manager_react.Instance.Register(this);
        New();
    }
    public virtual void New()
    {
        _state = new state_react();
        _active = _default;
        System.Array.Clear(_signal, 0, _signal.Length);
        // if (_testDisable)
        // {
        //     if (_collider && _collider.enabled != _active)
        //         _collider.enabled = _active;
        //     if (_sprite && _sprite.enabled != _active)
        //         _sprite.enabled = _active;
        // }
        if (_sprite)
            _sprite.enabled = true;
    }
    public virtual void Save()
    {
        // _active = _default;
        // System.Array.Clear(_signal, 0, _signal.Length);
        // // if (_testDisable)
        // // {
        // //     if (_collider && _collider.enabled != _active)
        // //         _collider.enabled = _active;
        // //     if (_sprite && _sprite.enabled != _active)
        // //         _sprite.enabled = _active;
        // // }
        // if (_sprite)
        //     _sprite.enabled = false;
        _state.Signal = _signal;
        _state.Active = _active;
    }
    public virtual void Load()
    {
        // _active = _default;
        // System.Array.Clear(_signal, 0, _signal.Length);
        // // if (_testDisable)
        // // {
        // //     if (_collider && _collider.enabled != _active)
        // //         _collider.enabled = _active;
        // //     if (_sprite && _sprite.enabled != _active)
        // //         _sprite.enabled = _active;
        // // }
        // if (_sprite)
        //     _sprite.enabled = false;
        _state.Signal = _signal;
        _state.Active = _active;
    }
    protected virtual void Update()
    {
        // // * testing
        // if (_sprite)
        //     // _sprite.enabled = Vector3.Distance(transform.position, controller_player.Instance.Motor.Position) <= game_variables.Instance.RadiusSprite;
        //     _sprite.enabled = game_camera.Instance.InView(transform.position);
        if (_oneWay && _active != _default)
            return;
        int count = 0;
        for (int i = _match.Length - 1; i > -1; i--)
            if (_signal[i] == _match[i])
                count++;
        // _active = (count >= _count) || (_active && _oneWay);
        _active = count == _match.Length ? !_default : _default;
        // if (_testDisable && _active)
        //     gameObject.SetActive(false);
        // if (_testDisable)
        // {
        //     if (_collider && _collider.enabled != _active)
        //         _collider.enabled = _active;
        //     if (_sprite && _sprite.enabled != _active)
        //         _sprite.enabled = _active;
        // }
    }
    public virtual void Ping(int id, bool value)
    {
        // print(gameObject.name + "," + id + "," + value);
        if (id > -1 && id < _match.Length)
            _signal[id] = value;
    }
    // public bool Ignore
    // {
    //     get { return _ignore; }
    // }
}
