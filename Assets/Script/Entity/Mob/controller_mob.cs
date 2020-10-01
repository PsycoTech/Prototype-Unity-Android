using UnityEngine;
using Panda;
using System.Collections.Generic;
public class controller_mob : entity_controller
{
    // * testing [minotaur lizard]
    // [SerializeField] protected bool _alwaysActive;
    // protected bool _isBT;
    // protected PandaBehaviour _BT;
    protected bool _isAlert;
    public float _attentionTime;    // ignore/forget older sounds ? events
    protected float _sensorTimer;   // time since sensor event
    // * testing
    [SerializeField] protected List<sensor_vision> _sensorsVision;
    [SerializeField] protected List<sensor_trigger> _sensorsTrigger;
    protected class EventPoint
    {
        public Vector3 Position;
        public LayerMask Layer;
        public float Time;
        public EventPoint()
        {
            // * testing
        }
        public EventPoint(Vector3 position, LayerMask layer, float time)
        {
            Position = position;
            Time = time;
        }
    }
    protected EventPoint _anchor;
    protected EventPoint _positionVision;
    protected EventPoint _positionTrigger;
    protected float _awareTimer;
    protected bool _isAware;
    // [SerializeField] protected List<Waypoint> _waypoints;
    // // * testing
    // protected List<Transform> _hostiles;
    [Tooltip("Number of internal timers")] [SerializeField] protected int _countTimers;
    protected float[] _timers;
    protected bool _isVision;
    protected bool _isTrigger;
    [Tooltip("Number of internal flags")] [SerializeField] protected int _countFlags;
    protected bool[] _flags;
    // * testing [attack ? effect]
    [Tooltip("Attacks, effects, etc")] [SerializeField] protected List<GameObject> _prefabs;
    [Tooltip("Prefab spawn points")] [SerializeField] protected List<Transform> _spawns;
    protected bool _isSensors;
    // * testing new ?continue
    protected bool _sleep;
    // * testing feedback status search ? detect !
    protected int _flagStatus;
    // * testing load sensors trigger
    [Tooltip("Sensor trigger gameObject")] [SerializeField] protected string _trigger = "";
    [Tooltip("Sensors check area")] [SerializeField] protected Vector2Int _bounds = Vector2Int.one;
    [Tooltip("Check area offset")] [SerializeField] protected Vector2 _offset = Vector2.zero;
    // protected List<Vector2> _test = new List<Vector2>();
    // private int _id;
    [Tooltip("Initial state ? root (optional?)")] [SerializeField] protected Waypoint _waypoint = null;
    private state_controller _state;
    // private LineRenderer _feedbackFOV;
    protected override void Awake()
    {
        // ? post
        base.Awake();
        // 
        // _anchor = Instantiate(new GameObject(), transform.position, transform.rotation).transform;
        // _anchor = (new GameObject()).transform;
        // _anchor.position = transform.position;
        // _anchor.rotation = transform.rotation;
        // _anchor.gameObject.name = gameObject.name + "-anchorMove";
        _anchor = new EventPoint(transform.position, gameObject.layer, 0f);
        // 
        // _BT = GetComponent<PandaBehaviour>();
        // _isBT = false;
        _isAlert = false;
        _sensorTimer = _attentionTime;
        _positionVision = null;
        _positionTrigger = null;
        _awareTimer = 0;
        _isAware = false;
        // _hostiles = new List<Transform>();
        _timers = new float[_countTimers];
        _flags = new bool[_countFlags];
        _isVision = true;
        _isTrigger = true;
        _isSensors = true;
        _sleep = true;
        _timerPath = 0f;
        _flagStatus = -1;
        // _id = 0;
        // _feedbackFOV = GetComponent<LineRenderer>();
        // if (_feedbackFOV)
        //     _feedbackFOV.positionCount = 0;
        // Vector3[] temp = new Vector3[path.Length + 1];
        // _feedbackFOV.SetPositions();
    }
    // * testing move
    void OnDrawGizmos()
    {
        // if (_trigger != "")
        // {
        //     Gizmos.color = Color.red;
        //     // Gizmos.DrawWireCube(_motor.Position + (Vector3)_offset, new Vector3(_bounds.x, _bounds.y));
        //     Gizmos.DrawWireCube(transform.position + (Vector3)_offset, new Vector3(_bounds.x - .1f, _bounds.y - .1f));
        // }
        // foreach (sensor_trigger sensor in _sensorsTrigger)
        // {
        //     Gizmos.color = new Color(1f, 0f, 0f, .5f);
        //     Gizmos.DrawCube(sensor.transform.position, Vector3.one * .9f);
        // }
        // foreach (Vector2 test in _test)
        // {
        //     Gizmos.color = new Color(1f, 0f, 0f, .5f);
        //     Gizmos.DrawCube(test, Vector3.one * .9f);
        // }
        if (_anchor != null)
        {
            Gizmos.color = new Color(1f, 1f, 1f, .5f);
            Gizmos.DrawSphere(_anchor.Position, .5f);
            Gizmos.DrawLine(_motor.Position, _anchor.Position);
        }
    }
    void Start()
    {
        manager_mob.Instance.Register(this);
        // 
        // foreach (sensor_vision sensor in _sensorsVision)
        //     sensor.SetActive(true);
        if (_trigger != "")
            for (float x = -_bounds.x / 2; x <= _bounds.x / 2; x++)
                for (float y = -_bounds.y / 2; y <= _bounds.y / 2; y++)
                {
                    // // * testing
                    // _test.Add((Vector2)transform.position + new Vector2(x, y) + _offset);
                    // sensor_trigger temp = Physics2D.OverlapCircle(_test[_test.Count - 1], .9f, game_variables.Instance.ScanLayerSensor)?.gameObject.GetComponent<sensor_trigger>();
                    sensor_trigger temp = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(x, y) + _offset, .1f, game_variables.Instance.ScanLayerSensor)?.gameObject.GetComponent<sensor_trigger>();
                    if (temp?.name == _trigger && !_sensorsTrigger.Contains(temp))
                        _sensorsTrigger.Add(temp);
                }
        foreach (sensor_trigger sensor in _sensorsTrigger)
            sensor.SetActive(true);
        
    }
    // new
    public virtual void New()
    {
        _state = new state_controller();
        // _isBT = true;
        // _state. = _isAlert;
        // _sensorTimer = _attentionTime;
        _isAlert = false;
        _sensorTimer = _attentionTime;
        _awareTimer = 0f;
        _state.AwareTimer = _awareTimer;
        _isAware = false;
        _state.IsAware = _isAware;
        _positionVision = null;
        _state.PositionVision.SetVector(_motor.Position);
        _positionTrigger = null;
        _state.PositionTrigger.SetVector(_motor.Position);
        // _motor.ToSpawn();
        // _anim.Load(_state);
        _anim.New();
        // _motor.Load(_state);
        _motor.New();
        // _motor.CollidersToggle(true);
        _data.HealthRestore();
        // _hostiles.Clear();
        // * testing [nani? index?]
        System.Array.Clear(_timers, 0, _countTimers);
        System.Array.Clear(_flags, 0, _countFlags);
        _isVision = true;
        _isTrigger = true;
        // _state.Sensors = ?;
        // foreach (sensor_vision sensor in _sensorsVision)
        //     sensor.SetActive(true);
        foreach (sensor_trigger sensor in _sensorsTrigger)
            sensor.SetActive(true);
        _isSensors = true;
        _sleep = false;
        _state.Sleep = _sleep;
        _timerPath = 0f;
        _flagStatus = -1;
        _state.FlagStatus = _flagStatus;
        _anim.SpritesToggle(true);
    }
    public virtual void Save()
    {
        _state.AwareTimer = _awareTimer;
        _state.IsAware = _isAware;
        _state.PositionVision.SetVector(_motor.Position);
        _state.PositionTrigger.SetVector(_motor.Position);
        _anim.Save();
        _motor.Save();
        _data.Save();
        _state.Sleep = _sleep;
        _state.FlagStatus = _flagStatus;
    }
    public virtual void Load()
    {
        // _isBT = true;
        _isAlert = false;
        _sensorTimer = _attentionTime;
        _positionVision = null;
        _positionTrigger = null;
        _awareTimer = 0;
        _isAware = false;
        // _motor.ToSpawn();
        // _anim.Load(_state);
        _anim.Load();
        // _motor.Load(_state);
        _motor.Load();
        _motor.CollidersToggle(true);
        _data.HealthRestore();
        // _hostiles.Clear();
        // * testing [nani? index?]
        System.Array.Clear(_timers, 0, _countTimers);
        System.Array.Clear(_flags, 0, _countFlags);
        _isVision = true;
        _isTrigger = true;
        // foreach (sensor_vision sensor in _sensorsVision)
        //     sensor.SetActive(true);
        foreach (sensor_trigger sensor in _sensorsTrigger)
            sensor.SetActive(true);
        _isSensors = true;
        _sleep = false;
        _timerPath = 0f;
        _flagStatus = -1;
        _anim.SpritesToggle(true);
    }
    void Update()
    {
        if (_sleep)
        {
            // _anim.SpritesToggle(game_camera.Instance.InView(_motor.Position));
            return;
        }
        // // * testing [redundant calculation ?]
        // if (_data.HealthInst > 0)
        //     _anim.SpritesToggle(game_camera.Instance.InView(_motor.Position));
        // ---
        // ??? awareness
        if (Sensor_Vision() || Sensor_Trigger())
        {
            // _awareTimer += _awareTime * Time.deltaTime * Vector3.Distance(transform.position, _positionVision) / _visionRadius;
            // _awareTimer += _attentionTime * Time.deltaTime * Vector3.Distance(transform.position, _positionVision) / _visionRadius;
            _awareTimer += _attentionTime * Time.deltaTime;
            if (_isAware)
            {
                if (_flagStatus != 1)
                {
                    _flagStatus = 1;
                    feedback_popup.Instance.SetAlert(transform, 1);
                }
            }
            else if (_flagStatus != 0)
            {
                _flagStatus = 0;
                feedback_popup.Instance.SetAlert(transform, 0);
            }
            // // ? feedback
            // if (_feedbackFOV)
            // {
            //     if (_feedbackFOV.positionCount != 2)
            //         _feedbackFOV.positionCount = 2;
            //     _feedbackFOV.SetPosition(0, _motor.Position);
            //     _feedbackFOV.SetPosition(1, _anchor.Position);
            // }
        }
        else if (_awareTimer > 0)
        {
            _awareTimer -= Time.deltaTime;
            if (_flagStatus != 0)
            {
                _flagStatus = 0;
                feedback_popup.Instance.SetAlert(transform, 0);
            }
            // // ? feedback
            // if (_feedbackFOV)
            //     _feedbackFOV.SetPosition(0, _motor.Position);
        }
        else if (_flagStatus != -1)
        {
            _flagStatus = -1;
            // // ? feedback
            // if (_feedbackFOV)
            // {
            //     feedback_popup.Instance.SetAlert(transform, -1);
            //     if (_feedbackFOV.positionCount != 2)
            //         _feedbackFOV.positionCount = 2;
            // }
        }
        // if (_awareTimer > _awareTime)
        if (_awareTimer > _attentionTime)
        {
            // _awareTimer = _awareTime;
            _awareTimer = _attentionTime;
            _isAware = true;
        }
        else if (_awareTimer < 0)
        {
            _awareTimer = 0;
            _isAware = false;
        }
        _sensorTimer += Time.deltaTime;
        _sensorTimer = Mathf.Clamp(_sensorTimer, 0, _attentionTime);
        // * testing
        for (int i = 0; i < _countTimers; i++)
            if (_timers[i] > 0f)
                _timers[i] -= Time.deltaTime;
        if (_timerPath > 0)
            _timerPath -= Time.deltaTime;
        // * testing discard old [conduit]
        if (_positionTrigger != null && Time.time - _positionTrigger.Time > _attentionTime)
            _positionTrigger = null;
        if (_positionVision != null && Time.time - _positionVision.Time > _attentionTime)
            _positionVision = null;
    }
    // // ? no need
    // protected override void FixedTick()
    // {
    //     // ? post
    //     base.FixedTick();
    //     // 
    //     // if (_motor && input_touch.Instance.EventTap)
    //     // {
    //     //     _motor.NavigateTo(input_touch.Instance.CacheTapWorld);
    //     //     feedback_move.Instance.SetPosition(input_touch.Instance.CacheTapWorld);
    //     //     // * testing
    //     //     if (input_touch.Instance.EventTapDouble)
    //     //         _motor.AddForce((input_touch.Instance.CacheTapWorld - controller_player.Instance.Motor.Position).normalized * _speedDash);
    //     // }

    //     // * testing
    //     // ? move to BT
    //     // Spawn
    //     // - enable
    //     if (game_camera.Instance.InView(_motor.Position))
    //     {
    //         Debug.Log("enable");
    //         // gameObject.SetActive(true);
    //         SpriteEnable();
    //     }
    //     // - disable
    //     else if (Vector3.Distance(_motor.Position, controller_player.Instance.Motor.Position) > _radiusSpawn)
    //     {
    //         Debug.Log("disable");
    //         // gameObject.SetActive(false);
    //         SpriteDisable();
    //         _motor.ToSpawn();
    //     }
    // }

    // // * testing chunk
    // public void ActiveToggle(bool value)
    // {
    //     gameObject.SetActive(value);
    // }

    // position in waypoint
    // protected bool IsWaypoint(Vector3 position)
    // {
    //     // foreach (Waypoint waypoint in _waypoints)
    //     //     // if (position == waypoint.Position)
    //     //     if (Vector3.Distance(position, waypoint.Position) <= waypoint.Radius)
    //     //         return true;
    //     // return false;
    //     // _waypoint.IsWaypoint()
    // }
    // // random waypoint, else current position
    // protected Vector3 GetWaypointRandom()
    // {
    //     List<Waypoint> waypoints = Filter_Waypoints();
    //     // Debug.Log("random\t" + waypoints.Count);
    //     // default
    //     if (waypoints.Count == 0)
    //         return _motor.Spawn;
    //     _id = Random.Range(0, waypoints.Count);
    //     return waypoints[_id].PositionRandom();
    // }
    // waypoint, else current position
    // protected Vector3 GetWaypoint(bool jitter)
    protected Vector3 GetWaypoint()
    {
        // List<Waypoint> waypoints = Filter_Waypoints();
        // Debug.Log("random\t" + waypoints.Count);
        // default
        // if (waypoints.Count == 0)
        //     return _motor.Spawn;
        // waypoints.FindIndex(a => a.Waypoint == waypoints[_id].GetNext());
        // _id = waypoints[_id].GetNext();
        // return waypoints[_id].PositionRandom();
        // return waypoints[_id].Position();
        if (_waypoint)
        {
            _waypoint = _waypoint.GetNext();
            return _waypoint ? _waypoint.Position : _motor.Spawn;
        }
        return _motor.Spawn;
    }
    // * testing
    protected Vector3 GetWaypointOffset()
    {
        if (_waypoint)
        {
            _waypoint = _waypoint.GetNext();
            return _waypoint ? _waypoint.PositionRandom() : _motor.Spawn;
        }
        return _motor.Spawn;
    }
    // override must always call base ?
    // protected override List<Waypoint> Filter_Waypoints(float distanceMin = int.MinValue, float distanceMax = int.MaxValue)
    // protected virtual List<Waypoint> Filter_Waypoints()
    // {
    //     List<Waypoint> waypoints = new List<Waypoint>();
    //     foreach (Waypoint waypoint in _waypoints)
    //         if (waypoint.IsEnabled)
    //             waypoints.Add(waypoint);
    //     return waypoints;
    // }
    // on alerted ?
    // protected void EnableWaypoints()
    // {
    //     foreach (Waypoint waypoint in Filter_Waypoints())
    //         waypoint.Load();
    // }
    protected bool Sensor_Vision()
    {
        if (!_isVision || !_isSensors)
            return false;
        float distance = float.MaxValue;
        foreach (sensor_vision sensor in _sensorsVision)
        {
            foreach (Transform target in sensor.Targets)
            {
                if (!_data.Hostiles.Contains(target))
                    continue;
                // ? closest [memory waste ?]
                if (Vector3.Distance(_motor.Position, target.position) < distance)
                {
                    distance = Vector3.Distance(_motor.Position, target.position);
                    _positionVision = new EventPoint(target.position, target.gameObject.layer, Time.time);
                }
            }
            // print(gameObject.name + "\t" + sensor.Targets.Count);
        }
        if (_positionVision != null)
        {
            // // enable waypoints ? borked ?
            // EnableWaypoints();
            _isAlert = true;
            _sensorTimer = 0;
            // *testing instant aware
            if (_isAware)
                _awareTimer = _attentionTime;
            return true;
        }
        return false;
    }
    protected bool Sensor_Trigger()
    {
        if (!_isTrigger || !_isSensors)
            return false;
        float distance = float.MaxValue;
        foreach (sensor_trigger sensor in _sensorsTrigger)
        {
            foreach (Transform target in sensor.Targets)
            {
                if (!_data.Hostiles.Contains(target))
                    continue;
                // * testing exclude dead existing
                if (target.gameObject.layer == game_variables.Instance.LayerPlayer || target.gameObject.layer == game_variables.Instance.LayerMob)
                    if (target.GetComponent<entity_data>().HealthInst <= 0)
                        continue;
                // ? closest [memory waste ?]
                if (Vector3.Distance(_motor.Position, target.position) < distance)
                {
                    distance = Vector3.Distance(_motor.Position, target.position);
                    _positionTrigger = new EventPoint(target.position, target.gameObject.layer, Time.time);
                }
            }
            // print(sensor.Targets.Count);
        }
        if (_positionTrigger != null)
        {
            // // enable waypoints ? borked ?
            // EnableWaypoints();
            _isAlert = true;
            _sensorTimer = 0;
            // *testing instant aware
            _awareTimer = _attentionTime;
            return true;
        }
        return false;
    }
    // * testing [bell hurt] ? allow sensors to fire ?
    public void RegisterEvent(Vector2 position, LayerMask layer, float time)
    {
        // all sensors ?
        _positionVision = new EventPoint(position, layer, time);
        _positionTrigger = new EventPoint(position, layer, time);
        // // enable waypoints ? borked ?
        // EnableWaypoints();
        _isAlert = true;
        _sensorTimer = 0;
        print(gameObject.name + " " + position);
    }
    // * testing pacify ?pursue radius
    public void SetSensors(bool value)
    {
        // _isTrigger = false;
        // _isVision = false;
        _isSensors = value;
    }

    // - BT tasks here -
    // 
    // Audio<clip>
    // 
    // * BTDisable
    // 
    // * DoAttack int int int (?bool)
    // * DoFlag int int float
    // 
    // * EntityAtCamera
    // * EntityAtMove float
    // * EntityForceRandom float int (?bool)
    // * EntityForceToMove float
    // * EntitySpeed float
    // * EntityToMove float
    // ? EntityAtPlayer
    // ? EntityAtPlayer float
    // ? EntityForceRandom float float
    // ? EntityToSpawn float
    // EntitySpeedScaled float float
    // EntityAtTarget float
    // EntityAtAnchor float
    // EntityAtWaypoint
    // EquippedIs<>
    // 
    // HealthTickDown float
    // 
    // * IsAlert
    // * IsAlive
    // * IsAlone
    // * IsAware
    // * IsBored
    // * IsDirection int float
    // * IsEquipped
    // * IsFlag int
    // * IsFluid
    // * IsMoveEntity
    // * IsMoveEquipped
    // * IsMoveFluid
    // * IsMoveItem
    // * IsMoveWaypoint
    // * IsSensors
    // * IsTimer int
    // ? IsDirectionTrigger Vector3 float
    // ? IsBT
    // IsDetected
    // IsMoveWaypointValid
    // 
    // * MoveToOffsetRadius float
    // * MoveToTrigger
    // * MoveToVision
    // * MoveToWaypoint
    // MobDisable
    // 
    // OffsetMoveToTargetDirection float
    // OffsetMoveToRadialBand float float float float
    // 
    // * ScanVision float float float
    // * SensorAny
    // * SetColliders int
    // * SetDead
    // * SetFlag int int (?bool)
    // * SetSensors int
    // * SetSprite int
    // * SetTimer int float
    // * SetTrigger int int (?bool)
    // * SetTriggers int
    // * SetVisions int
    // * SetAttack int int
    // ? SetSprite int int
    // ? SetSprites int
    // ? SensorAudio
    // ? SetAlive
    // ? SetFlag int int
    // ? SetPositionToSpawn
    // ? SetTimerRandom int float float
    // ? SpritesEnable
    // SensorVision
    // SensorTrigger
    // SetMoveToSensor
    // SetMoveToAnchor
    // SetMoveToAnchorForward float
    // SetMoveToAnchorTick float
    // SetMoveToOmniForward float
    // SetMoveToOmniTick float float
    // SetMoveToWaypointNearby
    // SetMoveToTargetBack float
    // SelfDestruct
    // Spawn<prefab/mob/item/interact>
    // SpawnAtCamera
    // 
    // TickTimerA bool
    // TargetAtAnchor float
    // TestDamageHealth float
    // TargetIsPlayer
    // 
    // * UnsetAlert
    // UnsetDetected

    // [Task]
    // void BTDisable()
    // {
    //     _isBT = false;
    //     Task.current.Succeed();
    // }

    [Task]
    void DoAttack(int indexPrefab, int indexSpawn, int parent)
    {
        // * testing [? safety]
        _motor.NavigateCancel();
        _anim.SetTarget(_anchor.Position);
        GameObject temp = Instantiate(_prefabs[indexPrefab], _spawns[indexSpawn].position, _spawns[indexSpawn].rotation);
        temp.GetComponent<base_hitbox>().Initialize(transform);
        if (parent == 1)
            temp.transform.SetParent(_spawns[indexSpawn]);
        temp.SetActive(true);
        Task.current.Succeed();
    }
    // * testing [maggot ? conduit]
    [Task]
    void DoFlag(int indexFlag, int indexTimer, float time)
    {
        Task.current.debugInfo = _timers[indexTimer].ToString();
        // * testing [? set flag int]
        if (_timers[indexTimer] > 0)
            _flags[indexFlag] = true;
        else if (_flags[indexFlag])
        {
           Task.current.Succeed();
           return;
        }
        else
            _timers[indexTimer] = time;
        Task.current.Fail();
    }

    // [Task]
    // void EntityAtCamera()
    // {
    //     Task.current.Complete(game_camera.Instance.InView(_motor.Position));
    // }
    [Task]
    void EntityAtMove(float value)
    {
        // Task.current.debugInfo = Vector3.Distance(_motor.Position, _anchor.Position).ToString();
        // Task.current.Complete(Vector3.Distance(_motor.Position, _motor.Move) <= value);
        float distance = Vector3.Distance(_motor.Position, _anchor.Position);
        Task.current.debugInfo = distance.ToString();
        Task.current.Complete(distance <= value);
    }
    [Task]
    void EntitySpeed(float value)
    {
        _motor.Speed = value;
        if (value == 0 && _motor.Speed != 0)
            _motor.NavigateCancel();
        Task.current.Succeed();
    }
    [Task]
    // void EntityForceRandom(float min, float max)
    // void EntityForceRandom(float value, int target)
    void EntityForceRandom(float value)
    {
        // * testing [fly]
        _motor.NavigateCancel();
        Vector2 direction = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
        // if (target == 0)
        _anim.SetTarget(_anchor.Position);
        // else
        // _anim.SetTarget(_anchor.Position + (Vector3)direction);
        // * testing
        // _motor.AddForce(new Vector2(Random.Range(-1f, 1f) * Random.Range(min, max), Random.Range(-1f, 1f) * Random.Range(min, max)));
        _motor.AddForce(direction * value);
        Task.current.Succeed();
    }
    [Task]
    void EntityForceToMove(float value)
    {
        // * testing [turtle]
        _motor.NavigateCancel();
        _anim.SetTarget(_anchor.Position);
        // * testing
        _motor.AddForce((_anchor.Position - _motor.Position).normalized * value);
        Task.current.Succeed();
    }
    [Task]
    void EntityToMove()
    {
        Task.current.debugInfo = _anchor.Position.ToString();
        if (_timerPath <= 0 && _motor.Speed != 0)
        {
            // * testing asynchronous
            _timerPath = _timePath + Random.Range(-.2f, .2f);
            _motor.NavigateTo(_anchor.Position);
        }
        Task.current.Succeed();
    }

    [Task]
    bool IsAlert
    {
        get { return _isAlert; }
    }
    [Task]
    void IsAlive()
    {
        Task.current.debugInfo = _data.HealthInst.ToString();
        Task.current.Complete(_data.HealthInst > 0);
    }
    [Task]
    void IsAlone(float value)
    {
        // Task.current.debugInfo = _data.HealthInst.ToString();
        // Task.current.Complete(Physics2D.OverlapCircle(_motor.Position, 1f, game_variables.Instance.ScanLayerFluid) != null);
        // Task.current.Complete(_data.HealthInst > 0);
        // Collider2D[] temp = Physics2D.OverlapCircleAll(_motor.Position, value, game_variables.Instance.ScanLayerMob);
        // bool temp1 = false;
        // foreach(Collider2D other in temp)
        //     // * testing
        //     if (other.transform != _data.transform)
        //         temp1 = true;
        // Task.current.Complete(temp1);
        // more than itself
        Task.current.Complete(Physics2D.OverlapCircleAll(_motor.Position, value, game_variables.Instance.ScanLayerMob)?.Length > 1);
    }
    [Task]
    void IsAware()
    {
        Task.current.debugInfo = (_awareTimer / _attentionTime) + " : 0";
        Task.current.Complete(_isAware);
    }
    [Task]
    void IsBored()
    {
        Task.current.debugInfo = _sensorTimer + " : " + _attentionTime;
        Task.current.Complete(_sensorTimer == _attentionTime);
    }
    [Task]
    void IsDirection(float value)
    {
        // * testing [? safety]
        _motor.NavigateCancel();
        _anim.SetTarget(_anchor.Position);
        Task.current.Complete(_anim.IsDirection(_anchor.Position, value));
    }
    [Task]
    void IsEquipped()
    {
        Task.current.debugInfo = _data.Equipped?.gameObject.name;
        Task.current.Complete(_data.Equipped);
    }
    [Task]
    void IsFlag(int index)
    {
        // * testing [? check flag int default zero]
        if (index > -1 && index < _countFlags)
        {
            Task.current.debugInfo = _flags[index].ToString();
            Task.current.Complete(_flags[index]);
        }
        else
            Task.current.Fail();
    }
    [Task]
    void IsFluid()
    {
        // * testing [? cache result for frame ? radius as parameter]
        Task.current.Complete(Physics2D.OverlapCircle(_motor.Position, 1f, game_variables.Instance.ScanLayerFluid) != null);
    }
    [Task]
    void IsMoveEntity()
    {
        Task.current.debugInfo = _anchor.Position.ToString();
        // * testing [? check collision type, radius, hostiles only]
        Task.current.Complete(_data.Hostiles.Contains(Physics2D.OverlapCircle(_anchor.Position, .1f, game_variables.Instance.ScanLayerEntity)?.transform));
        // Task.current.Complete(_anchor.Layer == game_variables.Instance.LayerMob || _anchor.Layer == game_variables.Instance.LayerPlayer);
    }
    [Task]
    // void IsMoveEntityEquipped()
    void IsMoveEquipped()
    {
        Transform temp = Physics2D.OverlapCircle(_anchor.Position, .1f, game_variables.Instance.ScanLayerEntity)?.transform;
        Task.current.debugInfo = temp.gameObject.name;
        Task.current.Complete(_data.Hostiles.Contains(temp) && temp.GetComponent<entity_data>().Equipped);
    }
    [Task]
    void IsMoveFluid()
    {
        Task.current.Complete(Physics2D.OverlapCircle(_anchor.Position, 1f, game_variables.Instance.ScanLayerFluid) != null);
    }
    [Task]
    void IsMoveItem()
    {
        // ? check valid item
        Task.current.Complete(Physics2D.OverlapCircle(_anchor.Position, .1f, game_variables.Instance.ScanLayerEntity)?.gameObject.layer == game_variables.Instance.LayerItem);
    }
    [Task]
    void IsMoveWaypoint()
    {
        // Task.current.Complete(IsWaypoint(_motor.Move));
        // Task.current.Complete(IsWaypoint(_anchor.Position));
        Task.current.Complete(_waypoint ? _waypoint.IsWaypoint(_anchor.Position) : true);
    }
    [Task]
    bool IsSensors
    {
        get { return _isVision && _isTrigger; }
    }
    [Task]
    void IsTimer(int index)
    {
        if (index > -1 && index < _countTimers)
        {
            Task.current.debugInfo = _timers[index].ToString();
            Task.current.Complete(_timers[index] > 0f);
        }
        else
            Task.current.Succeed();
    }

    [Task]
    void MoveToOffsetRadius(float value)
    {
        // * testing [? retry attempts on invalid, chase bounds/limit]
        // _anchor.position += transform.right * Random.Range(-value, value) + transform.up * Random.Range(-value, value);
        // Vector3 direction = transform.right * Random.Range(-1f, 1f) + transform.up * Random.Range(-1f, 1f);
        // RaycastHit2D hit = Physics2D.Raycast(_anchor.Position, direction.normalized, value, game_variables.Instance.ScanLayerObstruction);
        // RaycastHit2D hit = Physics2D.Raycast(_motor.Position, direction.normalized, value, game_variables.Instance.ScanLayerObstruction);
        Vector2 offset = (transform.right * Random.Range(-1f, 1f) + transform.up * Random.Range(-1f, 1f)).normalized;
        RaycastHit2D hit = Physics2D.Raycast(_motor.Position, offset, value, game_variables.Instance.ScanLayerObstruction);
        EventPoint temp = new EventPoint();
        // temp.Position = hit ? grid_map.Instance.WorldToGrid(hit.point + hit.normal * .3f) : grid_map.Instance.WorldToGrid(_motor.Position + (Vector3)offset * value);
        // temp.Position = hit ? grid_map.Instance.WorldToGrid(hit.point + hit.normal * .5f) : grid_map.Instance.WorldToGrid(_motor.Position + (Vector3)offset * value);
        // if (Physics2D.OverlapCircle(temp.Position, .9f, game_variables.Instance.ScanLayerObstruction) != null)
        //     temp.Position = grid_map.Instance.WorldToGrid(_motor.Position);
        // temp.Position = hit ? grid_map.Instance.WorldToGrid(_motor.Position) : grid_map.Instance.WorldToGrid(_motor.Position + (Vector3)offset * value);
        temp.Position = hit ? grid_map.Instance.WorldToGrid(hit.point + hit.normal * .5f) : grid_map.Instance.WorldToGrid(_motor.Position);
        // safety ?
        if (Physics2D.OverlapCircle(temp.Position, .9f, game_variables.Instance.ScanLayerObstruction) != null)
            // temp.Position = grid_map.Instance.WorldToGrid(_motor.Position);
            temp.Position = GetWaypoint();
        temp.Layer = _anchor.Layer;
        temp.Time = _anchor.Time;
        _anchor = temp;
        // else
        //     // temp.Position = _anchor.Position + direction;
        //     // temp.Position = _motor.Position + direction.normalized * value;
        //     temp.Position = _anchor.Position;
        // // ? locking
        // while (hit)
        // {
        //     direction = transform.right * Random.Range(-value, value) + transform.up * Random.Range(-value, value);
        //     hit = Physics2D.Raycast(_anchor.Position, direction.normalized, value, game_variables.Instance.ScanLayerObstruction);
        // }
        // temp.Position = _anchor.Position + direction;
        Task.current.Succeed();
    }
    [Task]
    void MoveToTrigger()
    {
        if (_positionTrigger == null)
            Task.current.Fail();
        else
        {
            Task.current.debugInfo = _positionTrigger.Position.ToString();
            // * testing [? chase bounds/limit]
            // _anchor.position = _positionTrigger.Position;
            _anchor = _positionTrigger;
            _positionTrigger = null;
            Task.current.Succeed();
        }
    }
    [Task]
    void MoveToVision()
    {
        if (_positionVision == null)
            Task.current.Fail();
        else
        {
            Task.current.debugInfo = _positionVision.Position.ToString();
            // * testing [? chase bounds/limit]
            // _anchor.position = _positionVision.Position;
            _anchor = _positionVision;
            _positionVision = null;
            Task.current.Succeed();
        }
    }
    [Task]
    void MoveToWaypoint()
    {
        // _anchor.position = GetWaypointRandom();
        EventPoint temp = new EventPoint();
        // temp.Position = GetWaypointRandom();
        temp.Position = GetWaypoint();
        temp.Layer = _anchor.Layer;
        temp.Time = _anchor.Time;
        _anchor = temp;
        // _anchor.position = GetWaypointNearby();
        Task.current.Succeed();
    }
    [Task]
    void MoveToWaypointOffset()
    {
        // _anchor.position = GetWaypointRandom();
        EventPoint temp = new EventPoint();
        // temp.Position = GetWaypointRandom();
        temp.Position = GetWaypointOffset();
        temp.Layer = _anchor.Layer;
        temp.Time = _anchor.Time;
        _anchor = temp;
        // _anchor.position = GetWaypointNearby();
        Task.current.Succeed();
    }

    // * testing [? use entity timer]
    protected float _scanTimer = 0f;
    // protected float[] _scanOffset;
    protected float _scanOffset;
    // * testing [? sensor interrupt, animation rotation offset]
    // step []
    [Task]
    void ScanVision(float duration, float step, float angle)
    {
        // // * testing [? specific sensor indices ? inefficient ? safety]
        // if (_scanTimer > 0)
        // {
        //     for (int i = _scanOffset.Length - 1; i > -1; i--)
        //     {
        //         float rotation = angle * Mathf.Sin(((_scanTimer % step) / step) * Mathf.PI * 2f);
        //         _sensorsVision[i].Rotation = _scanOffset[i] + rotation;
        //         _anim.Offset = rotation;
        //     }
        //     _scanTimer -= Time.deltaTime;
        //     if (_scanTimer <= 0)
        //     {
        //         for (int i = _scanOffset.Length - 1; i > -1; i--)
        //         {
        //             _sensorsVision[i].Rotation =_scanOffset[i];
        //         }
        //         Task.current.Succeed();
        //     }
        // }
        // else
        // {
        //     // * testing [? memory waste]
        //     _scanTimer = duration;
        //     _scanOffset = new float[_sensorsVision.Count];
        //     for (int i = _scanOffset.Length - 1; i > -1; i--)
        //         _scanOffset[i] = _sensorsVision[i].Rotation;
        // }
        // * testing
        _motor.NavigateCancel();
        Task.current.debugInfo = _scanTimer.ToString();
        if (_scanTimer > 0)
        {
            _anim.SetRotation(_scanOffset + angle * Mathf.Sin(((_scanTimer % step) / step) * Mathf.PI * 2f));
            _scanTimer -= Time.deltaTime;
            if (_scanTimer <= 0)
            {
                _anim.SetRotation(_scanOffset);
                Task.current.Succeed();
                return;
            }
        }
        else
        {
            // * testing [? memory waste]
            _scanTimer = duration;
            _scanOffset = 90f + _anim.Rotation;
        }
        Task.current.Fail();
    }
    [Task]
    void SensorAny()
    {
        Task.current.debugInfo = "S:" + _isSensors + " V:" + _isVision + " T:" + _isTrigger;
        // check sensors [priority]
        Task.current.Complete(Sensor_Trigger() || Sensor_Vision());
    }
    [Task]
    void SetAttack(int index, int value)
    {
        if (_prefabs[index].activeSelf != (value == 1))
        {
            _prefabs[index].GetComponent<base_hitbox>().Initialize(transform);
            _prefabs[index].SetActive(value == 1);
        }
        Task.current.Succeed();
    }
    [Task]
    void SetColliders(int value)
    {
        // foreach (Collider2D collider in _colliders)
        //     if (!collider.enabled)
        //         collider.enabled = true;
        _motor.CollidersToggle(value == 1);
        Task.current.Succeed();
    }
    [Task]
    void SetDead()
    {
        _data.HealthDrain(_data.Health);
        Task.current.Succeed();
    }
    [Task]
    // void SetFlag(int index, bool value)
    void SetFlag(int index, int value)
    {
        if (index > -1 && index < _countFlags)
        {
            _flags[index] = value == 1;
            Task.current.Succeed();
        }
        else
            Task.current.Fail();
    }
    [Task]
    void SetSensors(int value)
    {
        _isVision = value == 1;
        _isTrigger = value == 1;
        Task.current.Succeed();
    }
    [Task]
    void SetSprite(int value)
    {
        _anim.SetSprite(value);
        Task.current.Succeed();
    }
    [Task]
    void SetTimer(int index, float value)
    {
        if (index > -1 && index < _countTimers)
        {
            _timers[index] = value;
            Task.current.Succeed();
        }
        else
            Task.current.Fail();
    }
    [Task]
    void SetTimerRandom(int index, float min, float max)
    {
        if (index > -1 && index < _countTimers)
        {
            // * testing
            _timers[index] = Random.Range(min, max);
            Task.current.Succeed();
        }
        else
            Task.current.Fail();
    }
    [Task]
    void SetTrigger(int index, int value)
    {
        // * testing [? safety]
        _sensorsTrigger[index].SetActive(value == 1);
        Task.current.Succeed();
    }
    [Task]
    void SetTriggers(int value)
    {
        _isTrigger = value == 1;
        Task.current.Succeed();
    }
    [Task]
    void SetVisions(int value)
    {
        _isVision = value == 1;
        Task.current.Succeed();
    }
    // [Task]
    // void SetPositionToSpawn()
    // {
    //     _motor.ToSpawn();
    //     Task.current.Succeed();
    // }
    // [Task]
    // void SpritesEnable()
    // {
    //     _anim.SpritesToggle(true);
    //     Task.current.Succeed();
    // }
    // [Task]
    // void SetAlive()
    // {
    //     _data.HealthRestore();
    //     Task.current.Succeed();
    // }

    [Task]
    void UnsetAlert()
    {
        _isAlert = false;
        Task.current.Succeed();
    }
}
