using UnityEngine;
public class game_camera : MonoBehaviour
{
    public static game_camera Instance;
    private Camera _cam;
    // public Vector2 _boundsX;
    // public Vector2 _boundsY;
    // private Vector3 _anchor;
    // private bool _flag;
    public Vector2 _view = new Vector2(1f, 1f);
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _cam = GetComponent<Camera>();
        // _anchor = transform.position;
        // _flag = false;
    }
    void Update()
    {
        // only when alive 
        if (controller_player.Instance.Data.HealthInst > 0)
            SnapToPosition(controller_player.Instance.Motor.Position);
        // {
            // SetPosition();
            // SetAnchor();
        // }
    }
    // private void SetAnchor()
    // {
    //     // 
    //     if (input_touch.Instance.EventDrag)
    //         _flag = false;
    //     else
    //     {
    //         if (!_flag)
    //         {
    //             _anchor = transform.position;
    //             _flag = true;
    //         }
    //     }
    // }
    // private void SetPosition()
    // {
    //     // pan
    //     if (input_touch.Instance.EventDrag)
    //         transform.position = _anchor + new Vector3(input_touch.Instance.CacheDrag.x, input_touch.Instance.CacheDrag.y);
    //     // bounds
    //     transform.position = new Vector3(Mathf.Clamp(transform.position.x, _boundsX[0], _boundsX[1]), Mathf.Clamp(transform.position.y, _boundsY[0], _boundsY[1]), transform.position.z);
    // }
    public void SnapToPosition(Vector3 position)
    {
        // _anchor = new Vector3(position.x, position.y, -1f);
        // transform.position = _anchor;
        transform.position = new Vector3(position.x, position.y, -1f);
    }
    // public bool InView(Vector3 position)
    // {
    //     position = _cam.WorldToViewportPoint(position);
    //     return position.x > 1f - _view.x && position.x < _view.x && position.y > 1f - _view.y && position.y < _view.y;
    // }
    public Camera CameraMain
    {
        get { return _cam; }
    }
    public Vector3 Position
    {
        get { return _cam.transform.position; }
    }
}
