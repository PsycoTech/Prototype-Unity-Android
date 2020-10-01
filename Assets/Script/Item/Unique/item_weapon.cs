using UnityEngine;
// flintlock saber shotgun
public class item_weapon : base_item
{
    [Tooltip("-1 - none | 2 - flintlock | 3 - shotgun | 4 - saber")] [SerializeField] protected int _type = -1;
    [SerializeField] protected GameObject _hitbox;
    [SerializeField] protected int _amount = 1;
    [SerializeField] protected float _spread = 0f;
    public override void Use(entity_data source, Transform target = null)
    {
        // if (target.gameObject.layer == game_variables.Instance.LayerPlayer || target.gameObject.layer == game_variables.Instance.LayerMob)
        // {
        if (source.IsCollectible(_type))
        {
            for (int i = 0; i < _amount; i++)
                Instantiate(_hitbox, transform.position, Quaternion.Euler(0f, 0f, transform.eulerAngles.z + (_spread / (float)_amount) * (i - Mathf.FloorToInt(_amount / 2)))).GetComponent<base_hitbox>().Initialize(source.transform);
            source.CollectibleModify(_type, -1);
            _uses = source.GetCollectible(_type);
        }
        else
            feedback_popup.Instance.RegisterMessage(source.transform, "*click*", game_variables.Instance.ColorDefault, .5f);
        // }
        // else
        //     base.Use(target, source);
            // feedback_toaster.Instance.RegisterMessage(gameObject.name + " : invalid target", game_variables.Instance.ColorDefault);
            // feedback_popup.Instance.RegisterMessage(source.transform, "invalid target", game_variables.Instance.ColorDefault);
    }
}
