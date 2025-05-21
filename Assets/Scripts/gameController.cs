using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class gameController : MonoBehaviour
{
    private string title, artist;
    private string path = "Assets/Chart files/";
    private float BPM, offset;
    private List<Note> Notes = new List<Note>();
    void Start()
    {
        readChartFile(path + "Aether Crest/data.txt");
    }

    void readChartFile(string file)
    {
        string[] lines = File.ReadAllLines(file);

        foreach (string line in lines)
        {
            if (line.StartsWith("#TITLE")) title = line.Substring(6);
            else if (line.StartsWith("#ARTIST")) artist = line.Substring(7);
            else if (line.StartsWith("#BPM")) BPM = float.Parse(line.Substring(4));
            else if (line.StartsWith("#FILE")) path = line.Substring(5);
            else if (line.StartsWith("#OFFSET")) offset = float.Parse(line.Substring(7));
            else if (line.StartsWith("#")) insertNote(line.Substring(1));

        }
    }

    void insertNote(string line)
    {
        int type, lane;
        float time, hold_duration;

        string[] split = line.Split();

        type = int.Parse(split[0]);
        lane = int.Parse(split[1]);
        time = int.Parse(split[2]);
        hold_duration = int.Parse(split[3]);

        Notes.Add(new Note(type, lane, time, hold_duration));
    }

}
