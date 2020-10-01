using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// public class status_inventory : MonoBehaviour
public class menu_inventory : MonoBehaviour
{
    // private List<Image> _hearts = new List<Image>();
    // private int _segments;
    // private GameObject _heart;
    // private float _sizeHeart;
    // private float _sizePanel;
    // private RectTransform _panel;
    public static menu_inventory Instance;
    protected GameObject _pouch;
    // public Text _status;
    protected List<Pouch> _equipment;
    // protected int _size;
    protected struct Pouch
    {
        public Transform Slot;
        public base_item Item;
        public Text Status;
    }
    protected int _equipped;
    // protected state_inventory _state;
    // protected int _slots;
    // void Awake()
    // {
    //     // _heart = transform.GetChild(0).gameObject;
    //     // _panel = GetComponent<RectTransform>();
    //     // // foreach (Transform child in transform)
    //     // // {
    //     // //     _hearts.Add(child.GetChild(0).GetComponent<Image>());
    //     // //     _hearts[_hearts.Count - 1].fillAmount = 1f;
    //     // // }
    //     // _segments = 3;
    //     // _sizeHeart = 30f;
    //     // _sizePanel = 0f;
    // }
    // void Start()
    // {
    //     Initialize();
    // }
    // protected void Initialize()
    // {
    //     // // // * testing ? host change
    //     // // _segments = controller_player.Instance.Data.Health / _hearts.Count;
    //     // for (int i = controller_player.Instance.Data.Health / _segments; i > 0; i--)
    //     // {
    //     //     Transform temp = Instantiate(_heart, transform.position, transform.rotation).transform;
    //     //     temp.SetParent(transform);
    //     //     _hearts.Add(temp.GetChild(0).GetComponent<Image>());
    //     //     _hearts[_hearts.Count - 1].fillAmount = 1f;
    //     //     temp.gameObject.SetActive(true);
    //     //     _sizePanel += _sizeHeart;
    //     // }
    //     // _panel.sizeDelta = new Vector2(_sizePanel + _hearts.Count, _sizeHeart);
    // }
    void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
        _equipment = new List<Pouch>();
        _equipped = 0;
        _pouch = transform.GetChild(0).gameObject;
    }
    // // secondary to player controller ?
    // public void New()
    // {
    //     // 
    //     _size = 1;
    // }
    // public void Save()
    // {
    //     // 
    // }
    // public void Load()
    // {
    //     // 
    // }
    // void Start()
    // {
    //     // if (controller_player);
    // }
    // void Update()
    // {
    //     // foreach (base_item item in controller_player.Instance.Data.Equipment)
    //     for (int i = 0; i < controller_player.Instance.Data.Inventory.Count; i++)
    //     {
    //         if (controller_player.Instance.Data.Inventory[i])
    //         {
    //             // 
    //             // _equipment[i]
    //         }
    //         else
    //         {
    //             // 
    //         }
    //         // _status.text = "-";
    //         return;
    //     }
    //     // // _status.text = "Equipped : " + controller_player.Instance.Data.Equipped.name;
    //     // _status.text = controller_player.Instance.Data.Equipped.name;
    //     // int health = controller_player.Instance.Data.Equipped.HealthInst;
    //     // _status.text += "\n";
    //     // while (health > 0)
    //     // {
    //     //     _status.text += "|";
    //     //     health--;
    //     // }
    //     // if (controller_player.Instance.Data.)
    //     // for (int i = _hearts.Count - 1; i > -1; i--)
    //     //     if (controller_player.Instance.Data.HealthInst > i * _segments)
    //     //         _hearts[i].fillAmount = (controller_player.Instance.Data.HealthInst - (i * _segments)) / (float)_segments;
    //     //     else
    //     //         _hearts[i].fillAmount = 0f;
    // }
    // public void ItemAdd(GameObject item)
    // {
    //     Pouch pouch = new Pouch();
    //     pouch.Item = item;
    //     pouch.Status = Instantiate(_status, transform.position, transform.rotation).GetComponent<Text>();
    //     _equipment.Add(pouch);
    //     _equipment[_equipment.Count - 1].Status.transform.parent = transform;
    // }
    void Update()
    {
        foreach (Pouch pouch in _equipment)
        {
            pouch.Status.text = (pouch.Item == controller_player.Instance.Data.Equipped ? "*" : "") + pouch.Item.name + "\n";
            for (int i = pouch.Item.Uses; i > 0; i--)
                pouch.Status.text += "|";
        }
    }
    // // null - remove drop | else - add equip
    // public void SetEquipped(base_item item)
    // {
    //     // drop
    //     if (item == null)
    //     {
    //         // unequipped
    //         if (!controller_player.Instance.Data.Equipped)
    //             // ignore
    //             return;
    //         // // player
    //         // if (gameObject.layer == game_variables.Instance.LayerPlayer)
    //         // {
    //         //     // remove
    //         //     feedback_toaster.Instance.RegisterMessage(gameObject.name + " : drop " + controller_player.Instance.Data.Equipped?.gameObject.name, game_variables.Instance.ColorItem);
    //         //     InventoryModify(controller_player.Instance.Data.Equipped, false);
    //         // }
    //         // clear reference
    //         // controller_player.Instance.Data.Equipped.SetParent(null);
    //         // controller_player.Instance.Data.Equipped.SetActive(true);
    //         // controller_player.Instance.Data.SetEquipped(null);
    //     }
    //     // pickup
    //     else
    //     {
    //         // equipped
    //         if (controller_player.Instance.Data.Equipped)
    //         {
    //             // // space or match
    //             // if (IsEmptyOrContains(item))
    //                 // // holster ?
    //                 // controller_player.Instance.Data.Equipped.SetActive(false);
    //                 // ignore
    //                 // return;
    //             // // player
    //             // if (gameObject.layer == game_variables.Instance.LayerPlayer)
    //             // {
    //             //     // remove
    //             //     feedback_toaster.Instance.RegisterMessage(gameObject.name + " : drop " + controller_player.Instance.Data.Equipped?.gameObject.name, game_variables.Instance.ColorItem);
    //             //     InventoryModify(controller_player.Instance.Data.Equipped, false);
    //             // }
    //             // clear reference
    //             // controller_player.Instance.Data.Equipped.SetParent(null);
    //             // controller_player.Instance.Data.Equipped.SetActive(true);
    //             controller_player.Instance.Data.SetEquipped(null);
    //         }
    //         // else if (menu_inventory.Instance.IsEmptyOrContains(item))
    //         // {
    //         //     // 
    //         // }
    //         // // player
    //         // if (gameObject.layer == game_variables.Instance.LayerPlayer)
    //         // {
    //         //     feedback_toaster.Instance.RegisterMessage(gameObject.name + " : equip " + item.gameObject.name, game_variables.Instance.ColorItem);
    //         //     // menu_inventory.Instance.SetEquipped(item, true);
    //         //     // controller_player.Instance.Data.SetEquipped(item);
    //         //     InventoryModify(item, true);
    //         // }
    //         // store reference
    //         controller_player.Instance.Data.SetEquipped(item);
    //         // controller_player.Instance.Data.Equipped.SetActive(true);
    //         // controller_player.Instance.Data.Equipped.SetParent(controller_player.Instance.Motor._anchor);
    //     }
    // }
    // true - add | false - remove
    public bool Modify(base_item item, bool equip)
    {
        Pouch remove = new Pouch();
        for (int i = 0; i < _equipment.Count; i++)
        {
            if (_equipment[i].Item == item)
            {
                if (equip)
                {
                    // match - duplicate
                    feedback_toaster.Instance.RegisterMessage("Equip : " + item.gameObject.name, game_variables.Instance.ColorItem);
                    return true;
                }
                else
                    // match - remove
                    remove = _equipment[i];
                break;
            }
        }
        if (_equipment.Contains(remove))
        {
            _equipment.Remove(remove);
            // drop item
            feedback_toaster.Instance.RegisterMessage("Drop : " + remove.Item.gameObject.name, game_variables.Instance.ColorItem);
            remove.Slot.GetComponent<SelfDestruct>().Trigger();
            return true;
        }
        if (equip)
        {
            // if (_equipment.Count < 5)
            if (IsEmpty)
            {
                // no match, space - add
                Pouch pouch = new Pouch();
                pouch.Slot = Instantiate(_pouch, transform.position, transform.rotation).transform;
                pouch.Slot.SetParent(transform);
                pouch.Slot.gameObject.SetActive(true);
                pouch.Slot.GetComponent<button_pouch>().ID = _equipment.Count;
                pouch.Item = item;
                pouch.Status = pouch.Slot.GetChild(0).GetComponent<Text>();
                _equipment.Add(pouch);
                feedback_toaster.Instance.RegisterMessage("Equip : " + item.gameObject.name, game_variables.Instance.ColorItem);
                return true;
            }
            else
                // full - warning
                feedback_toaster.Instance.RegisterMessage("Inventory Full - Cannot Equip Item", game_variables.Instance.ColorDefault);
            // {
            //     for (int i = 0; i < _equipment.Count; i++)
            //     {
            //         if (_equipment[i].Item == controller_player.Instance.Data.Equipped)
            //         {
            //             // full, equipped - replace
            //             feedback_toaster.Instance.RegisterMessage("Drop : " + _equipment[i].Item.gameObject.name, game_variables.Instance.ColorItem);
            //             Pouch pouch = new Pouch();
            //             pouch.Slot = _equipment[i].Slot;
            //             pouch.Item = item;
            //             pouch.Status = _equipment[i].Status;
            //             // _equipment[i].Item.SetActive(true);
            //             _equipment[i] = pouch;
            //             feedback_toaster.Instance.RegisterMessage("Equip : " + item.gameObject.name, game_variables.Instance.ColorItem);
            //             return true;
            //         }
            //     }
            // }
        }
        return false;
    }
    public void Drop(int id)
    {
        // ehhh
        if (id > -1 && id < _equipment.Count)
            controller_player.Instance.Data.Drop(_equipment[id].Item);
    }
    public void SetEquipped(int id)
    {
        if (id > -1 && id < _equipment.Count)
            controller_player.Instance.Data.SetEquipped(_equipment[id].Item);
    }
    // public bool IsEmptyOrContains(base_item item)
    // {
    //     // if (_equipment.Count < 5)
    //     if (_equipment.Count < controller_player.Instance.Data.Inventory)
    //         return true;
    //     for (int i = 0; i < _equipment.Count; i++)
    //         if (_equipment[i].Item == item)
    //             return true;
    //     return false;
    // }
    public bool Contains(base_item item)
    {
        for (int i = 0; i < _equipment.Count; i++)
            if (_equipment[i].Item == item)
                return true;
        return false;
    }
    public bool IsEquipped(int id)
    {
        if (id > -1 && id < _equipment.Count)
            return controller_player.Instance.Data.Equipped == _equipment[id].Item;
        return false;
    }
    // public void Unequip()
    // {
    //     // 
    // }
    public bool IsEmpty
    {
        get { return _equipment.Count < controller_player.Instance.Data.Inventory; }
    }
}
