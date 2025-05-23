using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class gameController : MonoBehaviour
{
    private string title, artist;
    private string path = "Assets/Chart files/";
    private float BPM, offset = 0;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private AudioSource audioSource;
    public List<Note> Notes = new List<Note>();
    void Start()
    {
        ReadChartFile(path + "Aether Crest/data.txt");
    }

    void ReadChartFile(string file)
    {
        string[] lines = File.ReadAllLines(file);

        foreach (string line in lines)
        {
            if (line.StartsWith("#TITLE")) title = line.Substring(6);
            else if (line.StartsWith("#ARTIST")) artist = line.Substring(7);
            else if (line.StartsWith("#BPM")) BPM = float.Parse(line.Substring(4));
            else if (line.StartsWith("#FILE")) path = line.Substring(5);
            else if (line.StartsWith("#OFFSET")) offset = float.Parse(line.Substring(7));
            else if (line.StartsWith("#")) InsertNote(line.Substring(1));
        }
        DebugNote();
    }

    void InsertNote(string line)
    {
        GameObject newNoteObj = Instantiate(notePrefab);
        Note newNote = newNoteObj.GetComponent<Note>();
        string[] split = line.Split(':');
        int type, lane;
        float time, hold_duration;

        type = int.Parse(split[0]);
        lane = int.Parse(split[1]);
        time = float.Parse(split[2]);
        hold_duration = float.Parse(split[3]);
        newNote.instantiateNote(type, lane, time, hold_duration, offset);
        newNote.audioSource = audioSource;

        Notes.Add(newNote);
    }

    void DebugNote()
    {
        foreach (Note note in Notes)
        {
            note.displayNote();
        }
    }

}
