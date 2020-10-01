using UnityEngine;
using System.Collections.Generic;
public class manager_proximity : MonoBehaviour
{
    public static manager_proximity Instance;
    protected List<base_proximity> _proximities;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _proximities = new List<base_proximity>();
    }
    public void New()
    {
        foreach (base_proximity proximity in _proximities)
            proximity.New();
    }
    public void Save()
    {
        foreach (base_proximity proximity in _proximities)
            proximity.Save();
    }
    public void Load()
    {
        foreach (base_proximity proximity in _proximities)
            proximity.Load();
    }
    public void Register(base_proximity proximity)
    {
        if (_proximities.Contains(proximity))
            return;
        _proximities.Add(proximity);
    }
}
