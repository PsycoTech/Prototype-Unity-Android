using UnityEngine.EventSystems;
public class button_save : base_button
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (_state != 0)
            game_master.Instance.Save();
        base.OnPointerUp(eventData);
    }
}
