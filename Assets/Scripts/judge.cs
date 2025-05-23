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
    void Start()
    {
        transform.GetComponent<MeshRenderer>().material = default_material;
    }

    void checkForNote()
    {
        foreach (Note note in gameController.Instance.Notes)
        {
            if (!note.judged && note.lane == lane)
            {
                float timeDiff = math.abs(note.time - audioController.Instance.audioSource.time);
                if (timeDiff <= config.missWindow)
                {
                    note.judged = true;
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
            if (note.type == 1 && note.judged)
            {
                float releaseTime = note.time + note.hold_duration;
                float timeDiff = math.abs(releaseTime - audioController.Instance.audioSource.time);

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
        if (Input.GetKey(key)) transform.GetComponent<MeshRenderer>().material = hit_material;
        else transform.GetComponent<MeshRenderer>().material = default_material;

        if (Input.GetKeyDown(key)) checkForNote();
        if (Input.GetKeyUp(key)) checkRelease();

    }
}
