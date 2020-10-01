using UnityEngine;
// fruit
public class item_detonate : base_item
{
    [Tooltip("Instantiate on destroy")] [SerializeField] protected GameObject _drop = null;
    // [Tooltip("Delay till destroy")] [SerializeField] protected float _time = 0f;
    protected float _time = 3f;
    protected float _timer;
    public override void New()
    {
        base.New();
        _timer = -1f;
    }
    public override void Save()
    {
        base.Save();
        _state.Timer = _timer;
    }
    public override void Load()
    {
        base.Load();
        _timer = _state.Timer;
    }
    void Update()
    {
        if (_timer == -1)
            return;
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            if (_drop)
                Instantiate(_drop, transform.position, transform.rotation);
            base.Destroy();
        }
    }
    public override void Destroy()
    {
        if (_timer == -1)
        {
            _timer = _time;
            feedback_popup.Instance.RegisterMessage(transform, "3... 2... 1...", game_variables.Instance.ColorDefault);
        }
    }
}
