using UnityEngine;
using UnityEngine.EventSystems;

public class button_ad : MonoBehaviour, IPointerDownHandler
{
    [Range(1,3)] public int _amount = 1;
    public void OnPointerDown(PointerEventData eventData)
    {
        // * testing ? coroutine callback
        game_master.Instance.LifeRestore(_amount);
    }
}
