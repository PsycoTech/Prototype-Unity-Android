using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class button_continue : MonoBehaviour, IPointerDownHandler
{
    private Text _text;
    void Awake()
    {
        _text = transform.GetChild(0).GetComponent<Text>();
    }
    void Update()
    {
        _text.text = controller_player.Instance.Data.HealthInst > 0 ? "CONTINUE" : "CONTINUE?";
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (controller_player.Instance.Data.HealthInst > 0)
            manager_ui.Instance.SetMain(false);
        else
            manager_ui.Instance.ToggleContinue();
    }
}
