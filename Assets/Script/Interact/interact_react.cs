using UnityEngine;
// switch lever ?button ?pulley
public class interact_react : base_interact
{
    [Tooltip("Reference to react object")] [SerializeField] protected base_react _react = null;
    [Tooltip("Target signal")] [SerializeField] protected int _id = 0;
    public override int TryAction(Transform target)
    {
        int check = base.TryAction(transform);
        if (check > 0)
        {
            if (_react)
                _react.Ping(_id, _active);
        }
        return check;
    }
    // * testing switch sequence
    public void Clear()
    {
        _active = false;
        // feedback_popup.Instance.RegisterMessage(transform, _valid ? "closed" : "off", game_variables.Instance.ColorInteract);
    }
}
