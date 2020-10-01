using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class feedback_label : MonoBehaviour
{
    public static feedback_label Instance;
    // gomen~
    public GameObject _text;
    public Vector3 _offset;
    protected class Message
    {
        public Transform Source;
        public Transform Text;
        public Message(Transform source, string text)
        {
            Source = source;
            Text = Instantiate(Instance._text, game_camera.Instance.CameraMain.WorldToScreenPoint(Source.position), Instance.transform.rotation).transform;
            Text.SetParent(Instance.transform);
            Text.GetComponent<Text>().text = text;
            Text.gameObject.SetActive(true);
        }
    }
    protected List<Message> _messages;
    protected List<Message> _toRemove;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _messages = new List<Message>();
        _toRemove = new List<Message>();
    }
    void Update()
    {
        foreach (Message message in _messages)
        {
            if (message.Source)
            {
                message.Text.position = game_camera.Instance.CameraMain.WorldToScreenPoint(message.Source.position + _offset);
                message.Text.gameObject.SetActive(!manager_ui.Instance.IsOverlay);
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
    }
    public void RegisterMessage(Transform source, string text)
    {
        _messages.Add(new Message(source, text));
    }
}
