using UnityEngine;

public class BeatLine : MonoBehaviour
{
    private float currTime, timeDiff, time, distance, start_offset;
    public void initBeatLine(float beat, float start_offset)
    {
        this.start_offset = start_offset * 0.001f;
        this.time = beat * 60 / gameController.Instance.BPM + this.start_offset;
        Debug.Log(this.time);
    }
    void Update()
    {
        currTime = audioController.Instance.audioSource.time;
        timeDiff = time - currTime;
        distance = timeDiff * gameConfig.Instance.scrollSpeed;
        transform.position = new Vector3(0, 0.1f, distance);
    }
}
