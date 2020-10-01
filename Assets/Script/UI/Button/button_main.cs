using UnityEngine;
using UnityEngine.EventSystems;
public class button_main : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        manager_ui.Instance.SetMain(true);
    }
}
