using UnityEngine;
using Panda;
public class mob_slingSlime : controller_mob
{
    protected Vector3[] _fling = new Vector3[2];
    public override void Load()
    {
        base.Load();
        _fling = new Vector3[2];
    }
    [Task]
    void EntityLerpToMove(int index, float time)
    {
        if (_timers[index] > 0)
            _motor.ToPosition(Vector3.Lerp(_fling[1], _fling[0], _timers[index] / time));
        // else if (Mathf.Approximately(Vector3.Distance(_motor.Position, _fling[1]), 0f))
        else if (Vector3.Distance(_motor.Position, _fling[1]) <= .1f)
            Task.current.Succeed();
        else
        {
            _timers[index] = time;
            _fling[0] = _motor.Position;
            _fling[1] = _anchor.Position;
        }
    }
}
