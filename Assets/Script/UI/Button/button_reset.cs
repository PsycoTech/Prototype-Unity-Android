using UnityEngine.EventSystems;
public class button_reset : base_button
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (_state != 0)
            game_variables.Instance.Load();
        base.OnPointerUp(eventData);
    }
}
