using UnityEngine;
using System.Collections.Generic;
public class base_chunk : MonoBehaviour
{
    protected List<base_chunk> _neighbours = new List<base_chunk>();
    protected Vector2Int _bounds;
    protected Vector3 _offset;
    protected List<GameObject> _objects;    // mob prop item interact fluid sensor
    protected int _state;   // 0 - player | 1 + neighbour
    // protected int _depth;   // X - off
    void Awake()
    {
        _neighbours = new List<base_chunk>();
        // _bounds = Vector2Int.one * 10;
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        _bounds = new Vector2Int(Mathf.FloorToInt(collider.size.x), Mathf.FloorToInt(collider.size.y));
        _offset = collider.offset;
        _objects = new List<GameObject>();
    }
    void Start()
    {
        manager_chunk.Instance.Register(this);
        // Load();
        // * testing
        // _depth = game_variables.Instance.Depth;
        // SetState(game_variables.Instance.Depth);
        SetState(1);
        // SetState(game_variables.Instance.Depth - 1);
    }
    public void Initialize()
    {
        // if (_state == game_variables.Instance.Depth)
        //     return;
        // if (_objects.Contains(controller_player.Instance.Data.gameObject))
        //     SetState(game_variables.Instance.Depth);
        // else
        // SetState(game_variables.Instance.Depth);
        if (_state > 0)
            SetState(1);
        // if (_state != 0)
        //     SetState(game_variables.Instance.Depth - 1);
    }
    // public void Save()
    // {
    //     // if (_state == 2)
    //     //     return;
    //     // // _state = 1;
    //     // // SetState(0);
    //     // SetState(1);
    // }
    // public void Load()
    // {
    //     // if (_state == 2)
    //     //     return;
    //     // // _state = 1;
    //     // // SetState(0);
    //     // SetState(1);
    // }
    void OnDrawGizmos()
    {
        // player
        if (_state == 0)
            Gizmos.color = new Color(0, 1, 0, .5f);
        // neighbour
        else if (_state > 0 && _state < game_variables.Instance.Depth)
            Gizmos.color = new Color(0, 0, _state / (float)game_variables.Instance.Depth, .5f);
        // off
        else if (_state == game_variables.Instance.Depth)
            Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(transform.position + _offset, new Vector3(_bounds.x, _bounds.y, 1f));
    }
    void Update()
    {
        // // off
        // if (_state == game_variables.Instance.Depth)
        // {
        //     foreach (base_chunk chunk in _neighbours)
        //         // warmer
        //         if (chunk.State < _state - 1)
        //         {
        //             // warm
        //             SetState(_state - 1);
        //             break;
        //         }
        // }
        // // neighbour
        // else if (_state > 0 && _state < game_variables.Instance.Depth)
        // {
        //     int check = 0;
        //     foreach (base_chunk chunk in _neighbours)
        //         if (chunk.State < check)
        //             check = chunk.State;
        //     // if (check == 0)
        //     //     // demote
        //     //     SetState(_state - 1);
        //     // warmer
        //     if (check < _state)
        //         // cold
        //         SetState(check + 1);
        // }
        // ---
        // foreach (base_chunk chunk in _neighbours)
        //     // warmer
        //     if (chunk.State > 0 && chunk.State < _state)
        //     {
        //         // warm
        //         SetState(_state - 1);
        //         break;
        //     }
        //     // colder
        //     else if (chunk.State < game_variables.Instance.Depth && chunk.State > _state)
        //     {
        //         // cold
        //         SetState(_state + 1);
        //         break;
        //     }
        // ---
        // if (_state == 0)
        //     return;
        // int check = 0;
        // foreach (base_chunk chunk in _neighbours)
        //     if (_state == 0)
        //         chunk.SetState(_state + 1);
        //     else if (chunk.State < _state - 1)
        //         // promote
        //         check--;
        //     else if (chunk.State > _state + 1)
        //         // demote
        //         check++;
        // SetState(_state + check);
        // ---
        if (_state == 2)
        {
            foreach (base_chunk chunk in _neighbours)
                if (chunk.State == 0)
                {
                    SetState(1);
                    break;
                }
        }
        else if (_state == 1)
        {
            bool check = true;
            foreach (base_chunk chunk in _neighbours)
                if (chunk.State == 0)
                    check = false;
            if (check)
                SetState(2);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == game_variables.Instance.LayerPlayer)
            // promote
            SetState(0);
        else if (other.gameObject.layer == game_variables.Instance.LayerChunk)
        {
            base_chunk temp = other.GetComponent<base_chunk>();
            if (!_neighbours.Contains(temp))
                _neighbours.Add(temp);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == game_variables.Instance.LayerPlayer)
            // demote
            SetState(1);
    }
    public void SetState(int value)
    {
        // ?
        if (_state == value)
            return;
        // (re)load objects
        if (_state < game_variables.Instance.Depth)
        {
            // // restore ? used items reactivate
            // foreach (GameObject temp in _objects)
            //     temp?.SetActive(true);
            // restock
            _objects.Clear();
            for (int x = -_bounds.x / 2; x < _bounds.x / 2; x++)
                for (int y = -_bounds.y / 2; y < _bounds.y / 2; y++)
                {
                    // * testing
                    Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position + (Vector2)_offset + new Vector2(x, y) + Vector2.one * .5f, .45f, game_variables.Instance.ScanLayerObject);
                    foreach (Collider2D collider in colliders)
                        if (collider.tag == "Ignore")
                            continue;
                        else
                            _objects.Add(collider.gameObject);
                        // {
                        //     if (collider.gameObject.layer == game_variables.Instance.LayerPlayer)
                        //         value = game_variables.Instance.Depth;
                        // }
                        // // * testing ? redundant invert ignore
                        // if (!_objects.Contains(collider.gameObject))
                        // {
                        //     if (collider.gameObject.layer == game_variables.Instance.LayerMob)
                        //         _objects.Add(collider.gameObject);
                        //     else if (collider.gameObject.layer == game_variables.Instance.LayerItem)
                        //     {
                        //         if (!collider.isTrigger && !collider.GetComponent<base_item>().Ignore)
                        //             _objects.Add(collider.gameObject);
                        //     }
                        //     else if (collider.gameObject.layer == game_variables.Instance.LayerProp)
                        //     {
                        //         if (!collider.isTrigger)
                        //         {
                        //             base_prop temp = collider.GetComponent<base_prop>();
                        //             if (temp)
                        //             {
                        //                 if (!temp.Ignore)
                        //                     _objects.Add(collider.gameObject);
                        //             }
                        //             // else
                        //             //     _objects.Add(collider.gameObject);
                        //         }
                        //     }
                        //     // else if (collider.gameObject.layer == game_variables.Instance.LayerReact)
                        //     // {
                        //     //     if (!collider.isTrigger && !collider.GetComponent<base_react>().Ignore)
                        //     //         _objects.Add(collider.gameObject);
                        //     // }
                        //     // ???
                        //     // else if (collider.gameObject.layer == game_variables.Instance.LayerSensor)
                        //     // {
                        //     //     if (!collider.isTrigger && !collider.GetComponent<base_react>().Ignore)
                        //     //         _objects.Add(collider.gameObject);
                        //     // }
                        //     else if (!collider.isTrigger)
                        //         _objects.Add(collider.gameObject);
                        // }
                }
            // * testing ? in view / sprite visible / outside player radius sprite check
            // if (value == 0)
            //     foreach (GameObject temp in _objects)
            //         if (temp.layer == game_variables.Instance.LayerMob)
            //             temp.GetComponent<controller_mob>().Load();
        }
        // apply
        foreach (GameObject temp in _objects)
            temp?.SetActive(value < game_variables.Instance.Depth);
        // update
        _state = value;
    }
    public Vector2Int[] GetPositions()
    {
        Vector2Int[] positions = new Vector2Int[_bounds.x * _bounds.y];
        // for (int x = 0; x < _bounds.x; x++)
        //     for (int y = 0; y < _bounds.y; y++)
        //         positions[x + y * 10] = transform.position 
        int index = 0;
        for (int x = 1 - _bounds.x / 2; x <= _bounds.x / 2; x++)
            for (int y = 1 -_bounds.y / 2; y <= _bounds.y / 2; y++)
            {
                // positions[index] = transform.position + new Vector3(x + .5f, y + .5f, 0);
                positions[index] = grid_map.Instance.WorldToIndex(transform.position + _offset) + new Vector2Int(x, y);
                index++;
            }
        return positions;
    }
    public int State
    {
        get { return _state; }
    }
}
