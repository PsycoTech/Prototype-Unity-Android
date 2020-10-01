using UnityEngine;
[System.Serializable] public struct SerializableVector2
{
    public float x;
    public float y;
    public void SetVector(Vector2 vector)
    {
        x = vector.x;
        y = vector.y;
    }
}
[System.Serializable] public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;
    public Vector3 GetVector()
    {
        return new Vector3(x, y, z);
    }
    public void SetVector(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }
}
// * testing
[System.Serializable] public struct SerializableVector4
{
    public float x;
    public float y;
    public float z;
    public float w;
    public Quaternion GetQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }
    public Color GetColor()
    {
        return new Color(x, y, z, w);
    }
    public void SetQuaternion(Quaternion vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
        w = vector.w;
    }
}
[System.Serializable] public class base_state
{
}
