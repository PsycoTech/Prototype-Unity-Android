using UnityEngine;
public class item_axe : base_item
{
    [SerializeField] protected GameObject _hitbox = null;
    // [Tooltip("Cool down")] [SerializeField] protected float _time = .5f;
    protected float _charge;
    protected float _charges;
    public override void New()
    {
        base.New();
        // 
        _charge = 5f;
        _charges = 5f;
    }
    void Update()
    {
        if (_charge < _charges)
            _charge += Time.deltaTime;
        if (_uses != Mathf.FloorToInt(_charge))
            _uses = Mathf.FloorToInt(_charge);
    }
    public override void Use(entity_data source, Transform target = null)
    {
        if (_charge < 1f)
            return;
        // if (target.gameObject.layer == game_variables.Instance.LayerPlayer || target.gameObject.layer == game_variables.Instance.LayerMob)
        // {
        // source.SetEquipped(null);
        Instantiate(_hitbox, transform.position, transform.rotation).GetComponent<base_hitbox>().Initialize(source.transform);
        // gameObject.SetActive(false);
        // HealthDrain(1);
        _charge--;
        // }
        // print("Use " + gameObject.name + ":" + source);
        // else
        //     feedback_toaster.Instance.RegisterMessage(gameObject.name + " : invalid target", game_variables.Instance.ColorDefault);
            // feedback_popup.Instance.RegisterMessage(source.transform, "invalid target", game_variables.Instance.ColorDefault);
    }
}
