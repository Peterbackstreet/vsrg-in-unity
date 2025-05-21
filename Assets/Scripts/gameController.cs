using System;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class gameController : MonoBehaviour
{
    private string title, artist;
    private string path = "Assets/Chart files/";
    private float BPM, offset;
    void Start()
    {
        readChartFile(path + "Aether Crest/data.txt");
    }

    void readChartFile(string file)
    {
        string[] lines = File.ReadAllLines(file);

        foreach (string line in lines)
        {
            if(line.StartsWith("#TITLE")) title = line.Substring(6);
            if(line.StartsWith("#ARTIST")) artist = line.Substring(7);
            if(line.StartsWith("#BPM")) BPM = float.Parse(line.Substring(4));
            if(line.StartsWith("#FILE")) path = line.Substring(5);
            if(line.StartsWith("#OFFSET")) offset = float.Parse(line.Substring(7));
        }
        Debug.Log("title: " + title);
        Debug.Log("artist: " + artist);
        Debug.Log("BPM: " + BPM);
        Debug.Log("file: " + file);
        Debug.Log("offset: " + offset);
    }

}
