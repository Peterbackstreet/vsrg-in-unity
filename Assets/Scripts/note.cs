using UnityEngine;

public class Note : MonoBehaviour
{
    int type, lane;
    float time, hold_duration;

    public Note(int type, int lane, float time, float hold_duration)
    {
        this.type = type;
        this.lane = lane;
        this.time = time;
        this.hold_duration = hold_duration;
    }


    void Start()
    {

    }

    void Update()
    {

    }
}
