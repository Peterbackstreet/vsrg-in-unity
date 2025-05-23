using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class judge : MonoBehaviour
{
    [SerializeField] int lane;
    [SerializeField] Material hit_material, default_material;
    [SerializeField] KeyCode key;
    [SerializeField] gameController gameController;
    [SerializeField] AudioSource audioSource;
    private gameConfig config = new gameConfig();
    void Start()
    {
        transform.GetComponent<MeshRenderer>().material = default_material;
    }

    void checkForNote()
    {
        foreach (Note note in gameController.Notes)
        {
            if (note.lane == lane)
            {
                float timeDiff = math.abs(note.time - audioSource.time * 1000);
                if (timeDiff <= config.missWindow)
                {
                    if (timeDiff > config.goodWindow) gameController.resetCombo();
                    else
                    {
                        if (timeDiff <= config.perfectWindow) gameController.addScore(config.perfectScore);
                        else if (timeDiff <= config.greatWindow) gameController.addScore(config.greatScore);
                        else gameController.addScore(config.goodScore);
                        gameController.addCombo();
                    }
                    gameController.Notes.Remove(note);
                    Destroy(note.gameObject);
                    return;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKey(key)) transform.GetComponent<MeshRenderer>().material = hit_material;
        else transform.GetComponent<MeshRenderer>().material = default_material;

        if (Input.GetKeyDown(key)) checkForNote();
    }
}
