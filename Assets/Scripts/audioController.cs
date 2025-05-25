using Mono.Cecil;
using TMPro;
using Unity.Collections;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

public class audioController : MonoBehaviour
{
    public static audioController Instance { get; private set; }
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
    [SerializeField] public AudioSource audioSource;
    [SerializeField] private TMP_Text chartTimeText;
    [SerializeField] private Slider timeSlider;

    public float chartLength, chartTime;
    public AudioClip audioClip;
    private string chartLengthText;
    bool pause = true;
    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Pause();
        if (audioSource.clip) displayChartTime();
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

        if (pause) audioSource.Pause();
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
        int milisec = Mathf.FloorToInt(timeInSeconds * 100 % 100);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milisec);
    }

    public void loadAudio(string audio)
    {
        audioClip = Resources.Load<AudioClip>(audio);
        audioSource.clip = audioClip;
        if (audioSource.clip != null) chartLength = audioSource.clip.length;
        chartLengthText = FormatTime(chartLength);
    }
}
