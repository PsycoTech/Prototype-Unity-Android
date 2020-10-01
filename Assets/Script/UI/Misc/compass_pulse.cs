using UnityEngine;
using UnityEngine.UI;
public class compass_pulse : MonoBehaviour
{
    public float _time = 1f;
    private float _timer;
    private Image _sprite;
    void Awake()
    {
        _sprite = GetComponent<Image>();
    }
    void OnEnable()
    {
        _timer = _time;
    }
    void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else
            _timer = _time;
        // * testing ? animation
        float temp = Mathf.Lerp(1f, 0f, _timer / _time);
        transform.localScale = new Vector3(temp, temp, 1f);
        _sprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, _timer / _time));
    }
}
