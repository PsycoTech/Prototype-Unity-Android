using UnityEngine;
// layer : prop
// [RequireComponent(typeof(Rigidbody2D))]
public class base_prop : MonoBehaviour
{
    // [Tooltip("Chunk detectable")] [SerializeField] protected bool _ignore = false;
    [Tooltip("Health till break (-1 unbreakable)")] [SerializeField] protected int _health = -1;
    [Tooltip("Enable on destroy (optional)")] [SerializeField] protected GameObject _drop = null;
    protected int _healthInst = 0;
    protected SpriteRenderer _sprite;
    // protected Rigidbody2D _rb;
    protected state_prop _state;
    // * testing shake
    protected float _angle = 10f;
    protected float _timerShake = 0f;
    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        // _rb = GetComponent<Rigidbody2D>();
        // _rb.gravityScale = 0f;
        // _rb.drag = 0f;
        // _rb.angularDrag = 0f;
        // _rb.isKinematic = true;
        _timerShake = 0;
    }
    void Start()
    {
        manager_prop.Instance.Register(this);
        // Load();
        New();
    }
    void Update()
    {
        // * testing
        // _sprite.enabled = Vector3.Distance(transform.position, controller_player.Instance.Motor.Position) <= game_variables.Instance.RadiusSprite;
        // _sprite.enabled = game_camera.Instance.InView(transform.position);
        transform.eulerAngles = Vector3.forward * _angle * Mathf.Sin(_timerShake * Mathf.PI * 2f);
        if (_timerShake > 0)
            _timerShake -= Time.deltaTime;
    }
    // public void Load(StateItem state = null)
    public virtual void New()
    {
        _state = new state_prop();
        _healthInst = _health;
        gameObject.SetActive(true);
        if (_drop)
            _drop.SetActive(false);
        if (_sprite)
            _sprite.enabled = true;
    }
    public virtual void Save()
    {
        _state.HealthInst = _healthInst;
        _state.Active = gameObject.activeSelf;
        // gameObject.SetActive(true);
        if (_drop)
            _state.Drop = _drop.activeSelf;
        // if (_drop)
        //     _drop.SetActive(false);
        _state.Sprite = _sprite.enabled;
        // _sprite.enabled = false;
    }
    public virtual void Load()
    {
        _healthInst = _state.HealthInst;
        gameObject.SetActive(_state.Active);
        _drop?.SetActive(_state.Drop);
        _sprite.enabled = _state.Sprite;
    }
    public void HealthDrain(int value)
    {
        if (_health == -1)
            return;
        // feedback_popup.Instance.RegisterMessage(transform, "health -" + value, game_variables.Instance.ColorDamage);
        Shake(value);
        _healthInst -= value;
        _healthInst = Mathf.Clamp(_healthInst, 0, _health);
        if (_healthInst == 0)
            Destroy();
    }
    public virtual void Destroy()
    {
        // Load();
        gameObject.SetActive(false);
        if (_drop)
            _drop.SetActive(true);
        Instantiate(game_variables.Instance.ParticleDeath, transform.position, transform.rotation);
        // Instantiate(game_variables.Instance.ParticleDeath, transform.position, transform.rotation).GetComponent<ParticleSystem>().main.startColor = game_variables.Instance.ColorProp;
    }
    // * testing ? single use
    protected void Shake(float value)
    {
        _timerShake = value;
    }
    // public bool Ignore
    // {
    //     get { return _ignore; }
    // }
}
