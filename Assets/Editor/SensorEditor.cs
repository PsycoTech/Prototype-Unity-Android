using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (sensor_vision))]
public class SensorVisionEditor : Editor
{
    void OnSceneGUI()
    {
        sensor_vision vision = (sensor_vision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(vision.Position, Vector3.forward, Vector3.up, 360, vision.Radius);
        Vector3 angleA = vision.DirectionFromAngle(-vision.Angle / 2, false);
        Vector3 angleB = vision.DirectionFromAngle(vision.Angle / 2, false);
        Handles.DrawLine(vision.Position, vision.Position + angleA * vision.Radius);
        Handles.DrawLine(vision.Position, vision.Position + angleB * vision.Radius);
        Handles.color = Color.red;
        foreach (Transform target in vision.Targets)
        {
            Handles.DrawLine(vision.Position, target.position);
        }
    }
}
