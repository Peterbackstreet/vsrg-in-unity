using UnityEngine;
using System.Collections.Generic;

public class beatLineGenerator : MonoBehaviour
{
    [SerializeField] private GameObject beatLinePrefab;
    [SerializeField] private Transform beatLineParent;
    List<GameObject> beatLines;
    public void generatebeatLines(float BPM, float startOffset, float chartLength)
    {
        float start_offset = startOffset;
        float Length = audioController.Instance.chartLength - start_offset * 0.001f; 
        float beatCnt = (Length / 60) * BPM;
        for (float beat = 0; beat < beatCnt; beat++)
        {
            GameObject newBeatLine = Instantiate(beatLinePrefab, beatLineParent);
            newBeatLine.GetComponent<BeatLine>().initBeatLine(beat);
        }
    }


}
