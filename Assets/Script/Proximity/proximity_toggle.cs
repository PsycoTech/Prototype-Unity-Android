using UnityEngine;
// spike
public class proximity_toggle : base_proximity
{
    [SerializeField] protected GameObject _target;
    public override void Load()
    {
        base.Load();
        if (_target.activeSelf != _active)
            _target.SetActive(_active);
    }
    protected override void Update()
    {
        base.Update();
        // * testing
        if (_target.activeSelf != _active)
            _target.SetActive(_active);
    }
}
