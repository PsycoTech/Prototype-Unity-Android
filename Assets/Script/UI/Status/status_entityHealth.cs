using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class status_entityHealth : MonoBehaviour
{
    // public static menu_status
    public static status_entityHealth Instance;
    private List<Image> _hearts;
    private int _segments;
    private GameObject _heart;
    private float _sizeHeart;
    private float _sizePanel;
    private RectTransform _panel;
    void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
        _hearts = new List<Image>();
        _heart = transform.GetChild(0).gameObject;
        _panel = GetComponent<RectTransform>();
        // foreach (Transform child in transform)
        // {
        //     _hearts.Add(child.GetChild(0).GetComponent<Image>());
        //     _hearts[_hearts.Count - 1].fillAmount = 1f;
        // }
        _segments = 3;
        _sizeHeart = 30f;
    }
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        foreach (Image heart in _hearts)
            heart.transform.parent?.GetComponent<SelfDestruct>().Trigger();
        _hearts.Clear();
        _sizePanel = 0f;
        // // * testing ? host change
        // _segments = controller_player.Instance.Data.Health / _hearts.Count;
        for (int i = controller_player.Instance.Data.Health / _segments; i > 0; i--)
        {
            Transform temp = Instantiate(_heart, transform.position, transform.rotation).transform;
            temp.SetParent(transform);
            _hearts.Add(temp.GetChild(0).GetComponent<Image>());
            _hearts[_hearts.Count - 1].fillAmount = 1f;
            temp.gameObject.SetActive(true);
            _sizePanel += _sizeHeart;
        }
        _panel.sizeDelta = new Vector2(_sizePanel + _hearts.Count, _sizeHeart);
    }
    void Update()
    {
        for (int i = _hearts.Count - 1; i > -1; i--)
            if (controller_player.Instance.Data.HealthInst > i * _segments)
                _hearts[i].fillAmount = (controller_player.Instance.Data.HealthInst - (i * _segments)) / (float)_segments;
            else
                _hearts[i].fillAmount = 0f;
    }
}
