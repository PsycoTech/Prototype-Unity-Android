using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class proximity_direction : base_proximity
{
    [SerializeField] protected int _id = 0;
    [SerializeField] protected int _damage = 0;
    [Tooltip("Damage tick delay")] [SerializeField] protected float _timeDamage = 0f;
    [Tooltip("Max distance check")] [SerializeField] protected float _distance = .5f;
    [Tooltip("Travel velocity")] [SerializeField] protected float _speed = 1f;
    protected float _timerDamage;
    protected float _distanceInst;
    protected Transform _near;
    // * testing
    protected LineRenderer _line;
    protected override void Start()
    {
        _line = GetComponent<LineRenderer>();
        base.Start();
    }
    public override void New()
    {
        base.New();
        _timerDamage = 0f;
        _distanceInst = 0f;
        _near = null;
        // !?
        if (_line)
        {
            _line.positionCount = 2;
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, transform.position);
        }
    }
    public override void Load()
    {
        base.Load();
        // _timerDamage = _state.TimerDamage;
        // _distanceInst = _state.DistanceInst;
        _timerDamage = 0f;
        _distanceInst = 0f;
        _near = null;
        // !?
        if (_line)
        {
            _line.positionCount = 2;
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, transform.position);
        }
    }
    protected override void Update()
    {
        base.Update();
        if (_timerDamage > 0)
            _timerDamage -= Time.deltaTime;
        if (_near && (!_targets.Contains(_near) || ((_near.gameObject.layer == game_variables.Instance.LayerItem || _near.gameObject.layer == game_variables.Instance.LayerMob) && _near.GetComponent<Collider2D>().isTrigger)))
            _near = null;
        if (_active)
        {
            foreach (Transform target in _targets)
            {
                float distance = Vector3.Distance(target.position, transform.position);
                if (distance > _distanceInst || ((target.gameObject.layer == game_variables.Instance.LayerItem || target.gameObject.layer == game_variables.Instance.LayerMob) && target.GetComponent<Collider2D>().isTrigger))
                    continue;
                if (_near)
                {
                    if (distance < Vector3.Distance(_near.position, transform.position))
                    {
                        if (_near.gameObject.layer == game_variables.Instance.LayerReact)
                            _near.GetComponent<base_react>().Ping(_id, false);
                        _near = target;
                        _distanceInst = distance;
                    }
                }
                else
                {
                    _near = target;
                    _distanceInst = distance;
                }
            }
            if (_near)
            {
                if (_damage != 0 && _timerDamage <= 0)
                {
                    if (_near.gameObject.layer == game_variables.Instance.LayerPlayer || _near.gameObject.layer == game_variables.Instance.LayerMob)
                        _near.GetComponent<entity_data>().HealthDrain(_damage);
                    else if (_near.gameObject.layer == game_variables.Instance.LayerItem)
                        _near.GetComponent<base_item>().HealthDrain(_damage);
                    else if (_near.gameObject.layer == game_variables.Instance.LayerProp)
                        _near.GetComponent<base_prop>()?.HealthDrain(_damage);
                    _timerDamage = _timeDamage;
                }
                if (_near.gameObject.layer == game_variables.Instance.LayerReact)
                    _near.GetComponent<base_react>().Ping(_id, true);
            }
        }
        else if (!_active && _near)
        {
            _distanceInst = 0f;
            if (_near.gameObject.layer == game_variables.Instance.LayerReact)
                _near.GetComponent<base_react>().Ping(_id, false);
            else
                _near = null;
        }
        if (_near)
        {
            Vector3 direction = _near.position - transform.position;
            _line.SetPosition(1, transform.position + new Vector3(Mathf.Abs(transform.up.x) * direction.x, Mathf.Abs(transform.up.y) * direction.y, 0f));
            _distanceInst = direction.magnitude;
        }
        else
        {
            if (_distanceInst < _distance)
                _distanceInst += Time.deltaTime * _speed;
            _line.SetPosition(1, transform.position + transform.up * _distanceInst);
        }
    }
    void OnEnable()
    {
        // ? new or load save file check
        New();
    }
    void OnDisable()
    {
        if (_near && _near.gameObject.layer == game_variables.Instance.LayerReact)
            _near.GetComponent<base_react>().Ping(_id, false);
        // !?
        if (_line)
            _line.positionCount = 0;
    }
}
