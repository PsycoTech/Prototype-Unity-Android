using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class feedback_toaster : MonoBehaviour
{
    public static feedback_toaster Instance;
    // gomen~
    public GameObject _text;
    private int _limit = 3;
    protected class Message
    {
        public Transform Text;
        public float Timer;
        public Message(string text, Color type, float timer)
        {
            Text = Instantiate(Instance._text, (Vector2)Instance.transform.position, Instance.transform.rotation).transform;
            Text.SetParent(Instance.transform);
            Text.GetComponent<Text>().text = text;
            Text.GetComponent<Text>().color = type;
            Text.gameObject.SetActive(false);
            Timer = timer;
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
        // foreach (Message message in _messages)
        for (int i = 0; i < _messages.Count; i++)
        {
            if (i < _limit)
            {
                if (_messages[i].Timer > 0)
                {
                    _messages[i].Timer -= Time.deltaTime;
                    if (!_messages[i].Text.gameObject.activeSelf)
                        _messages[i].Text.gameObject.SetActive(true);
                }
                else
                    _toRemove.Add(_messages[i]);
            }
            else if (_messages[i].Text.gameObject.activeSelf)
                _messages[i].Text.gameObject.SetActive(false);
        }
        foreach (Message message in _toRemove)
        {
            message.Text.GetComponent<SelfDestruct>().Trigger();
            _messages.Remove(message);
        }
        _toRemove.Clear();
    }
    public void RegisterMessage(string text, Color type, float timer = -1f)
    {
        _messages.Add(new Message(text, type, timer < 0f ? game_variables.Instance.DurationPopup : timer));
    }
}
