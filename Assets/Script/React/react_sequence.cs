using UnityEngine;
using System.Collections.Generic;
// wall
public class react_sequence : base_react
{
    [SerializeField] protected GameObject _target;
    [SerializeField] protected List<interact_react> _interacts = null;
    protected int _next;    //implicit sequence
    public override void Load()
    {
        base.Load();
        _next = 0;
    }
    protected override void Update()
    {
        base.Update();
        // * testing
        if (_target.activeSelf != _active)
            _target.SetActive(_active);
    }
    public override void Ping(int id, bool value)
    {
        if (id == _next)
        {
            _next++;
            base.Ping(id, value);
            return;
        }
        foreach (interact_react interact in _interacts)
            interact.Clear();
        for (int i = _signal.Length - 1; i > -1; i--)
            _signal[i] = false;
        _next = 0;
    }
}
