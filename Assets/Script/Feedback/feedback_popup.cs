using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class feedback_popup : MonoBehaviour
{
    public static feedback_popup Instance;
    // gomen~
    public GameObject _text;
    public Vector3 _offset;
    protected class Message
    {
        public Transform Source;
        public Transform Text;
        public float Timer;
        public Message(Transform source, string text, Color type, float timer)
        {
            Source = source;
            Text = Instantiate(Instance._text, game_camera.Instance.CameraMain.WorldToScreenPoint(Source.position), Instance.transform.rotation).transform;
            // Text = Instantiate(Instance._text, Instance.transform.position, Instance.transform.rotation).transform;
            Text.SetParent(Instance.transform);
            Text.GetComponent<Text>().text = text;
            Text.GetComponent<Text>().color = type;
            Text.gameObject.SetActive(true);
            Timer = timer;
        }
    }
    protected List<Message> _messages;
    protected List<Message> _toRemove;
    protected List<Message> _alerts;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        // ? cache enum
        // foreach ()
        // _text = Resources.Load("Feedback/Popup-" + type);
        _messages = new List<Message>();
        _toRemove = new List<Message>();
        _alerts = new List<Message>();
    }
    void Update()
    {
        foreach (Message message in _messages)
        {
            if (message.Source && message.Timer > 0)
            {
                // *testing ? bounds
                message.Text.position = game_camera.Instance.CameraMain.WorldToScreenPoint(message.Source.position + Vector3.Lerp(_offset, Vector3.zero, message.Timer / game_variables.Instance.DurationPopup));
                // message.Text.color = new Color(message.Text.color.x, message.Text.color.y, message.Text.color.z, 1f - (message.Timer / game_variables.Instance.DurationPopup));
                message.Timer -= Time.deltaTime;
            }
            else
                _toRemove.Add(message);
        }
        foreach (Message message in _toRemove)
        {
            message.Text.GetComponent<SelfDestruct>().Trigger();
            _messages.Remove(message);
        }
        _toRemove.Clear();
        // 
        foreach (Message alert in _alerts)
        {
            if (alert.Source.gameObject.activeSelf)
                // *testing ? bounds
                alert.Text.position = game_camera.Instance.CameraMain.WorldToScreenPoint(alert.Source.position + Vector3.up);
            else
                _toRemove.Add(alert);
        }
        foreach (Message message in _toRemove)
        {
            message.Text.GetComponent<SelfDestruct>().Trigger();
            _alerts.Remove(message);
        }
        _toRemove.Clear();
    }
    public void RegisterMessage(Transform source, string text, Color type, float timer = -1f)
    {
        _messages.Add(new Message(source, text, type, timer < 0f ? game_variables.Instance.DurationPopup : timer));
    }
    public void SetAlert(Transform source, int status)
    {
        Message temp = null;
        foreach (Message alert in _alerts)
            if (alert.Source == source)
            {
                if (status == -1)
                {
                    temp = alert;
                    break;
                }
                else
                {
                    alert.Text.GetComponent<Text>().text = status == 0 ? "?" : "!";
                    return;
                }
            }
        if (temp == null)
            _alerts.Add(new Message(source, status == 0 ? "?" : "!", game_variables.Instance.ColorDefault, 0f));
        else
        {
            temp.Text.GetComponent<SelfDestruct>().Trigger();
            _alerts.Remove(temp);
        }
    }
}
