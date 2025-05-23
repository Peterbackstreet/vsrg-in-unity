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
    void Start()
    {
        transform.GetComponent<MeshRenderer>().material = default_material;
    }

    void checkForNote() {
        foreach (Note note in gameController.Notes)
        {
            if (note.lane == lane)
            {
                float timeDiff = math.abs(note.time - audioSource.time*1000);
                if (timeDiff < 50)
                {
                    gameController.Notes.Remove(note);
                    Destroy(note.gameObject);
                    Debug.Log("W");
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
