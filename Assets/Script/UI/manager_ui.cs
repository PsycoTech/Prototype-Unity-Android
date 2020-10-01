using UnityEngine;
public class manager_ui : MonoBehaviour
{
    public static manager_ui Instance;
    public GameObject menu_main;
    public GameObject menu_continue;    // ad payment menu
    public GameObject menu_controls;
    public GameObject menu_settings;
    public GameObject menu_escaped;
    public GameObject menu_credits;
    public GameObject button_save;
    // * testing
    protected bool[] _state;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _state = new bool[6];
        ApplyState();
    }
    void ApplyState()
    {
        if (menu_main.activeSelf != _state[0])
            menu_main.SetActive(_state[0]);
        if (menu_continue.activeSelf != _state[1])
            menu_continue.SetActive(_state[1]);
        if (menu_controls.activeSelf != _state[2])
            menu_controls.SetActive(_state[2]);
        if (menu_settings.activeSelf != _state[3])
            menu_settings.SetActive(_state[3]);
        if (menu_escaped.activeSelf != _state[4])
            menu_escaped.SetActive(_state[4]);
        if (menu_credits.activeSelf != _state[5])
            menu_credits.SetActive(_state[5]);
        button_save.SetActive(!_state[0]);
    }
    // * testing ? transition (dis)allow raycast
    public void SetMain(bool value)
    {
        _state[0] = value;
        _state[1] = false;
        _state[2] = false;
        _state[4] = false;
        _state[5] = false;
        ApplyState();
        // * testing iframes
        controller_player.Instance.Data.SetIframes();
    }
    public void ToggleContinue()
    {
        _state[1] = !_state[1];
        ApplyState();
    }
    public void ToggleControls()
    {
        _state[2] = !_state[2];
        ApplyState();
    }
    public void ToggleSettings()
    {
        _state[3] = !_state[3];
        ApplyState();
    }
    public void ToggleEscaped()
    {
        _state[4] = !_state[4];
        ApplyState();
    }
    public void ToggleCredits()
    {
        _state[5] = !_state[5];
        ApplyState();
    }
    // * testing
    public bool IsOverlay
    {
        get { return _state[0] || _state[2] || _state[3] || _state[4] || _state[5]; }
    }
}
