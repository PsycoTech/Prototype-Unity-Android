using UnityEngine;
public class item_healthRestore : base_item
{
    // +1 dried meat
    // +2 health vial
    // +9 health potion
    [SerializeField] protected int _amount = 0;
    public override void Use(entity_data source, Transform target = null)
    {
        if (target?.gameObject.layer == game_variables.Instance.LayerPlayer || target?.gameObject.layer == game_variables.Instance.LayerMob)
        {
            // source.SetEquipped(null);
            target.GetComponent<entity_data>()?.HealthRestore(_amount);
            HealthDrain(1);
        }
        else if (source?.gameObject.layer == game_variables.Instance.LayerPlayer || source?.gameObject.layer == game_variables.Instance.LayerMob)
        {
            // source.SetEquipped(null);
            source.GetComponent<entity_data>()?.HealthRestore(_amount);
            HealthDrain(1);
        }
        // else
        //     // feedback_popup.Instance.RegisterMessage(source.transform, "invalid target", game_variables.Instance.ColorDefault);
        //     feedback_toaster.Instance.RegisterMessage(gameObject.name + " : invalid target", game_variables.Instance.ColorDefault);
        _uses = _healthInst;
    }
}
