using UnityEngine;
public class feedback_navigation : MonoBehaviour
{
    public static feedback_navigation Instance;
    [SerializeField] protected float _radius = 1f;
    [SerializeField] protected float _time = 1f;
    protected float _timer;
    protected Transform[] _markers;
    protected SpriteRenderer[] _sprites;
    protected int _active;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _markers = new Transform[2];
        _markers[0] = transform.GetChild(0);
        _markers[1] = transform.GetChild(1);
        // _markers[0].gameObject.SetActive(false);
        // _markers[1].gameObject.SetActive(false);
        _sprites = new SpriteRenderer[2];
        _sprites[0] = _markers[0].GetComponent<SpriteRenderer>();
        _sprites[1] = _markers[1].GetComponent<SpriteRenderer>();
        _active = -1;
    }
    void Update()
    {
        if (_timer > 0)
        {
            if (_markers[_active].gameObject.activeSelf)
            {
                // * testing ? animation
                float temp = Mathf.Lerp(_radius, 1f, _timer / _time);
                _markers[_active].localScale = new Vector3(temp, temp, 1f);
                _sprites[_active].color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, _timer / _time));
                _timer -= Time.deltaTime;
            }
            else
                _markers[_active].gameObject.SetActive(true);
        }
        else if (_active > -1)
        {
            _markers[0].gameObject.SetActive(false);
            _markers[1].gameObject.SetActive(false);
            _active = -1;
        }
    }
    public void SetPosition(Vector3 position, bool value)
    {
        _markers[0].gameObject.SetActive(false);
        _markers[1].gameObject.SetActive(false);
        _active = value ? 1 : 0;
        _markers[_active].position = position;
        _timer = _time;
    }
}
