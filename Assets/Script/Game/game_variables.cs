using UnityEngine;
public class game_variables : MonoBehaviour
{
    public static game_variables Instance;
    // Basic 
    // private float _durationAutosave;
    private float _durationPopup;
    private float _sensitivityCamera;
    private float _sensitivityMotor;
    private int _chirality;
    private int _camera;
    private int _motor;
    private int _displayBattery;
    private int _vibration;
    private float _environmentSounds;
    private float _enemySounds;
    private int _language;
    // Developer
    // private float _radiusBT;
    // private float _radiusSensors;
    private float _sizeCamera;
    // private float _durationTap;
    private int _pinchZoom;
    private int _sizeFont;
    private int _typeset;
    private int _depth;
    // Experimental
    // private float _radiusSprite;
    private GameObject _particleDeath;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        // Initialize Variables 
        // _durationAutosave = 3 * 60;
        _durationPopup = 3f;
        _sensitivityCamera = 20f;
        _sensitivityMotor = 20f;
        _chirality = 0;
        _camera = 0;
        _motor = 0;
        _displayBattery = 0;
        _vibration = 0;
        _environmentSounds = 50f;
        _enemySounds = 50f;
        _language = 0;
        // ?
        // _radiusSprite = 12.5f;
        // _radiusSprite = 40f;
        // _radiusSprite = 12f;
        // _radiusBT = 17.5f;
        // _radiusSensors = 20f;
        //    
        _sizeCamera = 7f;
        // _durationTap = .2f;
        _pinchZoom = 1;
        _sizeFont = 14;
        _typeset = 0;
        _depth = 2;
        if (menu_settings.Instance)
            menu_settings.Instance.Reload();
        _particleDeath = Resources.Load("Prefab/Particle/death") as GameObject;
    }
    public void Save()
    {
        PlayerPrefs.SetInt("save", 1);
        PlayerPrefs.SetFloat("durationPopup", _durationPopup);
        PlayerPrefs.SetFloat("sensitivityCamera", _sensitivityCamera);
        PlayerPrefs.SetFloat("sensitivityMotor", _sensitivityMotor);
        PlayerPrefs.SetInt("chirality", _chirality);
        PlayerPrefs.SetInt("camera", _camera);
        PlayerPrefs.SetInt("motor", _motor);
        PlayerPrefs.SetInt("displayBattery", _displayBattery);
        PlayerPrefs.SetInt("vibration", _vibration);
        PlayerPrefs.SetFloat("environmentSounds", _environmentSounds);
        PlayerPrefs.SetFloat("enemySounds", _enemySounds);
        PlayerPrefs.SetInt("language", _language);
        // PlayerPrefs.SetFloat("radiusSprite", _radiusSprite);
        PlayerPrefs.SetFloat("sizeCamera", _sizeCamera);
        // PlayerPrefs.SetFloat("durationTap", _durationTap);
        PlayerPrefs.SetInt("pinchZoom", _pinchZoom);
        PlayerPrefs.SetInt("sizeFont", _sizeFont);
        PlayerPrefs.SetInt("typeset", _typeset);
        PlayerPrefs.SetInt("depth", _depth);
    }
    public void Load()
    {
        PlayerPrefs.SetInt("save", 1);
        _durationPopup = PlayerPrefs.GetFloat("durationPopup", 3f);
        _sensitivityCamera = PlayerPrefs.GetFloat("sensitivityCamera", 20f);
        _sensitivityMotor = PlayerPrefs.GetFloat("sensitivityMotor", 20f);
        _chirality = PlayerPrefs.GetInt("chirality", 0);
        _camera = PlayerPrefs.GetInt("camera", 0);
        _motor = PlayerPrefs.GetInt("motor", 0);
        _displayBattery = PlayerPrefs.GetInt("displayBattery", 0);
        _vibration = PlayerPrefs.GetInt("vibration", 0);
        _environmentSounds = PlayerPrefs.GetFloat("environmentSounds", 50f);
        _enemySounds = PlayerPrefs.GetFloat("enemySounds", 50f);
        _language = PlayerPrefs.GetInt("language", 0);
        // _radiusSprite = PlayerPrefs.GetFloat("radiusSprite", 12f);
        _sizeCamera = PlayerPrefs.GetFloat("sizeCamera", 7f);
        // _durationTap = PlayerPrefs.GetFloat("durationTap", .2f);
        _pinchZoom = PlayerPrefs.GetInt("pinchZoom", 0);
        _sizeFont = PlayerPrefs.GetInt("sizeFont", 0);
        _typeset = PlayerPrefs.GetInt("typeset", 0);
        _depth = PlayerPrefs.GetInt("depth", 2);
        if (menu_settings.Instance)
            menu_settings.Instance.Reload();
    }
    #region Setters
    // public void SetDurationAutosave(float value)
    // {
    //     _durationAutosave = value * 60;
    // }
    public void SetDurationPopup(float value)
    {
        _durationPopup = value;
    }
    public void SetSensitivityCamera(float value)
    {
        _sensitivityCamera = value;
    }
    public void SetSensitivityMotor(float value)
    {
        _sensitivityMotor = value;
    }
    public void SetChirality(int value)
    {
        _chirality = value;
    }
    public void SetCamera(int value)
    {
        _camera = value;
    }
    public void SetMotor(int value)
    {
        _motor = value;
    }
    public void SetDisplayBattery(int value)
    {
        _displayBattery = value;
    }
    public void SetVibration(int value)
    {
        _vibration = value;
    }
    public void SetEnvironmentSounds(float value)
    {
        _environmentSounds = value;
    }
    public void SetEnemySounds(float value)
    {
        _enemySounds = value;
    }
    public void SetLanguage(int value)
    {
        _language = value;
    }
    // Developer
    public void SetSizeCamera(float value)
    {
        _sizeCamera = value;
        // 
        game_camera.Instance.CameraMain.orthographicSize = value;
    }
    // public void SetDurationTap(float value)
    // {
    //     _durationTap = value;
    // }
    public void SetPinchZoom(int value)
    {
        _pinchZoom = value;
    }
    public void SetSizeFont(float value)
    {
        // ?
        _sizeFont = (int)value;
    }
    public void SetTypeset(int value)
    {
        _typeset = value;
    }
    public void SetDepth(int value)
    {
        _depth = value;
        // 
        manager_chunk.Instance.Initialize();
    }
    #endregion
    #region Properties [Basic]
    // public float DurationAutosave
    // {
    //     get { return _durationAutosave; }
    // }
    public float DurationPopup
    {
        get { return _durationPopup; }
    }
    public float SensitivityCamera
    {
        get { return _sensitivityCamera; }
    }
    public float SensitivityMotor
    {
        get { return _sensitivityMotor; }
    }
    public bool Chirality
    {
        get { return _chirality == 1; }
    }
    public int Chirality_Int
    {
        get { return _chirality; }
    }
    public int Camera
    {
        get { return _camera == 0 ? 1 : -1; }
    }
    public int Camera_Int
    {
        get { return _camera; }
    }
    public int Motor
    {
        get { return _motor == 0 ? 1 : -1; }
    }
    public int Motor_Int
    {
        get { return _motor; }
    }
    public int DisplayBattery
    {
        get { return _displayBattery; }
    }
    public int Vibration
    {
        get { return _vibration; }
    }
    public float EnvironmentSounds
    {
        get { return _environmentSounds; }
    }
    public float EnemySounds
    {
        get { return _enemySounds; }
    }
    public int Language
    {
        get { return _language; }
    }
    // * testing
    public Color ColorDefault
    {
        get { return Color.white; }
    }
    public Color ColorDamage
    {
        get { return Color.red; }
    }
    public Color ColorInteract
    {
        get { return Color.yellow; }
    }
    public Color ColorItem
    {
        get { return Color.cyan; }
    }
    public Color ColorEntity
    {
        get { return Color.green; }
    }
    public Color ColorProp
    {
        get { return Color.blue; }
    }
    #endregion
    #region Properties [Developer]
    // public float RadiusSprite
    // {
    //     get { return _radiusSprite + .5f; }
    // }
    // public float RadiusBT
    // {
    //     get { return _radiusBT; }
    // }
    // public int RadiusBT_Int
    // {
    //     get { return (int)_radiusBT; }
    // }
    // public float RadiusSensors
    // {
    //     get { return _radiusSensors; }
    // }
    // public float ClockGrid
    // {
    //     get { return 1f; }
    // }
    // public float ClockMotor
    // {
    //     get { return 1f; }
    // }
    public float SizeCamera
    {
        get { return _sizeCamera; }
    }
    // public float DurationTap
    // {
    //     get { return _durationTap; }
    // }
    public int PinchZoom
    {
        get { return _pinchZoom; }
    }
    public int SizeFont
    {
        get { return _sizeFont; }
    }
    public int Typeset
    {
        get { return _typeset; }
    }
    public int Depth
    {
        get { return _depth; }
    }
    #endregion

    #region Properties [Layers]
    public int LayerSolid
    {
        get { return 0; }
    }
    public int LayerPlayer
    {
        get { return 8; }
    }
    public int LayerMob
    {
        get { return 9; }
    }
    public int LayerInteract
    {
        get { return 10; }
    }
    public int LayerItem
    {
        get { return 11; }
    }
    public int LayerFeedback
    {
        get { return 12; }
    }
    public int LayerFluid
    {
        get { return 13; }
    }
    public int LayerAvoid
    {
        get { return 14; }
    }
    public int LayerDamage
    {
        get { return 15; }
    }
    // public int LayerSensor
    // {
    //     get { return 16; }
    // }
    // public int LayerAction
    // {
    //     get { return 17; }
    // }
    public int LayerProp
    {
        get { return 18; }
    }
    public int LayerReact
    {
        get { return 19; }
    }
    public int LayerChunk
    {
        get { return 20; }
    }
    public LayerMask ScanLayerSolid
    {
        // get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Prop"); }
        get { return LayerMask.GetMask("Default"); }
    }
    public LayerMask ScanLayerPlayer
    {
        get { return LayerMask.GetMask("Player"); }
    }
    public LayerMask ScanLayerMob
    {
        get { return LayerMask.GetMask("Mob"); }
    }
    public LayerMask ScanLayerInteract
    {
        get { return LayerMask.GetMask("Interact"); }
    }
    public LayerMask ScanLayerItem
    {
        get { return LayerMask.GetMask("Item"); }
    }
    // public LayerMask ScanLayerDamage
    // {
    //     get { return LayerMask.GetMask("Damage"); }
    // }
    // public LayerMask ScanLayerDamageSolid
    // {
    //     get { return LayerMask.GetMask("DamageSolid"); }
    // }
    public LayerMask ScanLayerFluid
    {
        get { return LayerMask.GetMask("Fluid"); }
    }
    public LayerMask ScanLayerSensor
    {
        get { return LayerMask.GetMask("Sensor"); }
    }
    public LayerMask ScanLayerEntity
    {
        get { return LayerMask.GetMask("Player") | LayerMask.GetMask("Mob"); }
    }
    public LayerMask ScanLayerStatic
    {
        get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Prop"); }
        // get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Interact"); }
    }
    public LayerMask ScanLayerAction
    {
        get { return LayerMask.GetMask("Mob") | LayerMask.GetMask("Interact") | LayerMask.GetMask("Item"); }
        // get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Interact") | LayerMask.GetMask("Prop"); }
        // get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Prop"); }
    }
    public LayerMask ScanLayerObstruction
    {
        get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Interact") | LayerMask.GetMask("Item") | LayerMask.GetMask("Prop"); }
        // get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Interact") | LayerMask.GetMask("Prop"); }
        // get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Prop"); }
    }
    public LayerMask ScanLayerTarget
    {
        get { return LayerMask.GetMask("Player") | LayerMask.GetMask("Mob") | LayerMask.GetMask("Interact") | LayerMask.GetMask("Item"); }
    }
    public LayerMask ScanLayerTap
    {
        get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Player") | LayerMask.GetMask("Mob") | LayerMask.GetMask("Interact") | LayerMask.GetMask("Item") | LayerMask.GetMask("Prop"); }
    }
    public LayerMask ScanLayerObject
    {
        // get { return LayerMask.GetMask("Mob") | LayerMask.GetMask("Interact") | LayerMask.GetMask("Item") | LayerMask.GetMask("Fluid") | LayerMask.GetMask("Sensor") | LayerMask.GetMask("Prop") | LayerMask.GetMask("React"); }
        get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Mob") | LayerMask.GetMask("Interact") | LayerMask.GetMask("Item") | LayerMask.GetMask("Fluid") | LayerMask.GetMask("Sensor") | LayerMask.GetMask("Prop") | LayerMask.GetMask("React"); }
        // get { return LayerMask.GetMask("Mob") | LayerMask.GetMask("Interact") | LayerMask.GetMask("Item") | LayerMask.GetMask("Prop"); }
        // get { return LayerMask.GetMask("Mob") | LayerMask.GetMask("Item") | LayerMask.GetMask("Fluid") | LayerMask.GetMask("Prop"); }
    }
    // // ???
    // public LayerMask ScanLayerNotWalkable
    // {
    //     get { return LayerMask.GetMask("Default") | LayerMask.GetMask("Prop"); }
    // }
    #endregion
    #region EXPERIMENTAL
    public GameObject ParticleDeath
    {
        get { return _particleDeath; }
    }
    #endregion
}
