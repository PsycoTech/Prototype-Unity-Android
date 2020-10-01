using UnityEngine;
using System.Collections.Generic;
// altar
public class interact_direction : base_interact
{
    [SerializeField] protected List<GameObject> _targets = new List<GameObject>();
    [Tooltip("true - on | false - off")] [SerializeField] protected bool _flag = false;
    protected int _direction = 0;
    // protected bool _default;
    protected override void Start()
    {
        _default = _flag;
        base.Start();
    }
    public override void Load()
    {
        base.Load();
        _flag = _default;
    }
    void OnEnable()
    {
        // if (_direction < _targets.Count && !_targets[_direction].activeSelf)
        //     _targets[_direction].SetActive(true);
        for (int i = 0; i < _targets.Count; i++)
            _targets[i].SetActive(_flag && i == _direction);
    }
    void OnDisable()
    {
        // if (_direction < _targets.Count && _targets[_direction].activeSelf)
        //     _targets[_direction].SetActive(false);
        for (int i = 0; i < _targets.Count; i++)
            if (_targets[_direction])
                _targets[_direction].SetActive(false);
    }
    public override int TryAction(Transform target)
    {
        int check = base.TryAction(target);
        if (check > 0)
        {
            if (_direction < _targets.Count)
            {
                if (_targets[_direction].activeSelf)
                    _targets[_direction].SetActive(false);
                _direction++;
                if (_direction == _targets.Count)
                    _direction = 0;
                if (_flag && !_targets[_direction].activeSelf)
                    _targets[_direction].SetActive(true);
            }
        }
        return check;
    }
    public void SetFlag(bool value)
    {
        _flag = value;
        if (_targets[_direction].activeSelf != value)
            _targets[_direction].SetActive(value);
    }
}
