using UnityEngine;

public class BeatLine : MonoBehaviour
{
    private float currTime, timeDiff, time, distance, beat, BPM, start_offset;
    void getInstanceValue()
    {
        if (gameController.Instance != null)
        {
            BPM = gameController.Instance.BPM;
            start_offset = gameController.Instance.offset * 0.001f;
        }
        else if (chartManager.Instance != null)
        {
            BPM = chartManager.Instance.BPM;
            start_offset = chartManager.Instance.startOffset * 0.001f;
        }
    }
    public void initBeatLine(float beat)
    {
        this.beat = beat;
    }
    void Update()
    {
        getInstanceValue();
        currTime = audioController.Instance.audioSource.time;
        time = beat * 60 / BPM + start_offset;
        timeDiff = time - currTime;
        distance = timeDiff * gameConfig.Instance.scrollSpeed;
        transform.position = new Vector3(0, 0.1f, distance);
    }
}
