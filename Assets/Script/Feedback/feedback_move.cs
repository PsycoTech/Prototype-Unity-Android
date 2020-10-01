using UnityEngine;
// * testing ? shader
public class feedback_move : MonoBehaviour
{
    protected LineRenderer _line;
    void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }
    public void SetPath(Vector3[] path, Color color)
    {
        Vector3[] temp = new Vector3[path.Length + 1];
        temp[0] = transform.position;
        for (int i = 0; i < path.Length; i++)
            temp[i + 1] = path[i];
        _line.positionCount = temp.Length;
        _line.SetPositions(temp);
        _line.startColor = color;
        _line.endColor = color;
    }
    public void Clear()
    {
        _line.positionCount = 0;
    }
}
