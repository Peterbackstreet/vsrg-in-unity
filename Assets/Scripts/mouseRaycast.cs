using UnityEngine;

public class mouseRaycast : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPosition = hit.point;
                Debug.Log("Clicked on: " + hit.collider.name);
                Debug.Log("hit poistion: " + hitPosition);
            }
        }
    }
}