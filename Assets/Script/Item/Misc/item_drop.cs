using UnityEngine;
// crate sack
public class item_drop : base_item
{
    [Tooltip("Reference to dropped item")] [SerializeField] protected GameObject _drop = null;
    public override void Load()
    {
        base.Load();
        if (_drop)
            _drop.GetComponent<base_item>().Load();
    }
    public override void Destroy()
    {
        base.Destroy();
        if (_drop)
            _drop.SetActive(true);
    }
}
