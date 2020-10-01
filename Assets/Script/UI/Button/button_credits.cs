using UnityEngine;
using UnityEngine.EventSystems;
public class button_credits : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        manager_ui.Instance.ToggleCredits();
    }
}
