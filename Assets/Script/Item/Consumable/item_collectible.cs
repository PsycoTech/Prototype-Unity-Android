using UnityEngine;
public class item_collectible : base_item
{
    [Tooltip("0 - gem | 1 - pouch | 2 - flintlock | 3 - shotgun | 4 - saber | 5 - coin?")] [SerializeField] protected int _id = -1;
    [SerializeField] protected int _amount = 0;
    public override void Use(entity_data source, Transform target = null)
    {
        if (source?.gameObject.layer == game_variables.Instance.LayerPlayer)
        {
            source.CollectibleModify(_id, _amount);
            // controller_player.Instance.Data.Drop(this as base_item);
            Destroy();
            // HealthDrain(1);
            // base.Use(source, target);
        }
        // // indestructible...
        // HealthDrain(1);
        // ...hence
        // else
        //     // feedback_popup.Instance.RegisterMessage(source.transform, "invalid target", game_variables.Instance.ColorDefault);
        //     feedback_toaster.Instance.RegisterMessage(gameObject.name + " : invalid target", game_variables.Instance.ColorDefault);
    }
}
