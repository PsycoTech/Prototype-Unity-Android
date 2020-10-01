using UnityEngine;
using UnityEngine.EventSystems;
public class input_touch : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static input_touch Instance;    
    public GameObject _cursorTap;
    public GameObject _cursorDragLeft;
    public GameObject _cursorDragTrackLeft;
    public GameObject _cursorDragRight;
    public GameObject _cursorDragTrackRight;
    private Vector2 _pointerTap;
    private Vector2 _pointerDragLeft;
    private Vector2 _pointerDragRight;
    private bool _eventTap;
    // private bool _eventTapDouble;
    private bool _eventDragLeft;
    private bool _eventDragRight;
    // private Vector2 _cacheTap;
    private Vector3 _cacheTapWorld;
    private Vector2 _cacheDragLeft;
    private Vector2 _cacheDragRight;
    private bool _hasCycled;
    // public float _timeTapDouble = 0.2f;
    // private float _timerTapDouble;
    // protected float _timerInput = .1f;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        // 
    }
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        _hasCycled = true;
        // 
        _pointerTap = Vector2.zero;
        _pointerDragLeft = Vector2.zero;
        _pointerDragRight = Vector2.zero;
        // _cacheTap = Vector2.zero;
        _cacheTapWorld = Vector3.zero;
        _cacheDragLeft = Vector2.zero;
        _cacheDragRight = Vector2.zero;
        // _timeTapDouble = game_variables.Instance._timeTap;
        // _timerTapDouble = 0f;
        ClearEvents();
    }
    private void ClearEvents()
    {
        if (_hasCycled)
        {
            _eventTap = false;
            // _eventTapDouble = false;
            // _eventDrag = false;
        }
        else
            // (at least?) one update cycle since false
            _hasCycled = true;
    }
    void Update()
    {
        // 
        ClearEvents();
        // 
        _cursorTap.SetActive(_eventTap);
        _cursorDragLeft.SetActive(_eventDragLeft);
        _cursorDragRight.SetActive(_eventDragRight);
        // // tick
        // if (_timerTapDouble > 0f)
        //     _timerTapDouble -= Time.deltaTime;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // tap new
        if (_pointerTap == Vector2.zero)
            _pointerTap = eventData.pressPosition;
        // drag new
        if (_pointerDragLeft == Vector2.zero && eventData.pressPosition.x < Screen.width / 2)
            _pointerDragLeft = eventData.pressPosition;
        if (_pointerDragRight == Vector2.zero && eventData.pressPosition.x > Screen.width / 2)
            _pointerDragRight = eventData.pressPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        // tap clear
        if (eventData.pressPosition == _pointerTap)
            _pointerTap = Vector2.zero;
        // drag update
        if (eventData.pressPosition == _pointerDragLeft)
        {
            _eventDragLeft = true;
            _cacheDragLeft = eventData.position - eventData.pressPosition;
            _cursorDragLeft.transform.position = eventData.pressPosition;
            _cursorDragTrackLeft.transform.position = eventData.position;
            _hasCycled = false;
        }
        // drag update
        if (eventData.pressPosition == _pointerDragRight)
        {
            _eventDragRight = true;
            _cacheDragRight = eventData.position - eventData.pressPosition;
            _cursorDragRight.transform.position = eventData.pressPosition;
            _cursorDragTrackRight.transform.position = eventData.position;
            _hasCycled = false;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        // tap update
        if (eventData.pressPosition == _pointerTap)
        {
            _pointerTap = Vector2.zero;
            _eventTap = true;
            // if (_timerTapDouble > 0f)
            // {
            //     _eventTapDouble = true;
            //     // reset
            //     _timerTapDouble = 0f;
            // }
            // else
            //     // set
            //     _timerTapDouble = _timeTapDouble;
            // _cacheTap = eventData.pressPosition;
            // _cacheTapWorld = game_camera.Instance.CameraMain.ScreenToWorldPoint(_cacheTap) + Vector3.forward;
            _cacheTapWorld = game_camera.Instance.CameraMain.ScreenToWorldPoint(eventData.pressPosition) + Vector3.forward;
            _cursorTap.transform.position = eventData.pressPosition;
            _hasCycled = false;
        }
        // drag clear
        if (eventData.pressPosition == _pointerDragLeft)
        {
            _pointerDragLeft = Vector2.zero;
            _eventDragLeft = false;
        }
        if (eventData.pressPosition == _pointerDragRight)
        {
            _pointerDragRight = Vector2.zero;
            _eventDragRight = false;
        }
    }
    public bool EventTap(bool clear = true)
    {
        if (!_eventTap)
            return false;
        bool temp = _eventTap;
        if (clear)
            _eventTap = false;
        return temp;
    }
    // unit square
    public Vector2 CacheCamera()
    {
        Vector2 temp = (game_variables.Instance.Chirality ? _cacheDragLeft : _cacheDragRight) / game_variables.Instance.SensitivityCamera * game_variables.Instance.Camera;
        // _cacheDrag = Vector2.zero;
        // ?
        temp = temp.magnitude > 1f ? temp.normalized : temp;
        return temp;
    }
    public Vector2 CacheMotor()
    {
        Vector2 temp = (game_variables.Instance.Chirality ? _cacheDragRight : _cacheDragLeft) / game_variables.Instance.SensitivityMotor * game_variables.Instance.Motor;
        // _cacheDrag = Vector2.zero;
        // ?
        temp = temp.magnitude > 1f ? temp.normalized : temp;
        return temp;
    }
    // public bool EventTapDouble()
    // {
    //     if (!_eventTapDouble)
    //         return false;
    //     bool temp = _eventTapDouble;
    //     _eventTapDouble = false;
    //     return temp;
    // }
    #region Properties
    // public bool EventTap
    // {
    //     get { return _eventTap; }
    // }
    // public bool EventTapDouble
    // {
    //     get { return _eventTapDouble; }
    // }
    public bool EventCamera
    {
        get { return game_variables.Instance.Chirality ? _eventDragLeft : _eventDragRight; }
    }
    public bool EventMotor
    {
        get { return game_variables.Instance.Chirality ? _eventDragRight : _eventDragLeft; }
    }
    // public Vector2 CacheTap
    // {
    //     get { return _cacheTap; }
    // }
    public Vector3 CacheTapWorld
    {
        get { return _cacheTapWorld; }
    }
    // unit square
    // public Vector2 CacheDrag
    // {
    //     get { return -_cacheDrag / game_variables.Instance.SensitivityCamera * game_variables.Instance.Camera; }
    // }
    // public bool IsDrag
    // {
    //     get { return _pointerDrag.sqrMagnitude > 0f; }
    // }
    #endregion
}
