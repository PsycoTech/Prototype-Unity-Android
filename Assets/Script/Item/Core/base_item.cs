using UnityEngine;
// using System.Collections.Generic;
// indestructible: key feather
// destructible: vase crate shield sack etc
public class base_item : MonoBehaviour
{
    // protected entity_data _data;
    // protected entity_motor _motor;
    // protected entity_anim _anim;
    protected Vector3 _spawn;
    // protected int _flagCollision;  // [0 - ignore | 1 - use | 2 - pickup]
    // protected List<Transform> _targets;
    // protected Transform _target;
    // protected Transform _source;
    // [SerializeField] protected float _radiusPickup = 1f;
    // [Tooltip("Chunk detectable")] [SerializeField] protected bool _ignore = false;
    // [Tooltip("Maximum use distance")] [SerializeField] protected float _radius = 1f;
    // [SerializeField] protected CircleCollider2D _collider = null;
    protected SpriteRenderer _sprite;
    [Tooltip("Health till break (-1 unbreakable)")] [SerializeField] protected int _health = -1;
    [Tooltip("Scale move speed")] [SerializeField] protected float _modifier = 1f;
    [Tooltip("Default rb state")] [SerializeField] protected bool _kinematic = false;
    [Tooltip("Default active state")] [SerializeField] protected bool _enabled = true;
    // * testing
    // [Tooltip("On used (key?collectible)")] [SerializeField] protected bool _destroy = false;
    protected int _healthInst = 0;
    // * testing drop
    protected Rigidbody2D _rb;
    protected Collider2D _collider;
    protected state_item _state;
    // [SerializeField] private bool _testDisable = false;
    protected int _uses;
    protected virtual void Awake()
    {
        // _collider = GetComponent<CircleCollider2D>();
        // _targets = new List<Transform>();
        _sprite = GetComponent<SpriteRenderer>();
        _spawn = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.angularDrag = 2f;
        _rb.drag = 2f;
        _collider = GetComponent<Collider2D>();
        // _collider.radius = .5f;
        // * testing ignore item collision
        if (_collider)
            _collider.isTrigger = false;
        // _timeShake = 0f;
        // _timerShake = 0f;
        _uses = 0;
    }
    void Start()
    {
        manager_item.Instance.Register(this);
        New();
    }
    public virtual void New()
    {
        _state = new state_item();
        // 
        transform.SetParent(null);
        transform.position = _spawn;
        transform.rotation = Quaternion.identity;
        _healthInst = _health;
        gameObject.SetActive(_enabled);
        _rb.isKinematic = _kinematic;
        _sprite.enabled = true;
    }
    public virtual void Save()
    {
        // 
        _state.Position.SetVector(transform.position);
        _state.Rotation.SetQuaternion(transform.rotation);
        _state.HealthInst = _healthInst;
        _state.Enabled = _enabled;
        // _state.Kinematic = _kinematic;
    }
    // public void Load(StateItem state = null)
    public virtual void Load()
    {
        // Clear();
        // * testing ? kinematic parent pairing preserve
        transform.SetParent(null);
        // Vector3 position = new Vector3(_state.Position.x, _state.Position.y, _state.Position.z);
        // transform.position = position;
        transform.position = _state.Position.GetVector();
        // Quaternion rotation = new Quaternion(_state.Rotation.x, _state.Rotation.y, _state.Rotation.z, _state.Rotation.w);
        // transform.rotation = rotation;
        transform.rotation = _state.Rotation.GetQuaternion();
        _healthInst = _state.HealthInst;
        gameObject.SetActive(_state.Enabled);
        // _rb.isKinematic = _state.Kinematic;
        _rb.isKinematic = _kinematic;
        _sprite.enabled = false;
    }
    // protected virtual void Update()
    // {
    //     // * testing
    //     // _sprite.enabled = Vector3.Distance(transform.position, controller_player.Instance.Motor.Position) <= game_variables.Instance.RadiusSprite;
    //     // _sprite.enabled = game_camera.Instance.InView(transform.position);
    //     // // track ? speed
    //     // transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
    //     // transform.rotation
    // }
    // protected void GetTarget()
    // {
    //     // nearest
    //     float min = float.MaxValue;
    //     foreach (Transform temp in _targets)
    //     {
    //         float distance = Vector3.Distance(transform.position, temp.position);
    //         if (distance < min)
    //         {
    //             min = distance;
    //             _target = temp;
    //         }
    //     }
    // }
    public void HealthDrain(int value)
    {
        // // * testing
        // if (_destroy)
        // {
        //     Destroy();
        //     return;
        // }
        if (_health == -1)
            return;
        // feedback_popup.Instance.RegisterMessage(transform, "health -" + value, game_variables.Instance.ColorDamage);
        feedback_toaster.Instance.RegisterMessage(gameObject.name + " : Consume -" + value, game_variables.Instance.ColorItem);
        // Shake(value);
        _healthInst -= value;
        _healthInst = Mathf.Clamp(_healthInst, 0, _health);
        if (_healthInst == 0)
            Destroy();
    }
    public virtual void SetParent(Transform parent)
    {
        // * testing pickup drop
        if (parent != null)
            transform.position = parent.position;
        // {
        //     gameObject.tag = "Ignore";
        // }
        // else
        //     gameObject.tag = "Untagged";
        transform.SetParent(parent);
        transform.localRotation = Quaternion.identity;
        // * testing collider-trigger
        if (_collider)
            _collider.isTrigger = transform.parent && transform.parent != null;
        if (_rb)
        {
            _rb.isKinematic = transform.parent != null;
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
        // * testing
        SetActive(_healthInst == -1 || _healthInst > 0);
    }
    public void SetActive(bool value)
    {
        // holdout ? chunk conflict ?
        gameObject.SetActive(value);
    }
    public virtual void Use(entity_data source, Transform target = null)
    {
        // // default : key feather
        // if (target?.gameObject.layer == game_variables.Instance.LayerInteract)
        // {
        //     print(gameObject.name + " : " + target.GetComponent<base_interact>().TryAction(transform));
        //     // if (target.GetComponent<base_interact>().TryAction(transform) == 2)
        //     // {
        //     //     // source.SetEquipped(null);
        //     //     // * testing consume
        //     //     // gameObject.SetActive(false);
        //     //     // if (_destroy)
        //     //     // print("huh");
        //     //     Destroy();
        //     //     // else
        //     //     // HealthDrain(1);
        //     // }
        //     // else
        //     //     print("wut");
        // }
        // if (_destroy)
        //     Destroy();
        // else
        HealthDrain(1);
    }
    // protected bool Pickup()
    // {
    //     // ? check
    //     // - target [player | mob]
    //     // - distance
    //     if (_target.gameObject.layer == game_variables.Instance.LayerPlayer || _target.gameObject.layer == game_variables.Instance.LayerMob)
    //     {
    //         _data = _target.GetComponent<entity_data>();
    //         _data.SetEquipped(this);
    //         _motor = _target.GetComponent<entity_motor>();
    //         // _anim = _target.GetChild(0).GetComponent<entity_anim>();
    //         return true;
    //     }
    //     return false;
    // }
    public virtual void Destroy()
    {
        // if (_data != null)
        //     _data.SetEquipped(null);
        // Load();
        // if (transform.parent && (transform.parent.gameObject.layer == game_variables.Instance.LayerPlayer || transform.parent.gameObject.layer == game_variables.Instance.LayerMob))
        //     transform.parent.GetComponent<entity_data>().SetEquipped(null);
        // SetParent(null);
        // SetParent(null);
        // drop
        if (transform.parent?.gameObject.layer == game_variables.Instance.LayerPlayer)
            controller_player.Instance.Data.Drop(this as base_item);
            // controller_player.Instance.Data.SetEquipped(null);
            // menu_inventory.Instance.SetEquipped(controller_player.Instance.Data.Equipped, false);
            // menu_inventory.Instance.SetEquipped(null);
        // deactivate
        gameObject.SetActive(false);
        _healthInst = 0;
    }
    // mob drop
    public void AddForce(Vector2 force)
    {
        if (_rb && !_rb.isKinematic)
            _rb.velocity += force;
    }
    // protected void Shake(float value)
    // {
    //     // 
    // }
    public float Modifier
    {
        get { return _modifier; }
    }
    // public float Radius
    // {
    //     get { return _radius; }
    // }
    // public bool Ignore
    // {
    //     get { return _ignore; }
    //     // print (gameObject.name + " " + _ignore);
    //     // return _ignore;
    // }
    public int HealthInst
    {
        get { return _healthInst; }
    }
    // public bool IsDestroy
    // {
    //     get { return _destroy; }
    // }
    public int Uses
    {
        get { return _uses; }
    }
}
