using TMPro;
using Unity.Collections;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

public class audioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TMP_Text chartTimeText;
    [SerializeField] private Slider timeSlider;

    public float chartLength, chartTime;
    private string chartLengthText;

    public static audioController Instance;
    void Awake()
    {
        Instance = this;
    }
    
    bool pause = true;
    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (audioSource.clip != null) chartLength = audioSource.clip.length;
        chartLengthText = FormatTime(chartLength);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Pause();
        displayChartTime();
    }

    void displayChartTime()
    {
        float curr_audioTime = audioSource.time;
        chartTime = curr_audioTime;
        chartTimeText.text = FormatTime(chartTime) + " / " + chartLengthText;
        timeSlider.value = chartTime / chartLength;

    }
    public void Play()
    {
        if (audioSource != null) audioSource.Play();
    }

    public void Pause()
    {
        pause = !pause;

        if (audioSource == null) return;
            
        if(pause) audioSource.Pause();
        else audioSource.Play();

    }

    public void Stop()
    {
        if (audioSource != null) audioSource.Stop();
    }
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        int milisec = Mathf.FloorToInt(timeInSeconds*100 %100);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milisec);
    }
}
