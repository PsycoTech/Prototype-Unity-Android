using UnityEngine;
public class controller_player : entity_controller
{
    public static controller_player Instance;
    // // * testing
    // [SerializeField] protected float _speedDash = 10f;
    protected Transform _target;
    [Tooltip("Target highlight")] [SerializeField] protected Transform _track = null;
    protected override void Awake()
    {
        // * testing
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        // ? post
        base.Awake();
        _target = null;
        if (_track)
            _track.gameObject.SetActive(false);
    }
    // * testing
    void Update()
    {
        if (!_data || !_motor)
            return;
        // ---
        if (Input.GetKeyDown("q"))
            _data.HealthDrain(1);
        else if (Input.GetKeyDown("w"))
            _data.HealthRestore(1);
        if (_timerPath > 0)
            _timerPath -= Time.deltaTime;
        if (_track)
            _track.position = _target ? _target.position : grid_map.Instance.WorldToGrid(input_touch.Instance.CacheTapWorld);
        // ---
        if (input_touch.Instance.EventCamera)
            _anim.SetRotation(_motor.Position + (Vector3)input_touch.Instance.CacheCamera());
        if (input_touch.Instance.EventTap())
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(_motor.Position, _data.RadiusAction, _anim.Direction, .5f, game_variables.Instance.ScanLayerAction);
            // // ---
            // bool check = true;
            // // interact, pickup
            // foreach (RaycastHit2D hit in hits)
            // {
            //     if (_data.Action(hit.transform) == 1)
            //     {
            //         check = false;
            //         break;
            //     }
            // }
            // print(check);
            // // use
            // if (check)
            //     _data.Equipped?.Use(_data, transform);
            // ---
            float min = float.MaxValue;
            Transform target = null;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform == _data.Equipped?.transform)
                    continue;
                float angle = _anim.PositionToAngle(hit.point);
                if (angle < min)
                {
                    min = angle;
                    target = hit.transform;
                }
            }
            // print(target?.gameObject.name + " : " + min);
            if (_data.Action(target))
                _data.Equipped?.Use(_data, target);
        }
    }
    // void FixedUpdate()
    // {
    //     if (!_data || !_motor)
    //         return;
    //     // print(input_touch.Instance.EventTap + " " + Time.realtimeSinceStartup);
    //     if (input_touch.Instance.EventCamera)
    //     {
    //         // _motor.NavigateCancel();
    //         // // * testing action cancel
    //         // _data.Clear();
    //         // * testing aim [shield axe gun charm jar etc]
    //         _anim.SetRotation(_motor.Position + (Vector3)input_touch.Instance.CacheCamera());
    //     }
    //     // if (input_touch.Instance.EventMotor)
    //     // {
    //     //     // _motor.NavigateCancel();
    //     //     // // * testing action cancel
    //     //     // _data.Clear();
    //     //     // * testing aim [shield axe gun charm jar etc]
    //     //     _anim.SetRotation(_motor.Position + (Vector3)input_touch.Instance.CacheMotor());
    //     // }
    //     if (input_touch.Instance.EventTap())
    //     {
    //         // _motor.NavigateCancel();
    //         // // * testing action cancel
    //         // _data.Clear();
    //         // // * testing aim [shield axe gun charm jar etc]
    //         // _anim.SetRotation(input_touch.Instance.CacheTapWorld);
    //         // * testing
    //         // if (input_touch.Instance.EventSwipe())
    //         //     _motor.AddForce(input_touch.Instance.CacheSwipe * _speedDash);
    //         // if (input_touch.Instance.EventTapDouble())
    //         //     _motor.AddForce((input_touch.Instance.CacheTapWorld - controller_player.Instance.Motor.Position).normalized * _speedDash);
    //         // else
    //         // {
    //         // if (TryAction())
    //         //     return;
    //         // // approach ? negative feedback
    //         // _motor.NavigateTo(input_touch.Instance.CacheTapWorld);
    //         // _timerPath = _timePath;
    //         // if (_track)
    //         //     _track.gameObject.SetActive(true);
    //         // ---
    //         // // ? check collision type, radius
    //         // Collider2D collider;
    //         // // if (input_touch.Instance.EventTap(false))
    //         // //     collider = Physics2D.OverlapCircle(input_touch.Instance.CacheTapWorld, .1f, game_variables.Instance.ScanLayerTap);
    //         // // else
    //         // collider = Physics2D.OverlapCircle(_motor.Position, _data.RadiusAction, game_variables.Instance.ScanLayerTap);
    //         // // // periodic path update till reach ?
    //         // // _target = collider ? collider.transform : null;
    //         // if (!collider && _data.TryUse())
    //         //     return;
    //         // // - interact
    //         // if (collider.gameObject.layer == game_variables.Instance.LayerInteract)
    //         // {
    //         //     if (_data.Equipped != null && _data.TryUse(collider.transform))
    //         //         return;
    //         //     // ? key invalid consume
    //         //     if (_data.TryInteract(collider.transform))
    //         //     // else if (_data.TryInteract(collider.transform))
    //         //     // else if (_data.Equipped == null && _data.TryInteract(collider.transform))
    //         //         return;
    //         // }
    //         // // - item
    //         // else if (collider.gameObject.layer == game_variables.Instance.LayerItem)
    //         // {
    //         //     // other > pickup
    //         //     if (_data.Equipped != collider.GetComponent<base_item>() && _data.TryPickup(collider.transform))
    //         //         return;
    //         // }
    //         // else if (_data.TryUse(collider.transform))
    //         //     return;
    //         // ---
    //         RaycastHit2D[] hits = Physics2D.CircleCastAll(_motor.Position, _data.RadiusAction, _anim.Direction, .5f);
    //         bool check = true;
    //         foreach (RaycastHit2D hit in hits)
    //         {
    //             if (_data.Action(hit.transform) == 1)
    //             {
    //                 check = false;
    //                 break;
    //             }
    //         }
    //         // use
    //         if (check)
    //             _data.Equipped?.Use(_data, transform);
    //         // ---
    //         // // consumable
    //         // if (_data.Equipped && !_data.Equipped.IsDestroy)
    //         //     // use
    //         //     _data.Equipped.Use(_data, transform);
    //         // else
    //         // {
    //         //     // interact, pickup
    //         //     RaycastHit2D[] hits = Physics2D.CircleCastAll(_motor.Position, _data.RadiusAction, _anim.Direction, .5f);
    //         //     foreach (RaycastHit2D hit in hits)
    //         //         if (_data.Action(hit.transform))
    //         //             break;
    //         // }
    //         // ---
    //         // - solid | prop > ignore
    //         // if (collider.gameObject.layer == game_variables.Instance.LayerSolid || collider.gameObject.layer == game_variables.Instance.LayerProp)
    //         //     return;
    //         // // - player > ignore | use
    //         // else if (collider.gameObject.layer == game_variables.Instance.LayerPlayer)
    //         // {
    //         //     if (_data.Equipped == null)
    //         //         return;
    //         //     else if (_data.TryUse(collider.transform))
    //         //         return;
    //         // }
    //         // // - mob > use, punch | approach
    //         // else if (collider.gameObject.layer == game_variables.Instance.LayerMob)
    //         // {
    //         //     if (_data.Equipped != null && _data.TryUse(collider.transform))
    //         //         return;
    //         //     else if (_data.Equipped == null)
    //         //     {
    //         //         //  && _data.TryUse(collider.transform)
    //         //         // return;
    //         //     }
    //         // }
    //         // // - interact > approach | use, action
    //         // else if (collider.gameObject.layer == game_variables.Instance.LayerInteract)
    //         // {
    //         //     if (_data.Equipped != null && _data.TryUse(collider.transform))
    //         //         return;
    //         //     // ? key invalid consume
    //         //     if (_data.TryInteract(collider.transform))
    //         //     // else if (_data.TryInteract(collider.transform))
    //         //     // else if (_data.Equipped == null && _data.TryInteract(collider.transform))
    //         //         return;
    //         // }
    //         // // - item
    //         // else if (collider.gameObject.layer == game_variables.Instance.LayerItem)
    //         // {
    //         //     // // equipped > drop
    //         //     // if (_data.Equipped == collider.GetComponent<base_item>())
    //         //     // {
    //         //     //     _data.SetEquipped(null);
    //         //     //     // menu_inventory.Instance.SetEquipped(_data.Equipped, false);
    //         //     //     return;
    //         //     // }
    //         //     // // other > approach > drop > pickup
    //         //     // else if (_data.TryPickup(collider.transform))
    //         //     //     // _anim.SetTarget(collider.transform.position);
    //         //     //     return;
    //         //     // other > pickup
    //         //     if (_data.Equipped != collider.GetComponent<base_item>() && _data.TryPickup(collider.transform))
    //         //         return;
    //         // }
    //     }
    //     // // * testing path update/reach
    //     // else if (_motor.IsMove)
    //     // {
    //     //     if (_timerPath <= 0)
    //     //     {
    //     //         _timerPath = _timePath;
    //     //         // _motor.NavigateTo(input_touch.Instance.CacheTapWorld);
    //     //         // _motor.NavigateTo(_target.position);
    //     //         // print(_target);
    //     //         _motor.NavigateTo(_target ? _target.position : input_touch.Instance.CacheTapWorld);
    //     //         // // reached ?
    //     //         // if (!feedback_move.Instance.gameObject.activeSelf)
    //     //         //     _motor.NavigateCancel();
    //     //     }
    //     //     if (_track)
    //     //         _track.gameObject.SetActive(true);
    //     // }
    //     // else
    //     // {
    //     //     _timerPath = 0f;
    //     //     // _target = null;
    //     //     if (_track)
    //     //         _track.gameObject.SetActive(false);
    //     // }
    // }
    // void OnDrawGizmos()
    // {
    //     if (_target)
    //     {
    //         Gizmos.color = new Color(.5f, .5f, 0f, .5f);
    //         Gizmos.DrawSphere(_target.position, .5f);
    //     }
    // }
    // public bool TryAction()
    // {
    //     // ? check collision type, radius
    //     Collider2D collider;
    //     // if (input_touch.Instance.EventTap(false))
    //     //     collider = Physics2D.OverlapCircle(input_touch.Instance.CacheTapWorld, .1f, game_variables.Instance.ScanLayerTap);
    //     // else
    //     collider = Physics2D.OverlapCircle(_motor.Position, _data.RadiusAction, game_variables.Instance.ScanLayerTap);
    //     // periodic path update till reach ?
    //     _target = collider ? collider.transform : null;
    //     if (!collider)
    //         return false;
    //     // - solid | prop > ignore
    //     if (collider.gameObject.layer == game_variables.Instance.LayerSolid || collider.gameObject.layer == game_variables.Instance.LayerProp)
    //         return true;
    //     // - player > ignore | use
    //     else if (collider.gameObject.layer == game_variables.Instance.LayerPlayer)
    //     {
    //         if (_data.Equipped == null)
    //             return true;
    //         else if (_data.TryUse(collider.transform))
    //             return true;
    //     }
    //     // - mob > use, punch | approach
    //     else if (collider.gameObject.layer == game_variables.Instance.LayerMob)
    //     {
    //         if (_data.Equipped != null && _data.TryUse(collider.transform))
    //             return true;
    //         else if (_data.Equipped == null)
    //         {
    //             //  && _data.TryUse(collider.transform)
    //             // return;
    //         }
    //     }
    //     // - interact > approach | use, action
    //     else if (collider.gameObject.layer == game_variables.Instance.LayerInteract)
    //     {
    //         if (_data.Equipped != null && _data.TryUse(collider.transform))
    //             return true;
    //         // ? key invalid consume
    //         if (_data.TryInteract(collider.transform))
    //         // else if (_data.TryInteract(collider.transform))
    //         // else if (_data.Equipped == null && _data.TryInteract(collider.transform))
    //             return true;
    //     }
    //     // - item
    //     else if (collider.gameObject.layer == game_variables.Instance.LayerItem)
    //     {
    //         // equipped > drop
    //         if (_data.Equipped == collider.GetComponent<base_item>())
    //         {
    //             _data.SetEquipped(null);
    //             // menu_inventory.Instance.SetEquipped(_data.Equipped, false);
    //             return true;
    //         }
    //         // other > approach > drop > pickup
    //         else if (_data.TryPickup(collider.transform))
    //             // _anim.SetTarget(collider.transform.position);
    //             return true;
    //     }
    //     return false;
    // }
}
