using UnityEngine;
// layer : react
// arrow
public class react_instantiate : base_react
{
    [Tooltip("Object to spawn (hitbox only)")] [SerializeField] protected GameObject _target;
    [Tooltip("Delay between spawns")] [SerializeField] protected float _time;
    protected float _timer;
    protected override void Update()
    {
        base.Update();
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else if (_active)
        {
            _timer = _time;
            GameObject temp = Instantiate(_target, transform.position, transform.rotation);
            temp.GetComponent<base_hitbox>().Initialize(transform);
            temp.SetActive(true);
            _active = false;
        }
    }
}
