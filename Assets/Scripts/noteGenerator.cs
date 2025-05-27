using UnityEngine;
using System.Collections.Generic;

public class noteGenerator : MonoBehaviour
{
    public List<Note> notes = new List<Note>();
    private float BPM = -1, start_offset = -1;
    [SerializeField] comboHandler comboHandler;
    [SerializeField] private GameObject notePrefab, longNotePrefab;
    [SerializeField] private Transform noteParent;
    [SerializeField] private gameController gameController;
    [SerializeField] private chartManager chartManager;
    void getInstanceValue()
    {
        if (gameController)
        {
            this.BPM = gameController.BPM;
            this.start_offset = gameController.offset;
        }
        else if (chartManager)
        {
            this.BPM = chartManager.BPM;
            this.start_offset = chartManager.start_offset;
        }
    }
    public void generateNote(int type, int lane, float beat, float hold_duration)
    {
        if (BPM == -1 || start_offset == -1) getInstanceValue();

        GameObject prefab = (type == 0) ? notePrefab : longNotePrefab;
        GameObject newNoteObj = Instantiate(prefab, noteParent);
        Note newNote = newNoteObj.GetComponent<Note>();
        newNote.getInstanceValue(BPM, start_offset, comboHandler, gameObject.GetComponent<noteGenerator>());
        newNote.instantiateNote(type, lane, beat, hold_duration, start_offset);
        notes.Add(newNote);
    }
}
