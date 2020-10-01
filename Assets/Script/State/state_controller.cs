// using UnityEngine;
[System.Serializable]
public class state_controller : base_state
{
    // controller
    public SerializableVector3 PositionVision;
    public SerializableVector3 PositionTrigger;
    public float AwareTimer;
    public bool IsAware;
    // public int[] timers;
    // public int[] flags;
    public bool Vision;
    public bool Trigger;
    public bool Sensors;
    public bool Sleep;
    // public int TimerPath;
    public int FlagStatus;
}
