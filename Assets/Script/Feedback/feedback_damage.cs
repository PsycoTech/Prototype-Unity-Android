using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class feedback_damage : MonoBehaviour
{
    public static feedback_damage Instance;
    [SerializeField] protected GameObject _marker;
    protected List<Marker> _markers;
    protected List<Marker> _toRemove;
    public class Marker
    {
        public Transform Source;
        public Transform Sprite;
        public Image Display;
        public float Timer;
        public Marker(Transform source)
        {
            Source = source;
            Sprite = Instantiate(Instance._marker, game_camera.Instance.CameraMain.WorldToScreenPoint(source.position), Instance.transform.rotation).transform;
            Sprite.SetParent(Instance.transform);
            Sprite.localPosition = Vector3.zero;
            Vector2 direction = source.position - game_camera.Instance.Position;
            Sprite.rotation = Quaternion.Euler(0f, 0f, -90f + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            Sprite.gameObject.SetActive(true);
            Display = Sprite.GetComponent<Image>();
            Timer = game_variables.Instance.DurationPopup;
        }
    }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _markers = new List<Marker>();
        _toRemove = new List<Marker>();
    }
    void Update()
    {
        foreach (Marker marker in _markers)
        {
            if (marker.Timer > 0)
            {
                Vector2 direction = marker.Source.position - game_camera.Instance.Position;
                marker.Sprite.rotation = Quaternion.Euler(0f, 0f, -90f + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                marker.Timer -= Time.deltaTime;
                // * testing lerp opacity
                marker.Display.color = Color.Lerp(new Color(1f, 0f, 0f, 0f), new Color(1f, 0f, 0f, 1f), marker.Timer / game_variables.Instance.DurationPopup);
            }
            else
                _toRemove.Add(marker);
        }
        foreach (Marker marker in _toRemove)
        {
            marker.Sprite.GetComponent<SelfDestruct>().Trigger();
            _markers.Remove(marker);
        }
        _toRemove.Clear();
    }
    public void RegisterMarker(Transform source)
    {
        _markers.Add(new Marker(source));
    }
}
