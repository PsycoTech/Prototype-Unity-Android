// * source: sebastian lague
using UnityEngine;

public class map_tile : IHeapItem<map_tile>
{
    private bool _isSolid;
    private Vector3 _position;
    private int _costG;
    private int _costH;
    private int _X;
    private int _Y;
    private map_tile _parent;
    private int _heapIndex;
    private int _penalty;
    public map_tile(bool isSolid, Vector3 position, int X, int Y, int penalty)
    {
        _isSolid = isSolid;
        _position = position;
        _X = X;
        _Y = Y;
        _penalty = penalty;
    }
    public int CompareTo(map_tile tile)
    {
        int compare = CostF.CompareTo(tile.CostF);
        if (compare == 0)
        {
            compare = _costH.CompareTo(tile.CostH);
        }
        return -compare;
    }
    public bool IsSolid
    {
        get { return _isSolid; }
    }
    public Vector3 Position
    {
        get { return _position; }
    }
    public int X
    {
        get { return _X; }
    }
    public int Y
    {
        get { return _Y; }
    }
    public int CostG
    {
        get { return _costG; }
        set { _costG = value; }
    }
    public int CostH
    {
        get { return _costH; }
        set { _costH = value; }
    }
    public int CostF
    {
        get { return _costG + _costH; }
    }
    public map_tile Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }
    public int HeapIndex
    {
        get { return _heapIndex; }
        set { _heapIndex = value; }
    }
    public int Penalty
    {
        get { return _penalty; }
        set { _penalty = value; }
    }
}
