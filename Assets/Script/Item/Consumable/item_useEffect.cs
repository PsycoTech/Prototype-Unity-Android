using UnityEngine;
// shield jar charm torch gun
public class item_useEffect : base_item
{
    // [Tooltip("Number of uses (-1 unlimited)")] [SerializeField] protected int _uses = -1;
    [Tooltip("Spawn on valid use")] [SerializeField] protected GameObject _effect = null;
    // [Tooltip("Delay before destroy")] [SerializeField] protected float _time = 0f;
    // [Tooltip("false - instantiate | true - set active")] [SerializeField] protected bool _parent = false;
    // protected float _timer;
    // public override void Load()
    // {
    //     _timer = -1f;
    // }
    // void Update()
    // protected override void Update()
    // {
    //     base.Update();
    //     // if (_timer > 0)
    //     //     _timer -= Time.deltaTime;
    //     // else if (_timer != -1)
    //     //     Destroy();
    //     // if (_uses == 0)
    //     //     _timer = _time;
    //     // base.Update();
    //     if (_uses == 0)
    //         Destroy();
    // }
    public override void Use(entity_data source, Transform target = null)
    {
        // if (target.gameObject.layer == game_variables.Instance.LayerPlayer || target.gameObject.layer == game_variables.Instance.LayerMob)
        // {
        if (_effect)
            Instantiate(_effect, transform.position, transform.rotation).GetComponent<base_hitbox>().Initialize(source.transform);
        // if (_uses != -1)
        //     _uses--;
        HealthDrain(1);
        // }
        // else
        //     // feedback_popup.Instance.RegisterMessage(source.transform, "invalid target", game_variables.Instance.ColorDefault);
        //     feedback_toaster.Instance.RegisterMessage(gameObject.name + " : invalid target", game_variables.Instance.ColorDefault);
    }
    void Update()
    {
        if (_uses != _healthInst)
            _uses = _healthInst;
    }
}
