using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class gameController : MonoBehaviour
{
    public static gameController Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int score = 0, combo = 0, maxCombo = 0;
    private string title, artist;
    private string path = "Assets/Chart files/";
    public float BPM, offset = 0;
    public float scrollSpeed = 10f;
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private GameObject notePrefab, longNotePrefab;
    public List<Note> Notes = new List<Note>();
    void Start()
    {
        ReadChartFile(path + "Aether Crest/data.txt");
        updateComboText();
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
        float beat, hold_duration;

        type = int.Parse(split[0]);
        lane = int.Parse(split[1]);
        beat = float.Parse(split[2]);
        hold_duration = float.Parse(split[3]);

        if (type == 0) addNote(type, lane, beat, hold_duration, notePrefab);
        else addNote(type, lane, beat, hold_duration, longNotePrefab);

    }

    void addNote(int type, int lane, float time, float hold_duration, GameObject prefab)
    {
        GameObject newNoteObj = Instantiate(prefab);
        Note newNote = newNoteObj.GetComponent<Note>();
        newNote.instantiateNote(type, lane, time, hold_duration, offset);
        Notes.Add(newNote);
    }

    public void addCombo()
    {
        combo++;
        maxCombo = math.max(combo, maxCombo);
        updateComboText();
    }

    public void resetCombo()
    {
        combo = 0;
        updateComboText();
    }

    public void addScore(int score)
    {
        this.score += score;
    }

    private void updateComboText()
    {
        comboText.text = combo.ToString();
    }
}
