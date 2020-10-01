using UnityEngine;
// door light conduit prism ?tread ?piston ?floor
public class react_toggle : base_react
{
    [SerializeField] protected GameObject _target;
    [Tooltip("Delay till active")] [SerializeField] protected float _timeHold = 0f;
    protected float _timerHold;
    public override void Load()
    {
        base.Load();
        _timerHold = 0f;
        if (_target.activeSelf != _active)
            _target.SetActive(_active);
    }
    protected override void Update()
    {
        base.Update();
        // * testing
        if (_target.activeSelf != _active)
        {
            if (_timerHold < _timeHold)
                _timerHold += Time.deltaTime;
            else
                _target.SetActive(_active);
        }
        else
            _timerHold = 0f;
    }
}
