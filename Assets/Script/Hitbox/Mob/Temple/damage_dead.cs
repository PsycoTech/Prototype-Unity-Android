using UnityEngine;
public class damage_dead : hitbox_damage
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == _source)
            return;
        // valid target
        if (other.gameObject.layer == game_variables.Instance.LayerPlayer || other.gameObject.layer == game_variables.Instance.LayerMob)
            _source.GetComponent<entity_data>().HealthDrain();
        base.OnTriggerEnter2D(other);
    }
}
