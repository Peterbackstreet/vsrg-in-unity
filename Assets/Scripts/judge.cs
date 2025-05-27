using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class judge : MonoBehaviour
{
    [SerializeField] int lane;
    [SerializeField] Material hit_material, default_material;
    [SerializeField] KeyCode key;
    [SerializeField] private comboHandler comboHandler;
    [SerializeField] private scoreHandler scoreHandler;
    [SerializeField] private noteGenerator noteGenerator;
    private float currTime;
    void Start()
    {
        transform.GetComponent<MeshRenderer>().material = default_material;
    }

    void checkForNote()
    {
        foreach (Note note in noteGenerator.notes)
        {
            if (!note.isJudged && note.lane == lane)
            {
                float timeDiff = math.abs(note.time - currTime);
                if (timeDiff <= gameConfig.Instance.missWindow)
                {
                    note.isJudged = true;
                    if (timeDiff > gameConfig.Instance.goodWindow) comboHandler.resetCombo();
                    else
                    {
                        if (timeDiff <= gameConfig.Instance.perfectWindow)
                        {
                            // Debug.Log("PERFECT");
                            scoreHandler.addScore(gameConfig.Instance.perfectScore);
                        }
                        else if (timeDiff <= gameConfig.Instance.greatWindow)
                        {
                            // Debug.Log("GREAT");
                            scoreHandler.addScore(gameConfig.Instance.greatScore);
                        }
                        else
                        {
                            // Debug.Log("GOOD");
                            scoreHandler.addScore(gameConfig.Instance.goodScore);
                        }
                        // Debug.Log(note.time - currTime);
                        comboHandler.addCombo();
                    }

                    if (note.type == 0)
                    {
                        noteGenerator.notes.Remove(note);
                        Destroy(note.gameObject);
                    }
                    return;
                }
            }
        }
    }

    void checkRelease()
    {
        foreach (Note note in noteGenerator.notes)
        {
            if (note.lane == lane && note.isJudged && note.lane == lane)
            {
                float releaseTime = note.time + note.hold_duration;
                float timeDiff = math.abs(releaseTime - currTime);

                if (timeDiff <= gameConfig.Instance.goodWindow)
                {
                    comboHandler.addCombo();
                    scoreHandler.addScore(gameConfig.Instance.perfectScore);
                }
                else comboHandler.resetCombo();

                noteGenerator.notes.Remove(note);
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
