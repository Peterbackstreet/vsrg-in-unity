using System.Collections.Generic;
using UnityEngine;

public class gameConfig : MonoBehaviour
{
    public static gameConfig Instance { get; private set; }
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
    public bool isEditorMode = false;
    public float scrollSpeed = 10f;
    public float perfectWindow = 20 * 0.001f;
    public float greatWindow = 50 * 0.001f;
    public float goodWindow = 100 * 0.001f;
    public float missWindow = 150 * 0.001f;
    public int perfectScore = 100;
    public int greatScore = 50;
    public int goodScore = 20;

}
