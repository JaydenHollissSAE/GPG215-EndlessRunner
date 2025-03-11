using TMPro;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{

    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI currentScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameManager.instance.scoreUpdate.AddListener(ChangeScore);
        GameManager.instance.highScoreUpdate.AddListener(ChangeHighScore);
    }
    private void Start()
    {
        currentScoreText = GameObject.FindGameObjectWithTag("CurrentScore").GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.FindGameObjectWithTag("HighScore").GetComponent<TextMeshProUGUI>();
    }

    void ChangeScore(int score) 
    {
        Debug.Log("RunChangeScore");
        currentScoreText.text = "Score:\n"+score.ToString();
    }
    void ChangeHighScore(int score)
    {
        Debug.Log("RunChangeHighScore");
        highScoreText.text = "Score:\n" + score.ToString();
    }
}
