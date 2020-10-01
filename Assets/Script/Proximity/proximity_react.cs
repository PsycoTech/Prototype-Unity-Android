using UnityEngine;
using System.Collections.Generic;
// plate wire
public class proximity_react : base_proximity
{
    [Tooltip("Reference to react object")] [SerializeField] protected base_react _react = null;
    [Tooltip("Target signal")] [SerializeField] protected int _id = 0;
    [Tooltip("Ping state value: 0 - false | 1 - true | 2 - either")] [SerializeField] protected int _match = 2;
    [Tooltip("Sprite pool")] [SerializeField] protected List<Sprite> _sprites = new List<Sprite>();
    protected bool _check;
    public override void Load()
    {
        base.Load();
        _check = _active;
    }
    protected override void Update()
    {
        base.Update();
        if (_check != _active)
        {
            _check = _active;
            if (_react && ((_match == 0 && !_active) || (_match == 1 && _active) || _match == 2))
                _react.Ping(_id, _active);
        }
        if (_sprites.Count > 1)
            _sprite.sprite = _sprites[_active ? 1 : 0];
    }
}
