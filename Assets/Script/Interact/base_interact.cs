using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
// destructible unlock toggle oneWay
// door
public class base_interact : MonoBehaviour
{
    public bool _testDisable;
    [Tooltip("Item that unlocks")] [SerializeField] protected Transform _valid = null;
    // [Tooltip("Item that unlocks")] [SerializeField] protected List<Transform> _valid = new List<Transform>();
    [SerializeField] protected bool _locked = false;
    // [SerializeField] protected int _lockedTest = 0;     //0 - unlocked | 1+ - locked
    [Tooltip("Lock to state change")] [SerializeField] protected bool _oneWay = false;
    // [SerializeField] protected bool _feedback = true;
    // [Tooltip("Health till break (-1 unbreakable)")] [SerializeField] protected int _health = -1;
    // protected int _healthInst = 0;
    [Tooltip("Initial state")] [SerializeField] protected bool _default = false;
    protected bool _active;
    protected SpriteRenderer _sprite;
    [Tooltip("Sprite pool")] [SerializeField] protected List<Sprite> _sprites = new List<Sprite>();
    // // Save
    // protected bool _activeCache;
    protected state_interact _state;
    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    protected virtual void Start()
    {
        manager_interact.Instance.Register(this);
        New();
    }
    public virtual void New()
    {
        _state = new state_interact();
        // _healthInst = _health;
        _active = _default;
        gameObject.SetActive(true);
        if (_sprite)
            _sprite.enabled = true;
    }
    public virtual void Save()
    {
        // _healthInst = _health;
        // _active = _default;
        // gameObject.SetActive(true);
        // _sprite.enabled = false;
        _state.Locked = _locked;
        _state.Active = _active;
    }
    public virtual void Load()
    {
        // _healthInst = _health;
        // _active = _default;
        // gameObject.SetActive(true);
        // _sprite.enabled = false;
        _locked = _state.Locked;
        _active = _state.Active;
    }
    void Update()
    {
        if (_testDisable && _active)
            gameObject.SetActive(false);
        // * testing
        // _sprite.enabled = Vector3.Distance(transform.position, controller_player.Instance.Motor.Position) <= game_variables.Instance.RadiusSprite;
        // _sprite.enabled =  game_camera.Instance.InView(transform.position);
        SetSprite();
    }
    protected void SetSprite()
    {
        if (_sprites.Count < 2)
            return;
        _sprite.sprite = _active ? _sprites[1] : _sprites[0];
    }
    // 0 - fail | 1 - succeed | 2 - succeed with item
    public virtual int TryAction(Transform target)
    {
        // print("doh");
        int check = 0;
        if (_locked && target == _valid)
        {
            _locked = false;
            check++;
        }
        // {
        //     feedback_popup.Instance.RegisterMessage(transform, "unlocked", game_variables.Instance.ColorInteract);
        //     return true;
        // }
        if (!_locked)
        {
            _active = _oneWay ? true : !_active;
            check++;
            // if (_oneWay && _active)
            //     feedback_popup.Instance.RegisterMessage(transform, _valid ? "opened" : "on", game_variables.Instance.ColorInteract);
            // else if (_active)
            //     feedback_popup.Instance.RegisterMessage(transform, "on", game_variables.Instance.ColorInteract);
            // else
            //     feedback_popup.Instance.RegisterMessage(transform, "off", game_variables.Instance.ColorInteract);
            // * testing ? duration type
            if (game_variables.Instance.Vibration == 0 || game_variables.Instance.Vibration == 2)
                Handheld.Vibrate();
            return check;
        }
        // * testing
        if (_valid)
            // feedback_popup.Instance.RegisterMessage(transform, "need " + _valid.gameObject.name, game_variables.Instance.ColorInteract);
            feedback_toaster.Instance.RegisterMessage(gameObject.name + " : need " + _valid.gameObject.name, game_variables.Instance.ColorInteract);
        else
            // feedback_popup.Instance.RegisterMessage(transform, "jammed", game_variables.Instance.ColorInteract);
            feedback_toaster.Instance.RegisterMessage(gameObject.name + " : jammed", game_variables.Instance.ColorInteract);
        return check;
    }
    // public void HealthDrain(int value)
    // {
    //     if (_health == -1)
    //         return;
    //     feedback_popup.Instance.RegisterMessage(transform, "health -" + value, game_variables.Instance.ColorDamage);
    //     _healthInst -= value;
    //     _healthInst = Mathf.Clamp(_healthInst, 0, _health);
    //     if (_healthInst == 0)
    //         Destroy();
    // }
    // public virtual void Destroy()
    // {
    //     gameObject.SetActive(false);
    //     Load();
    // }
}
