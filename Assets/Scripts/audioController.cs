using UnityEngine;

public class audioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    bool pause = true;
    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Pause();
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

}
