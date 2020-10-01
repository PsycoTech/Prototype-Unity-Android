using UnityEngine;
public class base_hitbox : MonoBehaviour
{
    protected Transform _source = null;
    [SerializeField] protected SpriteRenderer _sprite = null;
    protected virtual void Awake()
    {
        if (!_sprite)
            _sprite = GetComponent<SpriteRenderer>();
        if (_sprite)
            _sprite.enabled = true;
    }
    public void Initialize(Transform source)
    {
        _source = source;
    }
    protected virtual void Update()
    { 
        // if (_sprite)
        //     // _sprite.enabled = Vector3.Distance(transform.position, controller_player.Instance.Motor.Position) <= game_variables.Instance.RadiusSprite;
        //     _sprite.enabled =  game_camera.Instance.InView(transform.position);
    }
    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
