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
    private string title, artist;
    public float BPM, offset = 0;
    [SerializeField] private string song;
    private string pathPrefix = "Assets/Resources/", songPath = "Chart files/";
    [SerializeField] private Transform noteParent;
    [SerializeField] private noteGenerator noteGenerator;
    [SerializeField] private beatLineGenerator beatLineGenerator;
    void Start()
    {
        songPath += song + '/';
        ReadChartFile(pathPrefix + songPath + "data.txt");
    }

    void ReadChartFile(string file)
    {
        string[] lines = File.ReadAllLines(file);

        foreach (string line in lines)
        {
            if (line.StartsWith("#TITLE")) title = line.Substring(7);
            else if (line.StartsWith("#ARTIST")) artist = line.Substring(8);
            else if (line.StartsWith("#BPM")) BPM = float.Parse(line.Substring(5));
            else if (line.StartsWith("#FILE")) songPath += line.Substring(6);
            else if (line.StartsWith("#OFFSET")) offset = float.Parse(line.Substring(8));
            else if (line.StartsWith("#")) readNote(line.Substring(1));
        }
        audioController.Instance.loadAudio(songPath);
        beatLineGenerator.generatebeatLines(BPM, offset, audioController.Instance.chartLength);
    }

    void readNote(string line)
    {
        string[] split = line.Split(':');
        int type, lane;
        float beat, hold_duration;

        type = int.Parse(split[0]);
        lane = int.Parse(split[1]);
        beat = float.Parse(split[2]);
        hold_duration = float.Parse(split[3]);

        noteGenerator.generateNote(type, lane, beat, hold_duration);
    }

}
