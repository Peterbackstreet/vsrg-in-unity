using UnityEngine;

public class judge : MonoBehaviour
{
    [SerializeField] Material hit_material, default_material;
    [SerializeField] KeyCode key;
    void Start()
    {
        transform.GetComponent<MeshRenderer> ().material = default_material;
    }

    void Update()
    {
        if (Input.GetKey(key)) transform.GetComponent<MeshRenderer>().material = hit_material;
        else transform.GetComponent<MeshRenderer>().material = default_material;
    }
}
