using UnityEngine;
// cyka
public class item_key : base_item
{
    [Tooltip("(interact only)")] [SerializeField] protected Transform _valid = null;
    public override void Use(entity_data source, Transform target = null)
    {
        // print(target);
        if (target == _valid)
            Destroy();
    }
}
