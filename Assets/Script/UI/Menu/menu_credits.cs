using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class menu_credits : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public ScrollRect _window;
    // protected ScrollRect _window;
    protected float _scroll;
    protected float _speed;
    protected bool _pause;
    protected Vector2 _pointer;
    void Awake()
    {
        // _window = GetComponent<ScrollRect>();
        _scroll = 0f;
        _speed = 1f;
        _pause = false;
        _pointer = Vector2.zero;
    }
    void Update()
    {
        if (_pause)
            return;
        // scroll
        if (_scroll < 1f)
            _scroll += Time.deltaTime * _speed;
        _scroll = Mathf.Clamp01(_scroll);
        _window.normalizedPosition = Vector2.up * _scroll;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_pointer == Vector2.zero)
        {
            _pointer = eventData.position;
            // pause
            _pause = true;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pressPosition == _pointer)
        {
            // scrub
            _scroll = _window.normalizedPosition.y;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pressPosition == _pointer)
        {
            // resume
            _pause = false;
        }
    }
}
