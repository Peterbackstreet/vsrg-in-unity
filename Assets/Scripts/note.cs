using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int type, lane;
    public float time, hold_duration, beat, BPM, start_offset; //hold_duration(beat)
    public bool isJudged = false;
    private float tickTime, currTime;
    private comboHandler comboHandler;
    private noteGenerator noteGenerator;

    public void getInstanceValue(float BPM, float start_offset, comboHandler comboHandler, noteGenerator noteGenerator)
    {
        this.BPM = BPM;
        this.start_offset = start_offset;

        this.comboHandler = comboHandler;
        this.noteGenerator = noteGenerator;
    }

    public void instantiateNote(int type, int lane, float beat, float hold_duration, float offset)
    {
        this.type = type;
        this.lane = lane;
        this.start_offset = offset * 0.001f;
        this.beat = beat;
        this.time = beat * 60 / BPM + this.start_offset;
        this.hold_duration = hold_duration * 60 / BPM;
        this.tickTime = this.time;
        if (this.type == 1) ajdustLN();

    }

    private void ajdustLN()
    {
        float tailPos = hold_duration * gameConfig.Instance.scrollSpeed;
        float bodyPos = tailPos / 2;
        float bodyScale = hold_duration * gameConfig.Instance.scrollSpeed;
        gameObject.transform.Find("tail").localPosition = new Vector3(0, 0, tailPos);
        gameObject.transform.Find("body").localPosition = new Vector3(0, 0, bodyPos);

        Transform body = gameObject.transform.Find("body");
        body.localScale = new Vector3(body.localScale.x, body.localScale.y, bodyScale - body.localScale.z);
    }

    void onHoldTick()
    {
        float timeDiff =  currTime - tickTime;
        float timeInterval = 60 / BPM;
        float tailReleaseTime = time + hold_duration;
        if (timeDiff >= timeInterval && currTime < tailReleaseTime)
        {
            Debug.Log("time interval: " + timeInterval);
            tickTime = currTime;
            comboHandler.addCombo(); 
        }
    }

    void Update()
    {
        currTime = audioController.Instance.audioSource.time;
        float timeDiff = time - currTime;

        if (isJudged && type == 1) onHoldTick();

        if (!gameConfig.Instance.isEditorMode && timeDiff < -gameConfig.Instance.missWindow && !isJudged)
        {
            comboHandler.resetCombo();
            noteGenerator.notes.Remove(gameObject.GetComponent<Note>());
            Destroy(gameObject);
        }

        float lanePos = lane - 2.5f;
        float distance = timeDiff * gameConfig.Instance.scrollSpeed;
        transform.position = new Vector3(lanePos, 0, distance);
    }

    public void displayNote()
    {
        Debug.Log("type: " + type + " lane: " + lane + " time: " + time + " hold: " + hold_duration + "");
    }


}
