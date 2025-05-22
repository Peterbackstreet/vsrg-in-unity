using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int type, lane;
    public float time, hold_duration, offset;

    void Start()
    {
        gameConfig config = new gameConfig();
        float lanePos = (lane - 1) - 1.5f;
        float distance = (offset + time) * config.scrollSpeed * 0.001f;
        transform.position = new Vector3(lanePos,0,distance);
    }

    public void instantiateNote(int type, int lane, float time, float hold_duration, float offset)
    {
        this.type = type;
        this.lane = lane;
        this.time = time;
        this.hold_duration = hold_duration;
        this.offset = offset;
    }

    public void displayNote()
    {
        Debug.Log("type: " + type + " lane: " + lane + " time: " + time + " hold: " + hold_duration + "");
    }

}
