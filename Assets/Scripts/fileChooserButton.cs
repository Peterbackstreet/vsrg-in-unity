using UnityEngine;
using System.IO;
using System.Collections.Generic;
using TMPro;
using SFB;

public class fileChooserButton : MonoBehaviour
{
    [SerializeField] private noteGenerator noteGenerator;
    [SerializeField] private TMP_InputField titleTMP, artistTMP, bpmTMP, start_offsetTMP;

    struct noteData
    {
        public int type, lane;
        public float beat, hold_duration;
        public noteData(int type, int lane, float beat, float hold_duration)
        {
            this.type = type;
            this.lane = lane;
            this.beat = beat;
            this.hold_duration = hold_duration;
        }
    }
    public void save()
    {
        string path = StandaloneFileBrowser.SaveFilePanel("Save Chart", "", "", "txt");
        writeFile(path);
    }

    void writeFile(string path)
    {
        string chartData = getChartData();
        List<string> metaData = getChartMetaData();

        File.WriteAllText(path, chartData);
        File.AppendAllLines(path, metaData);

    }

    string getChartData()
    {
        string chartData = "";
        string title = titleTMP.text;
        string artist = artistTMP.text;
        string BPM = bpmTMP.text;
        string start_offset = start_offsetTMP.text;
        string file = "Aether Crest";

        chartData += "#TITLE " + title + '\n';
        chartData += "#ARTIST " + artist + '\n';
        chartData += "#BPM " + BPM + '\n';
        chartData += "#FILE " + file + '\n';
        chartData += "#OFFSET " + start_offset + '\n';
        return chartData;
    }

    List<string> getChartMetaData()
    {
        List<string> metaData = new List<string>();
        List<noteData> dataList = new List<noteData>();
        foreach (Note note in noteGenerator.notes)
        {
            int type = note.type;
            int lane = note.lane;
            float beat = note.beat;
            float hold = note.hold_duration;
            noteData newData = new noteData(type, lane, beat, hold);
            dataList.Add(newData);
        }

        dataList.Sort((a, b) => a.beat.CompareTo(b.beat)); //sort by beat

        foreach (noteData data in dataList)
        {
            string dataString = "";
            dataString += "#" + data.type;
            dataString += ":" + data.lane;
            dataString += ":" + data.beat;
            dataString += ":" + data.hold_duration;
            metaData.Add(dataString);
        }
        return metaData;
    }
}