using UnityEngine;
using UnityEngine.EventSystems;
public class button_compass : MonoBehaviour, IPointerDownHandler
{
    private GameObject _pulse;
    private Transform _direction;
    void Awake()
    {
        _pulse = transform.GetChild(0).gameObject;
        _direction = transform.GetChild(1);
    }
    void Update()
    {
        if (_pulse.activeSelf != controller_player.Instance.Motor.IsMove)
            _pulse.SetActive(controller_player.Instance.Motor.IsMove);
        // Vector2 direction = (Vector2)(controller_player.Instance.Motor.Position - game_camera.Instance.Position);
        Vector2 direction = (Vector2)(controller_player.Instance.Motor.Position - game_camera.Instance.CameraMain.ScreenToWorldPoint(transform.position));
        _direction.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        game_camera.Instance.SnapToPosition(controller_player.Instance.Motor.Position);
    }
}
