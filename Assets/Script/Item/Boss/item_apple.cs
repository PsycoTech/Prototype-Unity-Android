using UnityEngine;
public class item_apple : base_item
{
    [SerializeField] protected Transform _valid;
    public override void Use(entity_data source, Transform target = null)
    {
        if (target?.gameObject.layer == game_variables.Instance.LayerPlayer || target?.gameObject.layer == game_variables.Instance.LayerMob)
        {
            // source.SetEquipped(null);
            if (target == _valid)
                target.GetComponent<entity_data>().HealthDrain();
            else
                target.GetComponent<entity_data>().HealthAdd(3);
            // gameObject.SetActive(false);
            HealthDrain(1);
        }
        // else
        //     // feedback_popup.Instance.RegisterMessage(source.transform, "invalid target", game_variables.Instance.ColorDefault);
        //     feedback_toaster.Instance.RegisterMessage(gameObject.name + " : invalid target", game_variables.Instance.ColorDefault);
    }
}
