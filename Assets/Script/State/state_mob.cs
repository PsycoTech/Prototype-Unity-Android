// using UnityEngine;
// mob data class
[System.Serializable]
public class state_mob : base_state
{
    // 
    // anim
    public SerializableVector4 color;
    public SerializableVector3 rotation;
    public bool flicker;
    // 
    // data
    public int healthInst;
    public int equipped;
    // public int hostiles;
    public bool resurrect;
    // public int ammoWeapons;
    public int flag;
    public SerializableVector3 target;
    // 
    // motor
    // public SerializableVector3 path;
    public int targetIndex;
    public SerializableVector3 move;
    public SerializableVector2 cache;
    // public SerializableVector3 forces;
    public SerializableVector3 position;
    // 
    // controller
    public SerializableVector3 positionVision;
    public SerializableVector3 positionTrigger;
    public float awareTimer;
    public bool isAware;
    // public int[] timers;
    // public int[] flags;
    public bool vision;
    public bool trigger;
    public bool sensors;
    public bool sleep;
    public int timePath;
    public int flagStatus;
}
