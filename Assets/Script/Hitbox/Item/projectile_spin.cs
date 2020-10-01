using UnityEngine;
public class projectile_spin : damage_projectile
{
    [SerializeField] protected float _spin = 5f;
    protected override void Update()
    {
        base.Update();
        if (_sprite)
            _sprite.transform.eulerAngles += Vector3.forward * _spin * Time.deltaTime;
    }
}
