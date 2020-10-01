using UnityEngine;
public class entity_controller : MonoBehaviour
{
    public Transform _host;
    protected entity_data _data;
    protected entity_motor _motor;
    protected entity_anim _anim;
    [Tooltip("Pathfind update delay")] [SerializeField] protected float _timePath = 1f;
    protected float _timerPath;
    protected virtual void Awake()
    {
        // ??? post initialize
        if (_host)
        {
            _data = _host.GetComponent<entity_data>();
            _motor = _host.GetComponent<entity_motor>();
            _anim = _host.GetChild(0).GetComponent<entity_anim>();
        }
        _timerPath = 0f;
        // else
        // {
        //     // * testing
        //     _host = transform;
        //     _data = _host.GetComponent<entity_data>();
        //     _motor = _host.GetComponent<entity_motor>();
        // }
    }
    #region Properties
    public virtual entity_data Data
    {
        get { return _data; }
    }
    public virtual entity_motor Motor
    {
        get { return _motor; }
    }
    public virtual entity_anim Anim
    {
        get { return _anim; }
    }
    #endregion
}
