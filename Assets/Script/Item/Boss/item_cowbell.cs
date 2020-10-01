using UnityEngine;
public class item_cowbell : base_item
{
    [SerializeField] protected float _radiusEffect;
    public override void Use(entity_data source, Transform target = null)
    {
        if (target?.gameObject.layer == game_variables.Instance.LayerPlayer || target?.gameObject.layer == game_variables.Instance.LayerMob)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radiusEffect, game_variables.Instance.ScanLayerMob);
            foreach (Collider2D collider in colliders)
                // * testing
                collider.GetComponent<controller_mob>().RegisterEvent(transform.position, game_variables.Instance.LayerPlayer, Time.time);
        }
        // else
        //     feedback_toaster.Instance.RegisterMessage(gameObject.name + " : invalid target", game_variables.Instance.ColorDefault);
            // feedback_popup.Instance.RegisterMessage(source.transform, "invalid target", game_variables.Instance.ColorDefault);
    }
}
