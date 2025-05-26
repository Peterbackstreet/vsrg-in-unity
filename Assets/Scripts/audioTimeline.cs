using UnityEngine;
using UnityEngine.UI;

public class audioTimeline : MonoBehaviour
{
    private Slider slider;
    bool isDragging = false;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (!isDragging) slider.value = audioController.Instance.audioSource.time / audioController.Instance.chartLength;
    }

    public void changeValue()
    {
        if (isDragging)
        {
            float newTime = slider.value * audioController.Instance.chartLength;
            audioController.Instance.audioSource.time = newTime;
            slider.value = audioController.Instance.audioSource.time / audioController.Instance.chartLength;
        }
    }

    public void startDrag()
    {
        audioController.Instance.audioSource.time = slider.value * audioController.Instance.chartLength;
        isDragging = true;
    }

    public void endDrag()
    {
        isDragging = false;
    }
}
