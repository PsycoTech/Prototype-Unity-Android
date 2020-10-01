using UnityEngine;
using System.Collections.Generic;
// spill
public class hitbox_heal : MonoBehaviour
{
    [SerializeField] protected int _amount = 1;
    [Tooltip("Time between heal ticks")] [SerializeField] protected float _time = 1;
    protected float _timer = 0;
    protected List<entity_data> _targets = new List<entity_data>();
    void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else 
        {
            _timer = _time;
            foreach(entity_data target in _targets)
                target?.HealthRestore(_amount);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == game_variables.Instance.LayerChunk)
            return;
        if (other.gameObject.layer == game_variables.Instance.LayerPlayer || other.gameObject.layer == game_variables.Instance.LayerMob)
        {
            entity_data temp = other.GetComponent<entity_data>();
            if (!temp || _targets.Contains(temp))
                return;
            else
                _targets.Add(temp);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == game_variables.Instance.LayerChunk)
            return;
        if (other.gameObject.layer == game_variables.Instance.LayerPlayer || other.gameObject.layer == game_variables.Instance.LayerMob)
        {
            entity_data temp = other.GetComponent<entity_data>();
            if (_targets.Contains(temp))
                _targets.Remove(temp);
            else
                return;
        }
    }
}
