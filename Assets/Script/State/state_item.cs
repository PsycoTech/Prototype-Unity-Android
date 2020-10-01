// item data class
[System.Serializable]
public class state_item : base_state
{
    public SerializableVector3 Position;
    public SerializableVector4 Rotation;
    public int HealthInst;
    public bool Enabled;
    public bool Kinematic;
    // detonate
    public float Timer;
}
