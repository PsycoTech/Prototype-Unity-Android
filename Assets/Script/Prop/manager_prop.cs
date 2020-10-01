using UnityEngine;
using System.Collections.Generic;
public class manager_prop : MonoBehaviour
{
    public static manager_prop Instance;
    private List<base_prop> _props;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _props = new List<base_prop>();
    }
    public void New()
    {
        foreach (base_prop prop in _props)
            prop.New();
    }
    public void Save()
    {
        foreach (base_prop prop in _props)
            prop.Save();
    }
    public void Load()
    {
        foreach (base_prop prop in _props)
            prop.Load();
    }
    public void Register(base_prop prop)
    {
        if (_props.Contains(prop))
            return;
        _props.Add(prop);
    }
}
