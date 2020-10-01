using UnityEngine;
using UnityEngine.EventSystems;
public class button_settings : base_button
{
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
        if (_state != 0)
        {
            if (_timer > 0)
                manager_ui.Instance.ToggleSettings();
            else
                game_master.Instance.ToggleDeveloper();
        }
        base.OnPointerUp(eventData);
    }
}
