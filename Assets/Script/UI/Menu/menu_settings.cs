using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class menu_settings : MonoBehaviour
{
    public static menu_settings Instance;
    // Basic
    public Slider _durationPopup;
    public Text _durationPopupText;
    public Slider _sensitivityCamera;
    public Text _sensitivityCameraText;
    public Slider _sensitivityMotor;
    public Text _sensitivityMotorText;
    public Dropdown _chirality;
    public Dropdown _camera;
    public Dropdown _motor;
    public Dropdown _displayBattery;
    public Dropdown _vibration;
    public Slider _environmentSounds;
    public Text _environmentSoundsText;
    public Slider _enemySounds;
    public Text _enemySoundsText;
    public Dropdown _language;
    // public Button _reset;
    // Developer
    public Slider _sizeCamera;
    public Text _sizeCameraText;
    // public Slider _durationTap;
    // public Text _durationTapText;
    public Dropdown _pinchZoom;
    public Slider _sizeFont;
    public Text _sizeFontText;
    public Dropdown _typeset;
    public Slider _depth;
    public Text _depthText;
    // 
    public List<GameObject> _developerOnly;
    public RectTransform _rect;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        // link up all options to game_variables instance 
        _durationPopup.onValueChanged.AddListener(delegate { SetDurationPopup(); });
        _sensitivityCamera.onValueChanged.AddListener(delegate { SetSensitivityCamera(); });
        _sensitivityMotor.onValueChanged.AddListener(delegate { SetSensitivityMotor(); });
        _chirality.onValueChanged.AddListener(delegate { SetChirality(); });
        _camera.onValueChanged.AddListener(delegate { SetCamera(); });
        _motor.onValueChanged.AddListener(delegate { SetMotor(); });
        _displayBattery.onValueChanged.AddListener(delegate { SetDisplayBattery(); });
        _vibration.onValueChanged.AddListener(delegate { SetVibration(); });
        _environmentSounds.onValueChanged.AddListener(delegate { SetEnvironmentSounds(); });
        _enemySounds.onValueChanged.AddListener(delegate { SetEnemySounds(); });
        _language.onValueChanged.AddListener(delegate { SetLanguage(); });
        // _reset.onClick.AddListener(delegate { Reset(); });
        _sizeCamera.onValueChanged.AddListener(delegate { SetSizeCamera(); });
        // _durationTap.onValueChanged.AddListener(delegate { SetDurationTap(); });
        _pinchZoom.onValueChanged.AddListener(delegate { SetPinchZoom(); });
        _sizeFont.onValueChanged.AddListener(delegate { SetSizeFont(); });
        _typeset.onValueChanged.AddListener(delegate { SetTypeset(); });
        _depth.onValueChanged.AddListener(delegate { SetDepth(); });
    }
    private void OnEnable()
    {
        if (game_variables.Instance)
            Reload();
    }
    void Update()
    {
        _durationPopupText.text = "" + _durationPopup.value;
        _sensitivityCameraText.text = "" + _sensitivityCamera.value;
        _sensitivityMotorText.text = "" + _sensitivityMotor.value;
        _environmentSoundsText.text = "" + _environmentSounds.value;
        _enemySoundsText.text = "" + _enemySounds.value;
        // 
        _sizeCameraText.text = "" + _sizeCamera.value;
        // _durationTapText.text = "" + _durationTap.value;
        _sizeFontText.text = "" + _sizeFont.value;
        _depthText.text = "" + _depth.value;
        foreach (GameObject setting in _developerOnly)
            setting.SetActive(game_master.Instance.IsDeveloper);
        _rect.sizeDelta = new Vector2(0, game_master.Instance.IsDeveloper ? 20 * (12 + _developerOnly.Count) + 10 : 20 * 12 + 10);
    }
    public void Reload()
    {
        _durationPopup.value = game_variables.Instance.DurationPopup;
        _sensitivityCamera.value = game_variables.Instance.SensitivityCamera;
        _sensitivityMotor.value = game_variables.Instance.SensitivityMotor;
        _chirality.value = game_variables.Instance.Chirality_Int;
        _camera.value = game_variables.Instance.Camera_Int;
        _motor.value = game_variables.Instance.Motor_Int;
        _displayBattery.value = game_variables.Instance.DisplayBattery;
        _vibration.value = game_variables.Instance.Vibration;
        _environmentSounds.value = game_variables.Instance.EnvironmentSounds;
        _enemySounds.value = game_variables.Instance.EnemySounds;
        _language.value = game_variables.Instance.Language;
        // 
        _sizeCamera.value = game_variables.Instance.SizeCamera;
        // _durationTap.value = game_variables.Instance.DurationTap;
        _pinchZoom.value = game_variables.Instance.PinchZoom;
        _sizeFont.value = game_variables.Instance.SizeFont;
        _typeset.value = game_variables.Instance.Typeset;
        _depth.value = game_variables.Instance.Depth;
    }
    #region Setters [Wrappers]
    public void SetDurationPopup()
    {
        game_variables.Instance.SetDurationPopup(_durationPopup.value);
    }
    public void SetSensitivityCamera()
    {
        game_variables.Instance.SetSensitivityCamera(_sensitivityCamera.value);
    }
    public void SetSensitivityMotor()
    {
        game_variables.Instance.SetSensitivityMotor(_sensitivityMotor.value);
    }
    public void SetChirality()
    {
        game_variables.Instance.SetChirality(_chirality.value);
    }
    public void SetCamera()
    {
        game_variables.Instance.SetCamera(_camera.value);
    }
    public void SetMotor()
    {
        game_variables.Instance.SetMotor(_motor.value);
    }
    public void SetDisplayBattery()
    {
        game_variables.Instance.SetDisplayBattery(_displayBattery.value);
    }
    public void SetVibration()
    {
        game_variables.Instance.SetVibration(_vibration.value);
    }
    public void SetEnvironmentSounds()
    {
        game_variables.Instance.SetEnvironmentSounds(_environmentSounds.value);
    }
    public void SetEnemySounds()
    {
        game_variables.Instance.SetEnemySounds(_enemySounds.value);
    }
    public void SetLanguage()
    {
        game_variables.Instance.SetLanguage(_language.value);
    }
    public void SetSizeCamera()
    {
        game_variables.Instance.SetSizeCamera(_sizeCamera.value);
    }
    // public void SetDurationTap()
    // {
    //     game_variables.Instance.SetDurationTap(_durationTap.value);
    // }
    public void SetPinchZoom()
    {
        game_variables.Instance.SetPinchZoom(_pinchZoom.value);
    }
    public void SetSizeFont()
    {
        game_variables.Instance.SetSizeFont(_sizeFont.value);
    }
    public void SetTypeset()
    {
        game_variables.Instance.SetTypeset(_typeset.value);
    }
    public void SetDepth()
    {
        game_variables.Instance.SetDepth(Mathf.FloorToInt(_depth.value));
    }
    public void Load()
    {
        game_variables.Instance.Load();
    }
    #endregion
}
