using UnityEngine;
// conduit
public class react_react : base_react
{
    [Tooltip("Reference to react object")] [SerializeField] protected base_react _react = null;
    [Tooltip("Target signal")] [SerializeField] protected int _id = 0;
    [Tooltip("Ping state value: 0 - false | 1 - true | 2 - either")] [SerializeField] protected int _condition = 2;
    [Tooltip("Delay till active")] [SerializeField] protected float _timeHold = 0f;
    protected bool _check;
    protected float _timerHold;
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
            if (_react && (_condition == 0 && !_active) || (_condition == 1 && _active) || _condition == 2)
                _react.Ping(_id, _active);
        }
        else
            _timerHold = 0f;
    }
}
