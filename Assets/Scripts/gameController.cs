using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class gameController : MonoBehaviour
{
    public int score = 0, combo = 0, maxCombo = 0;
    private string title, artist;
    private string path = "Assets/Chart files/";
    private float BPM, offset = 0;
    [SerializeField] private GameObject notePrefab, longNotePrefab;
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
    }

    void InsertNote(string line)
    {
        string[] split = line.Split(':');
        int type, lane;
        float time, hold_duration;

        type = int.Parse(split[0]);
        lane = int.Parse(split[1]);
        time = float.Parse(split[2]);
        hold_duration = float.Parse(split[3]);

        if (type == 0) addNote(type, lane, time, hold_duration, notePrefab);
        else addNote(type, lane, time, hold_duration, longNotePrefab);

    }

    void addNote(int type, int lane, float time, float hold_duration, GameObject prefab)
    {
            GameObject newNoteObj = Instantiate(prefab);
            Note newNote = newNoteObj.GetComponent<Note>();
            newNote.instantiateNote(type, lane, time, hold_duration, offset);
            newNote.audioSource = audioSource;
            Notes.Add(newNote);
    }

    public void addCombo()
    {
        combo++;
        maxCombo = math.max(combo, maxCombo);
    }

    public void resetCombo()
    {
        combo = 0;
    }

    public void addScore(int score)
    {
        this.score += score;
    }

}
