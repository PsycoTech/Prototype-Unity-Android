using UnityEngine;
using System.Collections.Generic;
public class manager_react : MonoBehaviour
{
    public static manager_react Instance;
    protected List<base_react> _reacts;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _reacts = new List<base_react>();
    }
    public void New()
    {
        foreach (base_react react in _reacts)
            react.New();
    }
    public void Save()
    {
        foreach (base_react react in _reacts)
            react.Save();
    }
    public void Load()
    {
        foreach (base_react react in _reacts)
            react.Load();
    }
    public void Register(base_react react)
    {
        if (_reacts.Contains(react))
            return;
        _reacts.Add(react);
    }
}
