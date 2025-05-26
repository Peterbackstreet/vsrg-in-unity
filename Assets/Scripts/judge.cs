using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class judge : MonoBehaviour
{
    [SerializeField] int lane;
    [SerializeField] Material hit_material, default_material;
    [SerializeField] KeyCode key;
    
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
                if (timeDiff <= gameConfig.Instance.missWindow)
                {
                    note.isJudged = true;
                    if (timeDiff > gameConfig.Instance.goodWindow) gameController.Instance.resetCombo();
                    else
                    {
                        if (timeDiff <= gameConfig.Instance.perfectWindow)
                        {
                            Debug.Log("PERFECT");
                            gameController.Instance.addScore(gameConfig.Instance.perfectScore);
                        }
                        else if (timeDiff <= gameConfig.Instance.greatWindow)
                        {
                            Debug.Log("GREAT");
                            gameController.Instance.addScore(gameConfig.Instance.greatScore);
                        }
                        else
                        {
                            Debug.Log("GOOD");
                            gameController.Instance.addScore(gameConfig.Instance.goodScore);
                        }
                        Debug.Log(note.time - currTime);
                        gameController.Instance.addCombo();
                    }

                    if (note.type == 0)
                    {
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

                if (timeDiff <= gameConfig.Instance.goodWindow)
                {
                    gameController.Instance.addCombo();
                    gameController.Instance.addScore(gameConfig.Instance.perfectScore);
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
