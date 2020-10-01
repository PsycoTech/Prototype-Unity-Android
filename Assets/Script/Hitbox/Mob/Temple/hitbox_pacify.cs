using UnityEngine;
using System.Collections.Generic;
public class hitbox_pacify : MonoBehaviour
{
    [Tooltip("Time till self destruct")] [SerializeField] protected float _timer = 0f;
    protected List<controller_mob> _targets = new List<controller_mob>();
    void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else
        {
            foreach (controller_mob target in _targets)
                target.SetSensors(true);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == game_variables.Instance.LayerMob)
        {
            controller_mob temp = other.GetComponent<controller_mob>();
            if (!_targets.Contains(temp))
            {
                temp.SetSensors(false);
                _targets.Add(temp);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == game_variables.Instance.LayerMob)
        {
            controller_mob temp = other.GetComponent<controller_mob>();
            if (_targets.Contains(temp))
            {
                temp.SetSensors(true);
                _targets.Remove(temp);
            }
        }
    }
}
