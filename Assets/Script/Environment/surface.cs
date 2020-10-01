using UnityEngine;
public class surface : MonoBehaviour
{
    // 1f - default
    // 0f - void
    // 0.5f - ice
    // 1.5f - fluid, flow
    public float _drag = 1f;
    // 0.5f - fluid, flow
    public float _speed = 1f;
    // 0f > flow, wind
    public Vector2 _force = Vector2.zero;
    protected SpriteRenderer _sprite;
    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        if (_sprite)
            _sprite.enabled = true;
    }
    // void Update()
    // {
    //     // * testing
    //     if (_sprite)
    //         // _sprite.enabled = Vector3.Distance(transform.position, controller_player.Instance.Motor.Position) <= game_variables.Instance.RadiusSprite;
    //         _sprite.enabled =  game_camera.Instance.InView(transform.position);
    // }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == game_variables.Instance.LayerPlayer)
        {
            entity_motor temp = other.GetComponent<entity_motor>();
            // controller_player temp = other.GetComponent<controller_player>();
            if (_drag > 0f)
                // player_motor.Instance.ModifierDrag = _drag;
                temp.SetModifierDrag(_drag);
            if (_speed > 0f)
                temp.SetModifierSpeed(_speed);
            if (_force.sqrMagnitude > 0)
                temp.AddForce(_force);
        }
    }
}
