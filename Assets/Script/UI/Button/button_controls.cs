using UnityEngine.EventSystems;
public class button_controls : base_button
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (_state != 0)
            manager_ui.Instance.ToggleControls();
        base.OnPointerUp(eventData);
    }
}
