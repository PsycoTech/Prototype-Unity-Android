using UnityEngine;
using UnityEngine.EventSystems;
public class button_pouch : base_button
{
    protected int _id;
    private float _time = 1f;
    private float _timer;
    protected override void Awake()
    {
        base.Awake();
        _timer = 0;
    }
    protected override void Update()
    {
        base.Update();
        if (_timer > 0f)
            _timer -= Time.deltaTime;
        // {
        //     if (_timer <= 0)
        //         game_master.Instance.SetDeveloper(true);
        // }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        _timer = _time;
        base.OnPointerDown(eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        _timer = 0;
        base.OnDrag(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (_state == 0)
            return;
        if (_timer <= 0)
            menu_inventory.Instance.Drop(_id);
        else if (menu_inventory.Instance.IsEquipped(_id))
            controller_player.Instance.Data.Holster();
        else
            menu_inventory.Instance.SetEquipped(_id);
        base.OnPointerUp(eventData);
    }
    public int ID
    {
        set { _id = value; }
    }
}
