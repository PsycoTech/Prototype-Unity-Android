using UnityEngine;
using System.Collections.Generic;
public class manager_interact : MonoBehaviour
{
    public static manager_interact Instance;
    protected List<base_interact> _interacts;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _interacts = new List<base_interact>();
    }
    public void New()
    {
        foreach (base_interact interact in _interacts)
            interact.New();
    }
    public void Save()
    {
        foreach (base_interact interact in _interacts)
            interact.Save();
    }
    public void Load()
    {
        foreach (base_interact interact in _interacts)
            interact.Load();
    }
    public void Register(base_interact interact)
    {
        if (_interacts.Contains(interact))
            return;
        _interacts.Add(interact);
    }
}
