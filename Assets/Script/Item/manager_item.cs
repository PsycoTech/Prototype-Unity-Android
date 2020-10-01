using UnityEngine;
using System.Collections.Generic;
public class manager_item : MonoBehaviour
{
    public static manager_item Instance;
    private List<base_item> _items;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _items = new List<base_item>();
    }
    public void New()
    {
        foreach (base_item item in _items)
            item.New();
    }
    public void Save()
    {
        foreach (base_item item in _items)
            item.Save();
    }
    public void Load()
    {
        foreach (base_item item in _items)
            item.Load();
    }
    public void Register(base_item item)
    {
        if (_items.Contains(item))
            return;
        _items.Add(item);
    }
}
