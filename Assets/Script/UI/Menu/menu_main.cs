using UnityEngine;
using UnityEngine.UI;
public class menu_main : MonoBehaviour
{
    public GameObject _continue;
    private Image _bg;
    protected bool _hasUpdated;
    void Awake()
    {
        _bg = GetComponent<Image>();
        _hasUpdated = false;
    }
    void Update()
    {
        if (_hasUpdated)
            return;
        _hasUpdated = true;
        _bg.color = controller_player.Instance?.Data.HealthInst > 0 ? new Color(0, 0, 0, .75f) : new Color(1, 0, 0, .5f);
    }
    void OnEnable()
    {
        if (manager_state.Instance)
            _continue.SetActive(manager_state.Instance.IsSave);
        _hasUpdated = false;
    }
}
