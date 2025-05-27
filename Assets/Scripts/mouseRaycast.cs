using UnityEngine;

public class mouseRaycast : MonoBehaviour
{
    private float start_offset;
    private chartManager chartManager;

    void Start()
    {
        chartManager = GameObject.Find("chartManager").GetComponent<chartManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                int lane;
                Vector3 hitPos = hit.point;
                if (hitPos.x < -1) lane = 1;
                else if (hitPos.x <= 0) lane = 2;
                else if (hitPos.x <= 1) lane = 3;
                else lane = 4;

                float time = hitPos.z / gameConfig.Instance.scrollSpeed;
                float currTime = time + audioController.Instance.audioSource.time - chartManager.start_offset * 0.001f;
                float beat = currTime * chartManager.BPM / 60;
                beat = roundToTimeSignature(beat);
                Debug.Log("beat: " + beat);
                Debug.Log("hitPos: " + hitPos);
                Debug.Log("time: " + currTime);
                chartManager.insertNote(0, lane, beat, 0);
            }
        }
    }

    float roundToTimeSignature(float beat)
    {
        return beat = Mathf.Round(beat * 4) / 4;
    }
}