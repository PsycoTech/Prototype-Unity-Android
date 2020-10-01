using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class entity_motor : MonoBehaviour
{
    protected entity_data _data;
    protected entity_anim _anim;
    protected Rigidbody2D _rb;
    protected Vector3[] _path;
    protected int _targetIndex;
    protected Vector3 _spawn;
    protected Vector3 _move;
    // 
    [SerializeField] protected float _speed = 2f;
    [SerializeField] protected float _drag = .3f;
    [SerializeField] protected float _terminal = 10f;
    // 
    protected Vector2 _cache;
    // protected List<float> _modifiersDrag;
    // protected List<float> _modifiersSpeed;
    protected float _modifierDrag;
    protected float _modifierSpeed;
    protected List<Vector2> _active;
    // * testing
    public Transform _anchor;
    // * testing [? composite multiple colliders]
    [Tooltip("Toggle (mob only)")] [SerializeField] protected List<Collider2D> _colliders;
    // protected Vector2 _direction;
    // [Tooltip("Navigation path display")] [SerializeField] protected feedback_move _feedbackMove = null;
    protected state_motor _state;
    void Awake()
    {
        _data = GetComponent<entity_data>();
        _anim = transform.GetChild(0).GetComponent<entity_anim>();
        _rb = GetComponent<Rigidbody2D>();
        // 
        _path = null;
        _targetIndex = 0;
        _spawn = Position;
        _move = _spawn;
        // 
        _cache = Vector2.zero;
        // _modifiersDrag = new List<float>();
        // _modifiersSpeed = new List<float>();
        _modifierDrag = 1f;
        _modifierSpeed = 1f;
        _active = new List<Vector2>();
        // _direction = Vector2.zero;
        // _anchor = Instantiate(new GameObject(), transform.position + _offset, transform.rotation);
    }
    // void OnDrawGizmos()
    // {
    //     if (IsMove)
    //         for (int i = _targetIndex; i < _path.Length; i++)
    //         {
    //             Gizmos.color = Color.black;
    //             Gizmos.DrawCube(_path[i], Vector3.one * 0.9f);
    //             if (i == _targetIndex)
    //             {
    //                 Gizmos.DrawLine(Position, _path[i]);
    //             }
    //             else
    //             {
    //                 Gizmos.DrawLine(_path[i - 1], _path[i]);
    //             }
    //         }
    // }
    void Update()
    {
        if (IsMove)
        {
            _anim.SetTarget(_move);
            // // * testing
            // if (_feedbackMove)
            // {
            //     Vector3[] temp = new Vector3[_path.Length - _targetIndex];
            //     for (int i = 0; i < temp.Length; i++)
            //         temp[i] = _path[i + _targetIndex];
            //     _feedbackMove.SetPath(temp, _data.ActionColor());
            // }
        }
        // // * testing
        // else if (_feedbackMove)
        //     _feedbackMove.Clear();
        // // * testing corpse ignore
        // if (gameObject.layer == game_variables.Instance.LayerPlayer)
        //     foreach (Collider2D collider in _colliders)
        //         collider.isTrigger = _data?.HealthInst == 0;
    }
    void FixedUpdate()
    {
        // if ()
        // {
        //     // * testing drag move
        //     _rb.MovePosition(_rb.position + input_touch.Instance.CacheDrag() * Time.fixedDeltaTime);
        // }
        // else
        // {
        // input
        // - position > direction
        Vector2 direction = gameObject.layer == game_variables.Instance.LayerPlayer && input_touch.Instance.EventMotor ? input_touch.Instance.CacheMotor() : IsMove ? (Vector2)(_move - Position).normalized : Vector2.zero;
        // soft halt ?
        // direction = direction.magnitude > 1f ? direction.normalized : direction;
        // - scale * testing
        // float modifier = 1f;
        // for (int i = _modifiersSpeed.Count - 1; i > -1; i--)
        //     modifier *= _modifiersSpeed[i];
        // // _input *= _speed * (modifier > 0f ? modifier : 1f) * (_data.Armours.Count > 0 ? _data.Armour.OffsetSpeed : 1f);
        // direction *= _speed * modifier;
        // _modifiersSpeed.Clear();
        direction *= _speed * _modifierSpeed;
        _modifierSpeed = 1f;
        // active
        foreach (Vector2 force in _active)
            _cache += force;
        _active.Clear();
        // drag modifiersDrag [counter force] * testing
        // modifier = 1f;
        // for (int i = _modifiersDrag.Count - 1; i > -1; i--)
        //     modifier *= _modifiersDrag[i];
        // // float drag = _drag * (modifier > 0f ? modifier : 1f) * (_data.Armours.Count > 0 ? _data.Armour.OffsetDrag : 1f);
        // float drag = _drag * modifier;
        // _modifiersDrag.Clear();
        float drag = _drag * _modifierDrag;
        _modifierDrag = 1f;
        // - decelerate
        _cache -= _cache.normalized * drag;
        // - halt
        if (_cache.magnitude < drag)
            _cache = Vector2.zero;
        // terminal ?
        _cache = _cache.magnitude > _terminal ? _cache.normalized * _terminal : _cache;
        
        // rigidbody
        // - mass * testing
        // Debug.Log(base_player.Instance.Armour._offsetMass);
        // _rb.mass = _mass + base_player.Instance.Armour._offsetMass;
        // - drag ?
        // float drag = _drag + base_player.Instance.Armour._offsetDrag;
        // foreach (float value in _dragModifiers)
        //     drag += value;
        // _rb.drag = drag;
        // - position
        // Debug.Log("motor\t" + Time.fixedDeltaTime + "\t" + _input + "\t" + motion);
        // _rb.MovePosition(_rb.position + (_input + _cache) * Time.fixedDeltaTime);
        // _rb.MovePosition(_rb.position + (_cache + (IsMove ? direction : Vector2.zero)) * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + (_cache + direction) * Time.fixedDeltaTime);
        // }
    }
    public void New()
    {
        // 
        _path = null;
        _targetIndex = 0;
        // 
        _cache = Vector2.zero;
        _modifierDrag = 1f;
        _modifierSpeed = 1f;
        _active.Clear();
        // 
        NavigateCancel();
        transform.position = _spawn;
        _move = transform.position;
        // 
        _state = new state_motor();
    }
    public void Save()
    {
        // 
        _state.Position.SetVector(Position);
    }
    public void Load()
    {
        transform.position = _state.Position.GetVector();
    }
    public void AddForce(Vector2 force)
    {
        if (force.sqrMagnitude > 0)
            _active.Add(force);
    }
    public void SetModifierDrag(float value)
    {
        // if (value > 0)
        //     _modifiersDrag.Add(value);
        if (value > 0 && Mathf.Abs(1f - value) > Mathf.Abs(1f - _modifierDrag))
            _modifierDrag = value;
    }
    public void SetModifierSpeed(float value)
    {
        // if (value > 0)
        //     _modifiersSpeed.Add(value);
        if (value > 0 && Mathf.Abs(1f - value) > Mathf.Abs(1f - _modifierSpeed))
            _modifierSpeed = value;
    }
    // * testing
    public void NavigateCancel()
    {
        StopCoroutine("PathFollow");
        // _move = _rb.position;
        _path = null;
        _targetIndex = 0;
        // if (gameObject.layer == game_variables.Instance.LayerPlayer)
        //     feedback_move.Instance.gameObject.SetActive(false);
    }
    public void NavigateTo(Vector3 target)
    {
        // if (Position != target)
        // - pathfind
        // map_navigation.PathRequest(Position, target, OnPathFound);
        // map_navigation.PathRequest(new PathData(Position, target, OnPathFound));
        // grid_navigation.Instance.CalculatePath(Position, target);
        // grid_navigation.PathRequest(new PathData(Position, target, OnPathFound));
        // if (grid_map.Instance.IsWalkable(target) && grid_map.Instance.WorldToIndex(target) != grid_map.Instance.WorldToIndex(Position))
        if (grid_map.Instance.WorldToIndex(target) != grid_map.Instance.WorldToIndex(Position))
            grid_navigation.Instance.PathCalculate(new PathData(Position, target, OnPathFound));
    }
    public void OnPathFound(Vector3[] path, bool success)
    {
        // // * testing feedback navigation fail X success O
        // if (gameObject.layer == game_variables.Instance.LayerPlayer)
        //     feedback_navigation.Instance.SetPosition(input_touch.Instance.CacheTapWorld, success);
        if (success)
        {
            // if (path != null && path.Length > 0)
            // {
            _path = path;
            _targetIndex = 0;
            StopCoroutine("PathFollow");
            if (gameObject.activeSelf)
                StartCoroutine("PathFollow");
            // }
            // else
            //     _path = null;
        }
        // // * testing ?
        // else
        // {
        //     print("COCK");
        //     NavigateCancel();
        // }
    }
    IEnumerator PathFollow()
    {
        if (_path.Length > 0)
            _move = _path[0];
        while (true)
        {
            if (Vector3.Distance(Position, _move) < 0.1f)
            {
                _targetIndex++;
                if (_path == null)
                {
                    _targetIndex = 0;
                    yield break;
                }
                else if (_targetIndex >= _path.Length)
                {
                    _targetIndex = 0;
                    _path = null;
                    yield break;
                }
                _move = _path[_targetIndex];
            }
            yield return null;
        }
    }
    // // public void ToSpawn(float radius = 0f)
    // public void ToSpawn()
    // {
    //     NavigateCancel();
    //     transform.position = _spawn;
    //     _move = transform.position;
    //     // // * testing [? direction]
    //     // if (radius > 0)
    //     //     transform.position += new Vector3(Random.Range(-1f, 1f) * radius, Random.Range(-1f, 1f) * radius);
    // }
    // * testing
    public void CollidersToggle(bool value)
    {
        foreach (Collider2D collider in _colliders)
            if (collider.enabled != value)
                collider.enabled = value;
    }
    public void ToPosition(Vector3 position)
    {
        NavigateCancel();
        transform.position = position;
    }
    public Vector3 Position
    {
        get { return _rb.position; }
    }
    public Vector3 Spawn
    {
        get { return _spawn; }
    }
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public Vector3 Move
    {
        get { return _move; }
    }
    public bool IsMove
    {
        // get { return _direction.magnitude > 0; }
        // get { return _rb.velocity.magnitude > 0; }
        // get { return false; }
        get { return _path != null; }
    }
}
