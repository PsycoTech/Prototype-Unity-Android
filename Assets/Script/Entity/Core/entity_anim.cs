using UnityEngine;
using System.Collections.Generic;
public class entity_anim : MonoBehaviour
{
    protected entity_motor _motor;  // local copy ?
    protected Animator _anim;
    // protected float _offset;
    [SerializeField] protected List<SpriteRenderer> _renderers = new List<SpriteRenderer>();
    [SerializeField] protected List<Sprite> _sprites = new List<Sprite>();
    // public entity_anim(entity_motor motor, Animator anim)
    // protected Vector3 _target;
    protected bool _flicker;
    // protected Transform _target;
    protected float _rotation;
    protected Color _color;
    // protected Vector3 _target;
    // [Tooltip("Rotation speed")] [SerializeField] protected float _speed = 1f;
    protected float _speed = 30f;
    protected state_anim _state;
    void Awake()
    {
        _motor = transform.parent.GetComponent<entity_motor>();
        _anim = GetComponent<Animator>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (!_renderers.Contains(renderer))
            _renderers.Add(renderer);
        if (!_sprites.Contains(renderer.sprite))
            _sprites.Add(renderer.sprite);
        // _offset = 0f;
        // _target = Vector3.zero;
        _flicker = false;
        // _target = (new GameObject()).transform;
        // _target.name = gameObject.name + "-animTarget";
        // _target.position = transform.position;
        _rotation = transform.eulerAngles.z;
        // _target = transform.position;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 5f);
    }
    // pointer ?
    // public virtual void Tick()
    // void Update()
    // {
    //     if (!_anim)
    //         return;
    //     // animation
    //     // _anim.SetFloat("Horizontal", _motor.Input.x);
    //     // _anim.SetFloat("Vertical", _motor.Input.y);
    //     // _anim.SetFloat("Speed", _motor.Input.sqrMagnitude);
    //     Vector3 direction = (_target - _motor.Position).normalized;
    //     transform.eulerAngles = new Vector3(0f, 0f, -90f + _offset + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    // }
    void Update()
    {
        // print(_target.position);
        // Vector3 direction = (_target.position - _motor.Position).normalized;
        // if (direction.sqrMagnitude > 0)
        //     transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
        // ? acceleration ? deceleration
        // transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(_rotationTo, transform.eulerAngles.z, _timer / _time));
        // print(_data.gameObject.name + ": " + transform.eulerAngles.z + " " + _rotation);
        // transform.eulerAngles += new Vector3(0f, 0f, (_rotation - transform.eulerAngles.z) * _speed * Time.deltaTime);
        // transform.eulerAngles += new Vector3(0f, 0f, (transform.eulerAngles.z - _rotation) * _speed * Time.deltaTime);
        // transform.eulerAngles = new Vector3(0f, 0f, Mathf.Lerp(_rotation, transform.eulerAngles.z, _timer / _time));
        // if (Mathf.Abs(_rotationTo - transform.eulerAngles.z) > .1f)
        //     transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + (_rotationTo - transform.eulerAngles.z) * Time.deltaTime);
        // Vector3 direction = (_target - transform.position).normalized;
        // transform.eulerAngles += new Vector3(0f, 0f, -90f + (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - transform.eulerAngles.z) * Time.deltaTime);
        // if (direction.magnitude > .1f)
            // transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.up, direction, _speed * Time.deltaTime, 0f));
        // * testing iframes flicker
        foreach (SpriteRenderer renderer in _renderers)
            if (_flicker)
                renderer.color = Color.Lerp(_color, game_variables.Instance.ColorDefault, Mathf.PingPong(Time.time * 1.5f, 1f));
            else
                renderer.color = _color;
        // * testing ? high speed jitter ? handle overshoot
        if (Mathf.Abs(_rotation) > _speed * Time.deltaTime)
            transform.eulerAngles += Vector3.forward * Mathf.Sign(_rotation) * _speed * Time.deltaTime;
        // else
        //     transform.eulerAngles += Vector3.forward * _rotation;
        // idle free rotation jank ? reset rotation on same tile
        // if ()
    }
    public void New()
    {
        // * testing
        // transform.eulerAngles = Vector3.forward * _rotation;
        // _rotationTo = _rotation;
        _color = game_variables.Instance.ColorEntity;
        // _target = transform.position;
        // _rotation = transform.eulerAngles.z;
        _state = new state_anim();
    }
    public void Save()
    {
        // * testing
        // transform.eulerAngles = Vector3.forward * _rotation;
        // _rotationTo = _rotation;
        SerializableVector4 color = new SerializableVector4();
        color.x = _color.r;
        color.y = _color.g;
        color.z = _color.b;
        color.w = _color.a;
        _state.Color = color;
        // _target = transform.position;
        _state.Rotation = transform.eulerAngles.z;
    }
    // public void Load(base_state state)
    public void Load()
    {
        // * testing
        // transform.eulerAngles = Vector3.forward * _rotation;
        // _rotationTo = _rotation;
        // _color = new Color(_state.Color.x, _state.Color.y, _state.Color.z, _state.Color.w);
        _color = _state.Color.GetColor();
        // _target = transform.position;
        transform.eulerAngles = Vector3.forward * _state.Rotation;
    }
    // * testing [? property]
    public void SpritesToggle(bool value)
    {
        foreach (SpriteRenderer renderer in _renderers)
            if (renderer.enabled != value)
                renderer.enabled = value;
    }
    // * testing snap
    public void SetTarget(Vector3 target)
    {
        // _target = target;
        Vector3 direction = (target - _motor.Position).normalized;
        // // transform.eulerAngles = new Vector3(0f, 0f, -90f + _offset + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        // transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
        // _target.position = target;
        // _rotation = -90f + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // // if (_rotation < 0f)
        // if (Mathf.Abs(_rotation - transform.eulerAngles.z) > 180f)
        //     _rotation += 360f;
        // * testing deviation ?
        // _rotation = (270f + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) % 360f;
        // _rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        // print(transform.parent?.gameObject.name + " : " + _rotation + " | " + transform.eulerAngles.z);
        // _rotation = -90f + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // _rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // print(transform.parent?.gameObject.name + " : " + angle + " | " + transform.eulerAngles.z);
        // if (angle > transform.eulerAngles.z)
        //     return angle - transform.eulerAngles.z <= value;
        // return transform.eulerAngles.z - angle <= value;
        // 
        float from = transform.eulerAngles.z % 360f;
        // // ?
        // transform.eulerAngles = Vector3.forward * from;
        from = from < 0 ? from + 360f : from;
        float to = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f) % 360f;
        to = to < 0 ? to + 360f : to;
        _rotation = to - from;
        // // * testing
        // if (_rotation < 0)
        //     _rotation += 360;
        // if (_rotation >= 360)
        //     _rotation -= 360;
        // print(_rotation);
        // _rotation = transform.eulerAngles.z + (Mathf.Abs(_rotation) > 180 ? _rotation % 180 : _rotation);
        // _rotation = Mathf.Abs(_rotation) > 180 ? _rotation % 180 : _rotation;
        _rotation = Mathf.Abs(_rotation) > 180 ? -_rotation % 180 : _rotation;
        // print(_rotation);
    }
    public void SetFlicker(bool value)
    {
        _flicker = value;
    }
    public void SetRotation(float value)
    {
        transform.eulerAngles = new Vector3(0f, 0f, -90f + value);
        // _target = transform.position;
        // _rotationTo = -90f + value;
        // _rotation = -90f + value;
        // _target.position = _motor.Position;
        // _timer = _time;
        // _rotation = transform.eulerAngles.z;
        _rotation = 0f;
    }
    public void SetRotation(Vector3 target)
    {
        Vector3 direction = (target - _motor.Position).normalized;
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
        // _target = transform.position;
        // _rotationTo = -90f + value;
        // _rotation = -90f + value;
        // _target.position = _motor.Position;
        // _timer = _time;
        // _rotation = transform.eulerAngles.z;
        _rotation = 0f;
    }
    public void SetSprite(int value)
    {
        if (value > -1 && value < _sprites.Count)
        {
            // * testing sprite ? palette / sheet [? slow / every cycle ? cache]
            foreach (SpriteRenderer renderer in _renderers)
                renderer.sprite = _sprites[value];
        }
    }
    public void SetColor(Color value)
    {
        if (_color != value)
            _color = value;
    }
    public bool IsDirection(Vector3 target, float value)
    {
        Vector3 direction = (target - _motor.Position).normalized;
        // return Mathf.Abs(Mathf.Abs((-90f + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - transform.eulerAngles.z) - 360f) <= value;
        // return Mathf.Abs((-90f + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - transform.eulerAngles.z) <= value + 360f;
        // return Mathf.Abs(Mathf.Abs((Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)) - Mathf.Abs(transform.eulerAngles.z)) <= value;
        // * testing deviation ?
        float angle = (270f + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) % 360f;
        // print(transform.parent?.gameObject.name + " : " + angle + " | " + transform.eulerAngles.z);
        if (angle > transform.eulerAngles.z)
            return angle - transform.eulerAngles.z <= value;
        return transform.eulerAngles.z - angle <= value;
    }
    public float PositionToAngle(Vector3 target)
    {
        Vector3 direction = (target - _motor.Position).normalized;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        // return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - transform.eulerAngles.z;
        // return (270f + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) % 360f;
        return Vector2.Angle(transform.up, direction);
    }
    public float Rotation
    {
        get { return transform.eulerAngles.z; }
    }
    // public float Offset
    // {
    //     set { _offset = value; }
    // }
    public Vector2 Direction
    {
        get { return transform.up; }
    }
}
