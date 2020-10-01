using UnityEngine;
using System.Collections.Generic;
public class manager_mob : MonoBehaviour
{
    public static manager_mob Instance;
    protected List<controller_mob> _mobs;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _mobs = new List<controller_mob>();
    }
    // public void Save()
    // {
    //     foreach (controller_mob mob in _mobs)
    //         Save(mob.State);
    // }
    public void New()
    {
        foreach (controller_mob mob in _mobs)
            mob.New();
    }
    public void Save()
    {
        foreach (controller_mob mob in _mobs)
            mob.Save();
    }
    public void Load()
    {
        foreach (controller_mob mob in _mobs)
            mob.Load();
    }
    public void Register(controller_mob mob)
    {
        if (_mobs.Contains(mob))
            return;
        _mobs.Add(mob);
    }
}
