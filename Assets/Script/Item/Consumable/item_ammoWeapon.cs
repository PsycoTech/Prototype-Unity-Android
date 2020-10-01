using UnityEngine;
public class item_ammoWeapon : base_item
{
    [Tooltip("0 - flintlock | 1 - shotgun | 2 - saber")] [SerializeField] protected int _type = -1;
    [SerializeField] protected int _amount = 0;
    public override void Use(entity_data source, Transform target = null)
    {
        if (target?.gameObject.layer == game_variables.Instance.LayerPlayer || target?.gameObject.layer == game_variables.Instance.LayerMob)
        {
            // source.SetEquipped(null);
            // target.GetComponent<entity_data>().AmmoModify(_type, _amount);
            // gameObject.SetActive(false);
            HealthDrain(1);
        }
        // else
        //     // feedback_popup.Instance.RegisterMessage(source.transform, "invalid target", game_variables.Instance.ColorDefault);
        //     feedback_toaster.Instance.RegisterMessage(gameObject.name + " : invalid target", game_variables.Instance.ColorDefault);
    }
}
