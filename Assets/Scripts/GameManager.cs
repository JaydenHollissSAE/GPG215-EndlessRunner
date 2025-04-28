using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class ScoreUpdate : UnityEvent<int>
{

}

[System.Serializable]
public class HighScoreUpdate : UnityEvent<int>
{

}
public class GameManager : MonoBehaviour
{
    public int highScore = 0;
    public static GameManager instance;
    public int coinsCollected;
    public List<string> unlockedThemes = new List<string>();
    public string selectedTheme;
    public bool gameActive;
    public string activePowerup;
    public float powerupEndTime;
    public float gameStartTime;
    public float speed = 1.0f;
    public int currentScore = 0;
    public List<Sprite> spriteList = new List<Sprite>();
    public ScoreUpdate scoreUpdate;
    public HighScoreUpdate highScoreUpdate;
    public bool resetScores = true;
    private int currentScoreOld = 0;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;
    private int currentAudio = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Update()
    {
        if (resetScores)
        {
            //GameManager.instance.highScoreUpdate.Invoke(highScore);
            //GameManager.instance.scoreUpdate.Invoke(currentScore);
            currentScoreText = GameObject.FindGameObjectWithTag("CurrentScore").GetComponent<TextMeshProUGUI>();
            highScoreText = GameObject.FindGameObjectWithTag("HighScore").GetComponent<TextMeshProUGUI>();
            ScoreChange();
            HighScoreChange();
        }
        speed += 0.00001f * (Time.time - gameStartTime);
        currentScore = Mathf.RoundToInt((Time.time - gameStartTime) / 2 * speed);
        ScoreCheck();
        HighScoreCheck();
        ChangeAudio();
    }

    void ChangeAudio()
    {
        if (currentScore > 70 && currentAudio != 4)
        {
            currentAudio = 4;
            MusicManager.Instance.PlayMusic4();
        }
        else if (currentScore > 50 && currentAudio != 3)
        {
            currentAudio = 3;
            MusicManager.Instance.PlayMusic3();
        }
        else if (currentScore > 30 && currentAudio != 2)
        {
            currentAudio = 2;
            MusicManager.Instance.PlayMusic2();
        }
        else if (currentScore > 15 && currentAudio != 1)
        {
            currentAudio = 1;
            MusicManager.Instance.PlayMusic1();
        }
    }

    void ScoreCheck()
    {
        if (currentScoreOld != currentScore)
        {
            currentScoreOld = currentScore;
            //GameManager.instance.scoreUpdate.Invoke(currentScore);
            ScoreChange();
        }
    }

    void HighScoreCheck()
    {
        if (highScore < currentScore)
        {
            highScore = currentScore;
            //GameManager.instance.highScoreUpdate.Invoke(highScore);
            HighScoreChange();
        }
    }

    void ScoreChange()
    {
        currentScoreText.text = "Score:\n" + currentScore.ToString();
    }
    void HighScoreChange()
    {
        highScoreText.text = "High Score:\n" + highScore.ToString();
    }


    // Function to get saved data from json 

    // Function to save data to json

}
