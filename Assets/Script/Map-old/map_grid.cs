// * source: sebastian lague (arigatou~)
using UnityEngine;
using System.Collections.Generic;

public class map_grid : MonoBehaviour
{
    // public bool _testSmoothWeights;
    // public int _testSmoothWeight = 3;
    public bool _showGridGizmos;
    public Vector2 _sizeGrid;
    public float _sizeTile = 1f;
    private map_tile[,] _grid;
    public Vector3 _offset;

    private Vector2Int _sizeGrid_Int;

    public float _correction = 0.1f;

    // weight
    public SurfaceType[] _surfaces;
    private LayerMask _mask;
    private Dictionary<int, int> _surfacesDictionary = new Dictionary<int, int>();

    // weight smoothing
    private Vector2Int _penaltyBounds = new Vector2Int(int.MaxValue, int.MinValue);

    void Awake()
    {
        _sizeGrid_Int = new Vector2Int(Mathf.RoundToInt(_sizeGrid.x / _sizeTile), Mathf.RoundToInt(_sizeGrid.y / _sizeTile));
        foreach (SurfaceType surface in _surfaces)
        {
            _mask.value |= surface.mask.value;
            _surfacesDictionary.Add((int)Mathf.Log(surface.mask.value, 2), surface.penalty);
        }
        Initialize();
    }

    // * testing
    public int MaxSize
    {
        get { return _sizeGrid_Int.x * _sizeGrid_Int.y; }
    }
    void OnDrawGizmos()
    {
        if (!_showGridGizmos)
            return;
        // Gizmos.DrawWireCube(transform.position + _offset, new Vector3(_sizeGrid.x, _sizeGrid.y));
        if (_grid != null)
            foreach (map_tile tile in _grid)
            {
                Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(_penaltyBounds.x, _penaltyBounds.y, tile.Penalty));
                Gizmos.color = tile.IsSolid ? Color.red : Gizmos.color;
                Gizmos.DrawWireCube(tile.Position, Vector3.one * (1f - _correction));
            }
    }

    private void Initialize()
    {
        _grid = new map_tile[_sizeGrid_Int.x, _sizeGrid_Int.y];
        for (int x = _sizeGrid_Int.x - 1; x > -1; x--)
            for (int y = _sizeGrid_Int.y - 1; y > -1; y--)
            {
                Vector3 position = Vector3.right * (x + 0.5f) * _sizeTile + Vector3.up * (y + 0.5f) * _sizeTile + _offset - Vector3.right * _sizeGrid.x / 2f - Vector3.up * _sizeGrid.y / 2f;
                // bool isSolid = Physics2D.OverlapCircle(position, _sizeTile / 2f, game_variables.Instance.ScanLayerDefault) != null;
                bool isSolid = Physics2D.OverlapCircle(position, (_sizeTile / 2f) - _correction, game_variables.Instance.ScanLayerSolid) != null;
                int penalty = 0;
                // // * borked 200611
                // if (!isSolid)
                // {
                //     Ray ray = new Ray(position - Vector3.forward, Vector3.forward);
                //     // Debug.DrawLine(ray.origin, ray.origin + Vector3.up);
                //     RaycastHit hit;
                //     // if (Physics.Raycast(ray, out hit, 2f))
                //     if (Physics.Raycast(ray, out hit, 2f, _mask))
                //     {
                //         // Debug.Log("doh");
                //         _surfacesDictionary.TryGetValue(hit.collider.gameObject.layer, out penalty);
                //     }
                // }
                if (!isSolid)
                {
                    Collider2D collider = Physics2D.OverlapCircle(position, (_sizeTile / 2f) - _correction, _mask);
                    if (collider != null)
                        _surfacesDictionary.TryGetValue(collider.gameObject.layer, out penalty);
                    // * testing
                    if (penalty < _penaltyBounds.x)
                        _penaltyBounds.x = penalty;
                    if (penalty > _penaltyBounds.y)
                        _penaltyBounds.y = penalty;
                }
                _grid[x, y] = new map_tile(isSolid, position, x, y, penalty);
            }
        // // * testing
        // if (_testSmoothWeights)
        //     SmoothWeights(_testSmoothWeight);
    }

    // private void SmoothWeights(int blurSize)
    // {
    //     int kernelSize = blurSize * 2 + 1;
    //     int kernelExtents = (kernelSize - 1) / 2;
    //     int [,] penaltiesHorizontalPass = new int [_sizeGrid_Int.x, _sizeGrid_Int.y];
    //     int [,] penaltiesVerticalPass = new int [_sizeGrid_Int.x, _sizeGrid_Int.y];
    //     for (int y = 0; y < _sizeGrid_Int.y; y++)
    //     {
    //         for (int x = -kernelExtents; x <= kernelExtents; x++)
    //         {
    //             int sampleX = Mathf.Clamp(x, 0, kernelExtents);
    //             penaltiesHorizontalPass[0, y] += _grid[sampleX, y].Penalty;
    //         }
    //         for (int x = 1; x < _sizeGrid_Int.x; x++)
    //         {
    //             int removeIndex = Mathf.Clamp(x - kernelExtents - 1, 0, _sizeGrid_Int.x);
    //             int addIndex = Mathf.Clamp(x + kernelExtents, 0, _sizeGrid_Int.x - 1);
    //             penaltiesHorizontalPass[x, y] = penaltiesHorizontalPass[x - 1, y] - _grid[removeIndex, y].Penalty + _grid[addIndex, y].Penalty;
    //         }
    //     }
    //     for (int x = 0; x < _sizeGrid_Int.x; x++)
    //     {
    //         for (int y = -kernelExtents; y <= kernelExtents; y++)
    //         {
    //             int sampleY = Mathf.Clamp(y, 0, kernelExtents);
    //             penaltiesVerticalPass[x, 0] += penaltiesHorizontalPass[x, sampleY];
    //         }
    //         for (int y = 1; y < _sizeGrid_Int.y; y++)
    //         {
    //             int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, _sizeGrid_Int.y);
    //             int addIndex = Mathf.Clamp(y + kernelExtents, 0, _sizeGrid_Int.y - 1);
    //             penaltiesVerticalPass[x, y] = penaltiesVerticalPass[x, y - 1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];
    //             int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));
    //             _grid[x, y].Penalty = blurredPenalty;
    //             // * testing
    //             if (blurredPenalty < _penaltyBounds.x)
    //                 _penaltyBounds.x = blurredPenalty;
    //             if (blurredPenalty > _penaltyBounds.y)
    //                 _penaltyBounds.y = blurredPenalty;
    //         }
    //     }
    // }

    public map_tile WorldToIndexPoint(Vector3 position)
    {
        // axis
        position -= Vector3.right * 0.5f * _sizeTile + Vector3.up * 0.5f * _sizeTile + _offset - Vector3.right * _sizeGrid.x / 2f - Vector3.up * _sizeGrid.y / 2f;
        // scale
        position /= _sizeTile;
        // bounds
        position.x = Mathf.Clamp(position.x, 0, _sizeGrid.x - 1);
        position.y = Mathf.Clamp(position.y, 0, _sizeGrid.y - 1);
        // 
        return _grid[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)];
    }

    public List<map_tile> GetNeigbours(map_tile tile)
    {
        List<map_tile> neighbours = new List<map_tile>();
        for (int x = -1; x < 2; x++)
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                int X = tile.X + x;
                int Y = tile.Y + y;
                if (X > -1 && X < _sizeGrid_Int.x && Y > -1 && Y < _sizeGrid_Int.y)
                    neighbours.Add(_grid[X, Y]);
            }
        return neighbours;
    }

    [System.Serializable]
    public class SurfaceType
    {
        public LayerMask mask;
        public int penalty;
    }
}
