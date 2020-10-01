using UnityEngine;
using System.Collections.Generic;
public class manager_chunk : MonoBehaviour
{
    public static manager_chunk Instance;
    protected List<base_chunk> _chunks;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _chunks = new List<base_chunk>();
    }
    public void Initialize()
    {
        foreach (base_chunk chunk in _chunks)
            chunk.Initialize();
    }
    // public void Save()
    // {
    //     foreach (base_chunk chunk in _chunks)
    //         chunk.Save();
    // }
    // public void Load()
    // {
    //     foreach (base_chunk chunk in _chunks)
    //         chunk.Load();
    // }
    public List<Vector2Int[]> GetActive()
    {
        List<Vector2Int[]> positions = new List<Vector2Int[]>();
        foreach (base_chunk chunk in _chunks)
            if (chunk.State < game_variables.Instance.Depth)
                positions.Add(chunk.GetPositions());
        return positions;
    }
    public void Register(base_chunk chunk)
    {
        if (_chunks.Contains(chunk))
            return;
        _chunks.Add(chunk);
    }
}
