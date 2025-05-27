using TMPro;
using UnityEngine;

public class scoreHandler : MonoBehaviour
{
    private int score;
    [SerializeField] private TMP_Text scoreText;
    void Start()
    {
        scoreText.text = this.score.ToString();
    }

    public void addScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }
}
