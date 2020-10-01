using UnityEngine;
// torch
public class item_hold : base_item
{
    [Tooltip("Effect enabled while pickup (hitbox only)")] [SerializeField] protected GameObject _effect = null;
    public override void Load()
    {
        base.Load();
        if (_effect)
            _effect.SetActive(false);
    }
    // protected override void Update()
    // {
    //     base.Update();
    //     if (_effect)
    //         _effect.SetActive(transform.parent != null);
    // }
    public override void SetParent(Transform parent)
    {
        if (_effect)
        {
            // * testing hard coded source get
            _effect.GetComponent<base_hitbox>().Initialize(parent?.parent.parent);
            _effect.SetActive(parent);
        }
        base.SetParent(parent);
    }
}
