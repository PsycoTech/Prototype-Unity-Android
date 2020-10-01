using UnityEngine;
// vine
public class react_disable : base_react
{
    [SerializeField] protected GameObject _target;
    [SerializeField] protected Collider2D _collider;
    public override void Load()
    {
        base.Load();
        _collider.enabled = true;
    }
    protected override void Update()
    {
        base.Update();
        // * testing
        if (_target.activeSelf != _active)
        {
            _target.SetActive(_active);
            _collider.enabled = false;
        }
    }
}
