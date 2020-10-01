using UnityEngine;
// chest
public class interact_drop : base_interact
{
    [Tooltip("Reference to drop (item only)")] [SerializeField] protected GameObject _drop = null;
    // * testing
    protected Collider2D _collider;
    protected override void Start()
    {
        if (_drop)
            _drop.GetComponent<base_item>().New();
        _collider = GetComponent<Collider2D>();
        base.Start();
    }
    public override void Load()
    {
        base.Load();
        if (_drop)
        {
            _drop.GetComponent<base_item>().Load();
            // if (_drop.activeSelf)
            //     _drop.SetActive(false);
        }
        if (_collider && !_collider.enabled)
            _collider.enabled = true;
    }
    public override int TryAction(Transform target)
    {
        int check = base.TryAction(target);
        if (check > 0)
        {
            if (_drop && !_drop.activeSelf)
                // ? item specific
                _drop.SetActive(true);
            if (_collider && _collider.enabled)
                _collider.enabled = false;
        }
        return check;
    }
    // public override void Destroy()
    // {
    //     if (_drop && !_drop.activeSelf)
    //         _drop.SetActive(true);
    //     base.Destroy();
    // }
}
