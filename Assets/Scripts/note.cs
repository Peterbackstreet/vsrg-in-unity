using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int type, lane;
    public float time, hold_duration, offset;
    public bool judged = false;
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
        if(this.type == 1) ajdustLN();
    }

    private void ajdustLN()
    {
        float tailPos = hold_duration * config.scrollSpeed * 0.001f;
        float bodyPos = tailPos / 2;
        float bodyScale = hold_duration * config.scrollSpeed * 0.001f;
        gameObject.transform.Find("tail").localPosition = new Vector3(0, 0, tailPos);
        gameObject.transform.Find("body").localPosition = new Vector3(0, 0, bodyPos);

        Transform body = gameObject.transform.Find("body");
        body.localScale = new Vector3(body.localScale.x, body.localScale.y, bodyScale - body.localScale.z);
    }

    void Update()
    {
        float timeDiff = time - audioController.Instance.audioSource.time * 1000;

        if (!judged && timeDiff < -50)
        {
            gameController.Instance.resetCombo();
            gameController.Instance.Notes.Remove(gameObject.GetComponent<Note>());
            Destroy(gameObject);
        }

        float lanePos = lane - 2.5f;
        float distance = (offset + timeDiff) * config.scrollSpeed * 0.001f ; 
        transform.position = new Vector3(lanePos, 0, distance);
    }

    public void displayNote()
    {
        Debug.Log("type: " + type + " lane: " + lane + " time: " + time + " hold: " + hold_duration + "");
    }


}
