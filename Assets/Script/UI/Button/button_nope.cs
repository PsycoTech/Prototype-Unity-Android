using UnityEngine;
using UnityEngine.EventSystems;
public class button_nope : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        manager_ui.Instance.ToggleContinue();
    }
}
