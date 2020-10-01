using UnityEngine;
// placeholder/anticipation for mob attack
public class hitbox_attack : base_hitbox
{
    [Tooltip("Prefab to spawn")] [SerializeField] protected GameObject _attack = null;
    [Tooltip("Delay to spawn/self destruct")] [SerializeField] protected float _timer = 0f;
    protected override void Update()
    {
        base.Update();
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else
            Destroy();
    }
    protected override void Destroy()
    {
        if (_attack)
            Instantiate(_attack, transform.position, transform.rotation).GetComponent<base_hitbox>().Initialize(_source);
        base.Destroy();
    }
}
