using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using System.Collections.Generic;
using System;

public class chartManager : MonoBehaviour
{
    public string title, artist;
    public float BPM, start_offset;
    public string audioPath, jacketPath, bgPath;
    // private string pathPrefix = "Assets/Resources/Chart files/";
    [SerializeField] private GameObject notePrefab, longNotePrefab;
    [SerializeField] private Transform noteParent;
    [SerializeField] private noteGenerator noteGenerator;
    [SerializeField] private beatLineGenerator beatLineGenerator;

    void Start()
    {
        gameConfig.Instance.isEditorMode = true;
        audioPath = "Chart files/" + "Aether Crest/Aether Crest";
        audioController.Instance.loadAudio(audioPath);
        beatLineGenerator.generatebeatLines(BPM, start_offset, audioController.Instance.audioSource.time);
    }

    public void insertNote(int type, int lane, float beat, float hold_duration)
    {
        noteGenerator.generateNote(type, lane, beat, hold_duration);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) audioController.Instance.audioSource.time += 0.5f;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) audioController.Instance.audioSource.time -= 0.5f;
    }

    private bool getKeyDown(KeyCode leftArrow)
    {
        throw new NotImplementedException();
    }
}
