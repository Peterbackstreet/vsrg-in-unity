using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using System.Collections.Generic;

public class chartManager : MonoBehaviour
{
    public string title, artist;
    public float BPM, start_offset;
    public string audioPath, jacketPath, bgPath;
    // private string pathPrefix = "Assets/Resources/Chart files/";
    private List<Note> Notes;
    [SerializeField] private GameObject notePrefab, longNotePrefab;
    [SerializeField] private Transform noteParent;
    [SerializeField] private beatLineGenerator beatLineGenerator;

    void Start()
    {
        audioPath = "Chart files/" + "Aether Crest/Aether Crest";
        audioController.Instance.loadAudio(audioPath);
        beatLineGenerator.generatebeatLines(BPM, start_offset, audioController.Instance.audioSource.time);
        InsertNote(0, 1, 2, 0);
    }

    public void InsertNote(int type, int lane, float beat, float hold_duration)
    {
        if (type == 0) addNote(type, lane, beat, hold_duration, notePrefab);
        else addNote(type, lane, beat, hold_duration, longNotePrefab);
    }
    void addNote(int type, int lane, float beat, float hold_duration, GameObject prefab)
    {
        GameObject newNoteObj = Instantiate(prefab, noteParent);
        Note newNote = newNoteObj.GetComponent<Note>();
        newNote.instantiateNote(type, lane, beat, hold_duration, start_offset);
        // Notes.Add(newNote);
    }
}
