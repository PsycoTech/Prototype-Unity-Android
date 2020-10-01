using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class dev_teleport : MonoBehaviour, IPointerDownHandler
{
    private Image _sprite;
    void Awake()
    {
        _sprite = GetComponent<Image>();
    }
    void Update()
    {
        _sprite.enabled = game_master.Instance.IsDeveloper;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // if (!game_master.Instance.IsDeveloper)
        //     return;
        controller_player.Instance.Motor.ToPosition(new Vector3(game_camera.Instance.Position.x, game_camera.Instance.Position.y, 0f));
    }
}
