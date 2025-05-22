using UnityEngine;

public class Note : MonoBehaviour
{
    public int type, lane;
    public float time, hold_duration;

    public Note(int type, int lane, float time, float hold_duration)
    {
        this.type = type;
        this.lane = lane;
        this.time = time;
        this.hold_duration = hold_duration;
    }

    public void displayNote()
    {
        Debug.Log("type: " + type + " lane: " + lane + " time: " + time + " hold: " + hold_duration + "");
    }

}
