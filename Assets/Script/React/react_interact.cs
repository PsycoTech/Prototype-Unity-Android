using UnityEngine;
// altar
public class react_interact : base_react
{
    [SerializeField] protected interact_direction _interact = null;
    [Tooltip("Delay till active")] [SerializeField] protected float _timeHold = 0f;
    protected float _timerHold;
    protected bool _check;
    public override void Load()
    {
        base.Load();
        _check = _active;
        _timerHold = 0f;
    }
    protected override void Update()
    {
        base.Update();
        if (_check != _active)
        {
            if (_timerHold < _timeHold)
                _timerHold += Time.deltaTime;
            else
                _check = _active;
            if (_interact)
                _interact.SetFlag(_active);
        }
        else
            _timerHold = 0f;
    }
}
