using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class dev_invisible : MonoBehaviour, IPointerDownHandler
{
    private Image _sprite;
    // private bool _active;
    void Awake()
    {
        _sprite = GetComponent<Image>();
        // _active = false;
    }
    void Update()
    {
        _sprite.enabled = game_master.Instance.IsDeveloper;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // if (!game_master.Instance.IsDeveloper)
        //     return;
        // _active = !_active;
        controller_player.Instance.Data.ToggleInvisible();
        _sprite.color = controller_player.Instance.Data.ModeInvisible ? Color.white : Color.green;
    }
}
