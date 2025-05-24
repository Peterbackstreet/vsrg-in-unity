using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int type, lane;
    public float time, hold_duration, offset; //hold_duration(beat as unit)
    public bool isJudged = false;
    private float tickTime, currTime;
    private gameConfig config = new gameConfig();
    void Start()
    {
        float lanePos = (lane - 1) - 1.5f;
        float distance =  time * gameController.Instance.scrollSpeed;
        transform.position = new Vector3(lanePos, 0, distance);
    }

    public void instantiateNote(int type, int lane, float beat, float hold_duration, float offset)
    {
        this.type = type;
        this.lane = lane;
        this.time = beat * 60 / gameController.Instance.BPM + offset * 0.001f; 
        this.hold_duration = hold_duration * 60 / gameController.Instance.BPM;
        this.tickTime = this.time;
        if (this.type == 1) ajdustLN();
    }

    private void ajdustLN()
    {
        float tailPos = hold_duration * gameController.Instance.scrollSpeed;
        float bodyPos = tailPos / 2;
        float bodyScale = hold_duration * gameController.Instance.scrollSpeed;
        gameObject.transform.Find("tail").localPosition = new Vector3(0, 0, tailPos);
        gameObject.transform.Find("body").localPosition = new Vector3(0, 0, bodyPos);

        Transform body = gameObject.transform.Find("body");
        body.localScale = new Vector3(body.localScale.x, body.localScale.y, bodyScale - body.localScale.z);
    }

    void onHoldTick()
    {
        float timeDiff =  currTime - tickTime;
        float timeInterval = 60 / gameController.Instance.BPM;
        float tailReleaseTime = time + hold_duration;
        if (timeDiff >= timeInterval && currTime < tailReleaseTime)
        {
            Debug.Log("time interval: " + timeInterval);
            tickTime = currTime;
            gameController.Instance.addCombo();
        }
    }

    void Update()
    {
        currTime = audioController.Instance.audioSource.time;
        float timeDiff = time - currTime;

        if (isJudged && type == 1) onHoldTick();

        if (!isJudged && timeDiff < -config.missWindow)
        {
            gameController.Instance.resetCombo();
            gameController.Instance.Notes.Remove(gameObject.GetComponent<Note>());
            Destroy(gameObject);
        }

        float lanePos = lane - 2.5f;
        float distance = (offset + timeDiff) * gameController.Instance.scrollSpeed;
        transform.position = new Vector3(lanePos, 0, distance);
    }

    public void displayNote()
    {
        Debug.Log("type: " + type + " lane: " + lane + " time: " + time + " hold: " + hold_duration + "");
    }


}
