using UnityEngine;
using System.Collections.Generic;
public class entity_data : MonoBehaviour
{
    protected entity_motor _motor;
    protected entity_anim _anim;
    protected controller_mob _controller;
    // stats
    [SerializeField] protected int _health = 1;
    protected int _healthInst;
    // // feedback
    // public GameObject _gib;
    // public GameObject _gibMini;
    // 
    protected base_item _equipped;
    // protected bool _autoPickup;
    // * testing
    protected List<Transform> _hostiles;
    protected bool _resurrect;
    // protected int[] _ammoWeapons;
    // * testing action
    [Tooltip("Maximum interact distance")] [SerializeField] protected float _radius = 1.5f;
    // protected int _flag;    // 0 - ignore | 1 - interact | 2 - pickup | 3 - use
    // protected Transform _target;
    // protected CircleCollider2D _collider;
    [Tooltip("Action radius display")] [SerializeField] protected feedback_action _action = null;
    [Tooltip("Enable on death")] [SerializeField] protected List<GameObject> _drops = new List<GameObject>();
    [Tooltip("Drop yeet force")] [SerializeField] protected float _force = 1f;
    // protected int _inventory;
    protected int[] _collectibles;
    // * testing iframes
    [Tooltip("Iframe duration")] [SerializeField] protected float _timeIframes = 0f;
    protected float _timerIframes;
    // public bool testImmune
    private bool _invisible = true;
    private state_data _state;
    void Awake()
    {
        _motor = GetComponent<entity_motor>();
        _anim = transform.GetChild(0).GetComponent<entity_anim>();
        _hostiles = new List<Transform>();
        // if (transform.childCount > 1)
        //     _collider = transform.GetChild(1).GetComponent<CircleCollider2D>();
    }
    void Start()
    {
        Initialize();
        New();
    }
    // ? override Start
    protected virtual void Initialize()
    {
        if (gameObject.layer == game_variables.Instance.LayerMob)
            _controller = GetComponent<controller_mob>();
        // 
        _equipped = null;
        // _autoPickup = false;
        // _health = 1;
        // _ammoWeapons = new int[3];
        // ? new load save
        // * testing gem ?coin ?pearl
        _collectibles = new int [5];
        // gold
        // pouch
        CollectibleModify(1, 1);
        // ammo flintlock
        CollectibleModify(2, 8);
        // ammo shotgun
        CollectibleModify(3, 4);
        // ammo saber
        CollectibleModify(4, 6);
        _invisible = false;
    }
    public virtual void New()
    {
        // 
        _healthInst = _health;
        _resurrect = false;
        _hostiles.Clear();
        if (gameObject.layer == game_variables.Instance.LayerMob)
            _hostiles.Add(controller_player.Instance.Motor.transform);
        Clear();
        // Save();
        // _inventory = 1;
        _state = new state_data();
    }
    public virtual void Load()
    {
        // GetState();
        base_state temp = (base_state)_state;
        manager_state.Instance.Load(gameObject.name, out temp, TypeState.MOB);
        _state = (state_data)temp;
        // * testing
        // _healthInst = _state.Health;
        HealthRestore();
        HealthDrain(_health - _state.Health);
        _resurrect = _state.Resurrect;
        // _hostiles = _state.Hostiles;
        _hostiles.Clear();
        if (gameObject.layer == game_variables.Instance.LayerMob)
            _hostiles.Add(controller_player.Instance.Motor.transform);
        // Clear();
        // _target = _state.Target;
        // _target = null;
        // _flag = _state.Flag;
        // if (_collider)
        //     _collider.radius = _state.Radius;
        // * testing feedback action
        if (_action)
            _action.Disable();
        // _inventory = _state.Inventory;
        _timerIframes = 0f;
    }
    public virtual void Save()
    {
        // SetState();
        // * testing
        _state.Health = _healthInst;
        _state.Resurrect = _resurrect;
        // _state.Flag = _flag;
        // if (_collider)
        //     _state.Radius = _collider.radius;
        // _state.Inventory = _inventory;
        manager_state.Instance.Save(gameObject.name, _state, TypeState.MOB);
    }
    // void Update()
    protected virtual void Update()
    {
        // remove dead
        if (_hostiles.Count > 0)
        {
            List<Transform> toRemove = new List<Transform>();
            foreach (Transform hostile in _hostiles)
                if (!hostile || !hostile.gameObject.activeSelf)
                    toRemove.Add(hostile);
            foreach (Transform hostile in toRemove)
                _hostiles.Remove(hostile);
        }
        // * testing ? duplicate
        if (_equipped)
        {
            _motor.SetModifierSpeed(_equipped.Modifier);
            // _motor.AddModifierDrag(_equipped.Modifier);
            // // * testing auto drop
            // if (!_equipped.gameObject.activeSelf)
            //     SetEquipped(null, false);
        }
        // 
        if (_timerIframes > 0f)
            _timerIframes -= Time.deltaTime;
        // * testing iframe flicker
        _anim.SetFlicker(_timerIframes > 0f);
        // _motor.CollidersToggle(false);
        _anim.SetColor(_invisible ? game_variables.Instance.ColorDefault : game_variables.Instance.ColorEntity);
    }
    // protected void SetState()
    // {
    //     // * testing
    //     // _state = new state_data();
    //     manager_state.Instance.Save(gameObject.name, _state);
    // }
    // protected void GetState()
    // {
    //     // ??? id hash
    //     manager_state.Instance.Load(gameObject.name, out _state);
    // }
    // public void InventoryAdd()
    // {
    //     _inventory++;
    // }
    public bool IsCollectible(int type)
    {
        // * testing gun dev
        if (type == -1)
            return true;
        if (type > -1 && type < _collectibles.Length)
            return _collectibles[type] > 0;
        return false;
    }
    // public void AmmoModify(int type, int amount)
    // {
    //     if (type > -1 && type < _ammoWeapons.Length)
    //     {
    //         _ammoWeapons[type] += amount;
    //         _ammoWeapons[type] = Mathf.Clamp(_ammoWeapons[type], 0, int.MaxValue);
    //         // feedback_popup.Instance.RegisterMessage(transform, "ammo" + type + " +" + amount, game_variables.Instance.ColorItem);
    //         if (gameObject.layer == game_variables.Instance.LayerPlayer)
    //             feedback_toaster.Instance.RegisterMessage(gameObject.name + " : ammo" + type + " " + amount, game_variables.Instance.ColorItem);
    //     }
    // }
    public void HealthAdd(int value)
    {
        _health += value;
        HealthRestore();
        status_entityHealth.Instance.Initialize();
    }
    public virtual void HealthDrain(int value = 0, Transform source = null)
    {
        if (_healthInst == 0 || _timerIframes > 0f)
            return;
        _healthInst -= value == 0 ? _health : value;
        _healthInst = Mathf.Clamp(_healthInst, 0, _health);
        if (source)
        {
            if (!_hostiles.Contains(source))
                _hostiles.Add(source);
            if (gameObject.layer == game_variables.Instance.LayerPlayer)
                feedback_damage.Instance.RegisterMarker(source);
            if (gameObject.layer == game_variables.Instance.LayerMob)
                _controller.RegisterEvent(source.position, source.gameObject.layer, Time.time);
        }
        print("Hurt " + gameObject.name + ":" + source);
        // * testing
        if (gameObject.layer == game_variables.Instance.LayerPlayer)
        {
            // ? duration
            if (game_variables.Instance.Vibration == 0 || game_variables.Instance.Vibration == 1)
                Handheld.Vibrate();
            feedback_toaster.Instance.RegisterMessage(gameObject.name + " : health -" + (value == 0 ? _health : value), game_variables.Instance.ColorDamage);
        }
        else if (gameObject.layer == game_variables.Instance.LayerMob)
            feedback_popup.Instance.RegisterMessage(transform, "health -" + (value == 0 ? _health : value), game_variables.Instance.ColorDamage);
        // // * testing drop on hurt
        // if (_equipped)
        //     SetEquipped(null);
        // Instantiate(_gib, transform.position, transform.rotation).SetActive(true);
        // * testing
        if (_healthInst == 0)
        {
            Clear();
            _motor.NavigateCancel();
            // * testing drop item
            // menu_inventory.Instance.SetEquipped(null);
            SetEquipped(null);
            if (_resurrect)
            {
                HealthRestore();
                if (gameObject.layer == game_variables.Instance.LayerPlayer)
                   SetIframes();
            }
            else if (gameObject.layer == game_variables.Instance.LayerPlayer)
            {
                // game_master.Instance.Save();
                manager_ui.Instance.SetMain(true);
                input_touch.Instance.Initialize();
            }
            else if (gameObject.layer == game_variables.Instance.LayerMob)
            {
                _hostiles.Clear();
                _hostiles.Add(controller_player.Instance.Motor.transform);
                // * testing disable colliders ? disallow target
                gameObject.SetActive(false);
                foreach (GameObject drop in _drops)
                {
                    base_item temp = (Instantiate(drop, transform.position, transform.rotation)).GetComponent<base_item>();
                    temp?.gameObject.SetActive(true);
                    temp?.SetParent(null);
                    // temp?.AddForce(new Vector2(Random.Range(-1f, 1f) * _force, Random.Range(-1f, 1f) * _force));
                }
            }
        }
    }
    public void SetResurrect()
    {
        _resurrect = true;
    }
    // * testing ? BT
    public void HealthRestore(int value = 0)
    {
        _healthInst += value == 0 ? _health : value;
        _healthInst = Mathf.Clamp(_healthInst, 0, _health);
        if (gameObject.layer == game_variables.Instance.LayerPlayer)
            feedback_toaster.Instance.RegisterMessage(gameObject.name + " : health +" + (value == 0 ? _health : value), game_variables.Instance.ColorEntity);
        // * testing revive
        gameObject.SetActive(true);
    }
    // null - drop | else - equip
    public bool SetEquipped(base_item item)
    {
        // if (item == null)
        // {
        //     if (!_equipped)
        //         return;
        //     if (gameObject.layer == game_variables.Instance.LayerPlayer)
        //     {
        //         feedback_toaster.Instance.RegisterMessage(gameObject.name + " : drop " + _equipped?.gameObject.name, game_variables.Instance.ColorItem);
        //         menu_inventory.Instance.SetEquipped(_equipped, false);
        //     }
        //     _equipped?.SetParent(null);
        //     _equipped?.SetActive(true);
        //     _equipped = null;
        // }
        // else
        // {
        //     if (_equipped != null)
        //     {
        //         if (menu_inventory.Instance.IsEmptyOrContains(item))
        //             _equipped.SetActive(false);
        //         else
        //         {
        //             _equipped.SetParent(null);
        //             _equipped.SetActive(true);
        //         }
        //     }
        //     // else if (menu_inventory.Instance.IsEmptyOrContains(item))
        //     // {
        //     //     // 
        //     // }
        //     if (gameObject.layer == game_variables.Instance.LayerPlayer)
        //     {
        //         feedback_toaster.Instance.RegisterMessage(gameObject.name + " : equip " + item.gameObject.name, game_variables.Instance.ColorItem);
        //         menu_inventory.Instance.SetEquipped(item, true);
        //     }
        //     _equipped = item;
        //     _equipped.SetActive(true);
        //     _equipped.SetParent(_motor._anchor);
        // }
        // ---
        // // player
        // if (gameObject.layer == game_variables.Instance.LayerPlayer)
        // {
        //     if (item)
        //     {
        //         // add
        //         feedback_toaster.Instance.RegisterMessage(gameObject.name + " : equip " + item.gameObject.name, game_variables.Instance.ColorItem);
        //         menu_inventory.Instance.InventoryModify(item, true);
        //     }
        //     else
        //     {
        //         // remove
        //         feedback_toaster.Instance.RegisterMessage(gameObject.name + " : drop " + _equipped?.gameObject.name, game_variables.Instance.ColorItem);
        //         menu_inventory.Instance.InventoryModify(_equipped, false);
        //     }
        // }
        // ---
        // // * testing
        // // pickup
        // if (item)
        // {
        //     // player
        //     if (gameObject.layer == game_variables.Instance.LayerPlayer)
        //     {
        //         // try add/replace
        //         if (!menu_inventory.Instance.Modify(item, true))
        //             // fail - ignore
        //             return;
        //         // 
        //         if (_equipped)
        //         {
        //             // added
        //             if (menu_inventory.Instance.Contains(_equipped))
        //                 // holster
        //                 _equipped.SetActive(false);
        //             // replaced
        //             else
        //                 // drop
        //                 _equipped.SetParent(null);
        //         }
        //     }
        //     // store/overwrite reference
        //     _equipped = item;
        //     _equipped.SetParent(_motor._anchor);
        // }
        // // drop
        // else
        // {
        //     // ignore
        //     if (!_equipped)
        //         return;
        //     // player, try remove
        //     if (gameObject.layer == game_variables.Instance.LayerPlayer && !menu_inventory.Instance.Modify(_equipped, false))
        //         // fail - ignore
        //         return;
        //     // clear reference
        //     _equipped.SetParent(null);
        //     _equipped = item;
        // }
        // ---
        if (!item)
            return false;
        // player, try add
        if (gameObject.layer == game_variables.Instance.LayerPlayer && !menu_inventory.Instance.Modify(item, true))
            // fail - ignore
            return false;
        // store/overwrite reference
        Holster();
        _equipped = item;
        _equipped.SetParent(_motor._anchor);
        return true;
    }
    public void Drop(base_item item)
    {
        // ignore
        if (!item)
            return;
        // player, try remove
        if (gameObject.layer == game_variables.Instance.LayerPlayer && !menu_inventory.Instance.Modify(item, false))
            // fail - ignore
            return;
        // drop
        item.SetParent(null);
        // clear reference
        if (item == _equipped)
            _equipped = null;
    }
    public void Holster()
    {
        _equipped?.SetActive(false);
        _equipped = null;
    }
    // public void SetActive(bool value)
    // {
    //     gameObject.SetActive(value);
    // }
    public void SetIframes()
    {
        _timerIframes = _timeIframes;
    }
    public void CollectibleModify(int id, int amount)
    {
        if (id > -1 && id < _collectibles.Length)
            _collectibles[id] += amount;
    }
    public int GetCollectible(int id)
    {
        if (id > -1 && id < _collectibles.Length)
            return _collectibles[id];
        return 0;
    }
    public void ToggleInvisible()
    {
        _invisible = !_invisible;
    }
    # region Action
    // void OnTriggerEnter2D(Collider2D other)
    public bool Action(Transform target)
    {
        // *testing ? ignore
        if (!target)
            return true;
        // if (_flag == 0)
        //     return;
        // if (_target != other)
        //     return;
        // print(_flag);
        // stop preemptively ?
        // _motor.NavigateCancel();
        // _anim.SetRotation(_target.position);
        // // ignore
        // if (other == _equipped?.transform)
        //     return false;
        // // in range
        // if (Vector3.Distance(_motor.Position, target.transform.position) < _radius + .5f)
        // {
        // interact
        if (target.gameObject.layer == game_variables.Instance.LayerInteract && target.GetComponent<base_interact>().TryAction(_equipped?.transform) == 1)
            return false;
        // pickup
        // if (!_equipped && other.gameObject.layer == game_variables.Instance.LayerItem)
        // base_item temp = other.GetComponent<base_item>();
        // if (other.gameObject.layer == game_variables.Instance.LayerItem && !menu_inventory.Instance.Contains(temp) && SetEquipped(temp))
        if (!_equipped && target.gameObject.layer == game_variables.Instance.LayerItem && SetEquipped(target.GetComponent<base_item>()))
            return false;
        // {
        //     SetEquipped(other.GetComponent<base_item>());
            // menu_inventory.Instance.SetEquipped(other.GetComponent<base_item>());
        // }
        // }
        // // drop
        // if (_equipped && !_equipped.gameObject.activeSelf)
        //     SetEquipped(null);
        // Clear();
        return true;
    }
    public void Clear()
    {
        // _target = null;
        // _flag = 0;
        // if (_collider)
        //     _collider.radius = _radius + .5f;
        // * testing feedback action
        if (_action)
            _action.Disable();
    }
    // // false - far > approach | true - done > hold
    // public bool TryInteract(Transform target)
    // {
    //     if (Vector3.Distance(transform.position, target.position) > _radius + .5f)
    //     {
    //         // enable interact
    //         // _target = target;
    //         // _flag = 1;
    //         // _collider.radius = _radius + .5f;
    //         // * testing feedback action
    //         if (_action)
    //             // _action.Initialize(_target, _radius, ActionColor());
    //             _action.Initialize(transform, _radius, ActionColor());
    //         // {
    //         //     _action.transform.localScale = Vector2.one * _radius * 2f;
    //         //     _action.transform.GetComponent<SpriteRenderer>().color = ActionColor();
    //         // }
    //         return false;
    //     }
    //     // no approach even if fail ?
    //     target.GetComponent<base_interact>().TryAction(null);
    //     return true;
    // }
    // // false - far > approach | true - done > hold
    // public bool TryPickup(Transform target)
    // {
    //     if (Vector3.Distance(transform.position, target.position) > _radius + .5f)
    //     {
    //         // enable pickup
    //         // _target = target;
    //         // _flag = 2;
    //         // _collider.radius = _radius + .5f;
    //         // * testing feedback action
    //         if (_action)
    //             // _action.Initialize(_target, _radius, ActionColor());
    //             _action.Initialize(transform, _radius, ActionColor());
    //         return false;
    //     }
    //     SetEquipped(target.GetComponent<base_item>());
    //     return true;
    // }
    // // false - far > approach | true - done > hold
    // public bool TryUse(Transform target = null)
    // {
    //     if (!target)
    //     {
    //         // // get forward facing target
    //         // RaycastHit2D hit = Physics2D.Raycast(_motor.Position, input_touch.Instance.CacheDragRight());
    //         // if (hit)
    //         //     _equipped.Use(hit.transform, this);
    //         // print("why");
    //         _equipped?.Use(this);
    //     }
    //     else
    //     {
    //         if (Vector3.Distance(transform.position, target.position) > (_equipped ? _equipped.Radius : _radius) + .5f)
    //         {
    //             // enable use
    //             // _target = target;
    //             // _flag = 3;
    //             // _collider.radius = (_equipped ? _equipped.Radius : _radius) + .5f;
    //             // * testing feedback action
    //             if (_action)
    //                 // _action.Initialize(_target, _equipped.Radius, ActionColor());
    //                 _action.Initialize(transform, (_equipped ? _equipped.Radius : _radius), ActionColor());
    //             return false;
    //         }
    //         _equipped?.Use(this, target);
    //     }
    //     return true;
    // }
    // * testing feedback action
    public Color ActionColor()
    {
        if (!_equipped)
            return game_variables.Instance.ColorDefault;
        if (_equipped.gameObject.layer == game_variables.Instance.LayerInteract)
            return game_variables.Instance.ColorInteract;
        else if (_equipped.gameObject.layer == game_variables.Instance.LayerItem)
            return game_variables.Instance.ColorItem;
        else if (_equipped.gameObject.layer == game_variables.Instance.LayerMob)
            return game_variables.Instance.ColorEntity;
        else if (_equipped.gameObject.layer == game_variables.Instance.LayerProp)
            return game_variables.Instance.ColorProp;
        return game_variables.Instance.ColorDefault;
    }
    #endregion
    #region Properties
    public int Health
    {
        get { return _health; }
    }
    public int HealthInst
    {
        get { return _healthInst; }
    }
    // * testing feedback
    // public GameObject Gib
    // {
    //     get { return _gib; }
    // }
    // public GameObject GibMini
    // {
    //     get { return _gibMini; }
    // }
    public base_item Equipped
    {
        get { return _equipped; }
    }
    public List<Transform> Hostiles
    {
        get { return _hostiles; }
    }
    // public int Inventory
    // {
    //     get { return _inventory; }
    // }
    public bool ModeInvisible
    {
        get { return _invisible; }
    }
    // public int[] Collectibles
    // {
    //     get { return _collectibles; }
    // }
    public int Inventory
    {
        get { return _collectibles[1]; }
    }
    // public Transform Target
    // {
    //     get { return _target; }
    // }
    public float RadiusAction
    {
         get { return _radius; }
    }
    #endregion
}
