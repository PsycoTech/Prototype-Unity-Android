using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
public enum TypeState
{
    // SETTINGS,
    MOB, 
    // PLAYER, 
    // ITEM, 
    // PROP, 
    // REACT, 
    // INTERACT, 
}
public class manager_state : MonoBehaviour
{
    public static manager_state Instance;
    // mob interact item prop react
    // List<state_mob> _mobs;
    // List<state_interact> _interacts;
    // List<state_item> _items;
    // List<state_prop> _props;
    // List<state_react> _reacts;
    // protected bool _isSave;
    void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
        // 
        // _mobs = new List<state_mob>();
        // _interacts = new List<state_interact>();
        // _items = new List<state_item>();
        // _props = new List<state_prop>();
        // _reacts = new List<state_react>();
        // _isSave = false;
        // base_state state = new base_state();
        // Load("settings", out state, TypeState.SETTINGS);
        // game_variables.Instance.Load();
        // _isSave = PlayerPrefs.GetInt("save", 0) == 0 ? false : true;
        // * testing
        Directory.CreateDirectory(Application.persistentDataPath + "/" + TypeState.MOB.ToString());
    }
    // void Start()
    // {
    //     // game_variables.Instance.IsSave;
    //     // _isSave = PlayerPrefs.GetInt("save", 0) == 0 ? false : true;
    // }
    public void Save(string id, base_state state, TypeState type)
    {
        // 
        FileStream file = new FileStream(Application.persistentDataPath + "/" + type.ToString() + "/" + id + ".dot", FileMode.OpenOrCreate);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, state);
        }
        catch (SerializationException e)
        {
            feedback_toaster.Instance.RegisterMessage(gameObject.name + " : " + e.Message, game_variables.Instance.ColorDefault);
        }
        finally
        {
            file.Close();
            game_variables.Instance.Save();
        }
    }
    public void Load(string id, out base_state state, TypeState type)
    {
        // 
        state = new base_state();
        FileStream file = new FileStream(Application.persistentDataPath + "/" + type.ToString() + "/" + id + ".dot", FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            state = (base_state)formatter.Deserialize(file);
        }
        catch (SerializationException e)
        {
            feedback_toaster.Instance.RegisterMessage(gameObject.name + " : " + e.Message, game_variables.Instance.ColorDefault);
        }
        finally
        {
            file.Close();
            game_variables.Instance.Load();
        }
    }
    public bool IsSave
    {
        get { return false; }
        // get { return PlayerPrefs.GetInt("save", 0) == 0 ? false : true; }
        // get { return _isSave; }
    }
}
