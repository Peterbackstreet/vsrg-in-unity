using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class chartManager : MonoBehaviour
{
    public static chartManager Instance { get; private set; }
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
    public string title, artist, BPM, startOffset;
    public string audioPath, jacketPath, bgPath;
    private string pathPrefix = "Assets/Resources/Chart files/";

    void Start()
    {
        audioPath = "Chart files/" + "Aether Crest/Aether Crest";
        audioController.Instance.loadAudio(audioPath);
    }
}
