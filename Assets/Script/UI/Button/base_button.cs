using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class base_button : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // private Text _text;
    [SerializeField] protected List<Sprite> _sprites = new List<Sprite>();
    protected Image _image;
    protected int _state;
    // protected float _time = .05f;
    // protected float _timer = 0f;
    protected virtual void Awake()
    {
        // _text = transform.GetChild(0).GetComponent<Text>();
        _image = GetComponent<Image>();
        _state = 0;
    }
    protected virtual void Update()
    {
        if (_sprites.Count > 0)
            _image.sprite = _sprites[_state];
        // _text.text = controller_player.Instance.Data.HealthInst > 0 ? "CONTINUE" : "CONTINUE?";
        // // if (_state > 9)
        // if (_timer > 0)
        //     _timer -= Time.deltaTime;
        // else if (_state > 0)
        //     _state = 0;
    }
    protected void SetState(int value)
    {
        _state = Mathf.Clamp(value, 0, _sprites.Count);
        // if (_state > 0)
        //     _timer = _time;
    }
    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     if (controller_player.Instance.Data.HealthInst > 0)
    //         manager_ui.Instance.SetMain(false);
    //     else
    //         manager_ui.Instance.ToggleContinue();
    // }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        SetState(1);
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        SetState(0);
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        SetState(0);
    }
}
