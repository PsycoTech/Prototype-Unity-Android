using UnityEngine;
public class game_master : MonoBehaviour
{
    public static game_master Instance;
    // private bool _isSave;
    private base_state _state;
    private bool _isDeveloper;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
        // // * testing
        // _isSave = false;
        _state = new base_state();
        _isDeveloper = false;
    }
    void Start()
    {
        manager_ui.Instance.SetMain(true);
        // if (!manager_state.Instance.IsSave)
        //     Load();
    }
    // update instance to default
    public void New()
    {
        // * testing ? transition
        manager_ui.Instance.SetMain(false);
        controller_player.Instance.Data.New();
        // controller_player.Instance.Data.Load();
        // controller_player.Instance.Motor.ToSpawn();
        controller_player.Instance.Motor.New();
        controller_player.Instance.Anim.New();
        game_camera.Instance.SnapToPosition(controller_player.Instance.Motor.Spawn);
        // 
        manager_mob.Instance.New();
        manager_item.Instance.New();
        manager_interact.Instance.New();
        manager_proximity.Instance.New();
        manager_react.Instance.New();
        manager_prop.Instance.New();
        manager_chunk.Instance.Initialize();
    }
    // create|update save file to instance
    public void Save()
    {
        // 
        // _isSave = true;
        // manager_mob.Instance.Save();
        // // * testing ? sequence
        // controller_player.Instance.Data.Save();
        // controller_player.Instance.Motor.Save();
        // controller_player.Instance.Anim.Save();
        // // 
        // manager_mob.Instance.Save();
        // manager_item.Instance.Save();
        // manager_interact.Instance.Save();
        // manager_proximity.Instance.Save();
        // manager_react.Instance.Save();
        // manager_prop.Instance.Save();
        // manager_chunk.Instance.Save();
        game_variables.Instance.Save();
    }
    // update instance to save file
    public void Load()
    {
        // // * testing ? transition
        // manager_ui.Instance.SetMain(false);
        // controller_player.Instance.Data.HealthRestore();
        // // controller_player.Instance.Data.Load();
        // // controller_player.Instance.Motor.ToSpawn();
        // controller_player.Instance.Motor.Load();
        // // controller_player.Instance.Anim.Load(_state);
        // controller_player.Instance.Anim.Load();
        // game_camera.Instance.SnapToPosition(controller_player.Instance.Motor.Spawn);
        // // 
        // manager_mob.Instance.Load();
        // manager_item.Instance.Load();
        // manager_interact.Instance.Load();
        // manager_proximity.Instance.Load();
        // manager_react.Instance.Load();
        // manager_prop.Instance.Load();
        // manager_chunk.Instance.Initialize();
        // manager_chunk.Instance.Load();
        game_variables.Instance.Load();
    }
    // ad | resurrect
    public void LifeRestore(int scale = 0)
    {
        // * testing
        manager_ui.Instance.SetMain(false);
        controller_player.Instance.Data.HealthRestore(scale * 3);
    }
    // public bool IsSave
    // {
    //     get { return _isSave; }
    // }
    public void ToggleDeveloper()
    {
        _isDeveloper = !_isDeveloper;
    }
    public bool IsDeveloper
    {
        get { return _isDeveloper; }
    }
}
