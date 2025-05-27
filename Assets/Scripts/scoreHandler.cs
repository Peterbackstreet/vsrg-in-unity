using TMPro;
using UnityEngine;

public class scoreHandler : MonoBehaviour
{
    private int score;
    private TMP_Text scoreText;
    void Start()
    {
        scoreText = gameObject.GetComponent<TMP_Text>();
        scoreText.text = this.score.ToString();
    }

    public void addScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }
}
