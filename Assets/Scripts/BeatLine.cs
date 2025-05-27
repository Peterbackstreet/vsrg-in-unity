using UnityEngine;

public class BeatLine : MonoBehaviour
{
    private float currTime, timeDiff, time, distance, beat, BPM, start_offset;
    public void getInstanceValue(float BPM, float start_offset)
    {
        this.BPM = BPM;
        this.start_offset = start_offset;
    }
    public void initBeatLine(float beat)
    {
        this.beat = beat;
    }
    void Update()
    {
        currTime = audioController.Instance.audioSource.time;
        time = beat * 60 / BPM + start_offset;
        timeDiff = time - currTime;
        distance = timeDiff * gameConfig.Instance.scrollSpeed;
        transform.position = new Vector3(0, 0.1f, distance);
    }
}
