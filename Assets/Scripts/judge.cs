using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class judge : MonoBehaviour
{
    [SerializeField] int lane;
    [SerializeField] Material hit_material, default_material;
    [SerializeField] KeyCode key;
    private gameConfig config = new gameConfig();
    private float currTime;
    void Start()
    {
        transform.GetComponent<MeshRenderer>().material = default_material;
    }

    void checkForNote()
    {
        foreach (Note note in gameController.Instance.Notes)
        {
            if (!note.isJudged && note.lane == lane)
            {
                float timeDiff = math.abs(note.time - currTime);
                if (timeDiff <= config.missWindow)
                {
                    note.isJudged = true;
                    if (timeDiff > config.goodWindow) gameController.Instance.resetCombo();
                    else
                    {
                        if (timeDiff <= config.perfectWindow) gameController.Instance.addScore(config.perfectScore);
                        else if (timeDiff <= config.greatWindow) gameController.Instance.addScore(config.greatScore);
                        else gameController.Instance.addScore(config.goodScore);
                        gameController.Instance.addCombo();
                    }

                    if (note.type == 0)
                    {
                        Debug.Log(note.time);
                        gameController.Instance.Notes.Remove(note);
                        Destroy(note.gameObject);
                    }
                    return;
                }
            }
        }
    }

    void checkRelease()
    {
        foreach (Note note in gameController.Instance.Notes)
        {
            if (note.lane == lane && note.isJudged && note.lane == lane)
            {
                float releaseTime = note.time + note.hold_duration;
                float timeDiff = math.abs(releaseTime - currTime);

                if (timeDiff <= config.goodWindow)
                {
                    gameController.Instance.addCombo();
                    gameController.Instance.addScore(config.perfectScore);
                }
                else gameController.Instance.resetCombo();

                gameController.Instance.Notes.Remove(note);
                Destroy(note.gameObject);
                return;
            }
        }
    }

    void Update()
    {
        currTime = audioController.Instance.audioSource.time;
        if (Input.GetKey(key)) transform.GetComponent<MeshRenderer>().material = hit_material;
        else transform.GetComponent<MeshRenderer>().material = default_material;

        if (Input.GetKeyDown(key)) checkForNote();
        if (Input.GetKeyUp(key)) checkRelease();

    }
}
