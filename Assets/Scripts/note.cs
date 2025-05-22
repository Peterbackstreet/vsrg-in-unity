using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int type, lane;
    public float time, hold_duration, offset;
    public AudioSource audioSource; 
    private gameConfig config = new gameConfig();
    void Start()
    {
        float lanePos = (lane - 1) - 1.5f;
        float distance = (offset + time) * config.scrollSpeed * 0.001f;
        transform.position = new Vector3(lanePos, 0, distance);
    }

    public void instantiateNote(int type, int lane, float time, float hold_duration, float offset)
    {
        this.type = type;
        this.lane = lane;
        this.time = time;
        this.hold_duration = hold_duration;
        this.offset = offset;
    }

    void Update()
    {
        float lanePos = (lane - 1) - 1.5f;
        float distance = (offset + time - audioSource.time*1000) * config.scrollSpeed * 0.001f ;
        transform.position = new Vector3(lanePos, 0, distance);
    }

    public void displayNote()
    {
        Debug.Log("type: " + type + " lane: " + lane + " time: " + time + " hold: " + hold_duration + "");
    }


}
