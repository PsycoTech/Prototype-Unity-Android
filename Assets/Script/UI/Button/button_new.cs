using UnityEngine.EventSystems;
public class button_new : base_button
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (_state != 0)
            game_master.Instance.New();
        base.OnPointerUp(eventData);
    }
}
