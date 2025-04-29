using LootLocker.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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
    public bool gameActive = false;
    public string activePowerup;
    public float powerupEndTime;
    public float gameStartTime;
    public float speed = 1.0f;
    public int currentScore = 0;
    public List<Sprite> spriteList = new List<Sprite>();
    public ScoreUpdate scoreUpdate;
    public HighScoreUpdate highScoreUpdate;
    public bool resetScores = false;
    private int currentScoreOld = 0;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;
    private int currentAudio = 0;
    private JsonDataStorage jsonDataStorage = null;
    public bool savedGame = false;
    public float volume = 1.0f;
    public string username = "";

    [Header("Username UI")]
    public GameObject usernamePanel;
    public TMP_InputField usernameInputField;
    public Button usernameConfirmButton;
    public TextMeshProUGUI usernameErrorText;
    public GameObject mainMenuPanel;
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
        if (gameActive)
        {
            savedGame = false;
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
        else if (!savedGame)
        {
            savedGame = true;
            SaveGame();
        }
    }

    void ChangeAudio()
    {
        if (currentScore > 70 && currentAudio < 4)
        {
            currentAudio = 4;
            MusicManager.Instance.PlayMusic4();
        }
        else if (currentScore > 50 && currentAudio < 3)
        {
            currentAudio = 3;
            MusicManager.Instance.PlayMusic3();
        }
        else if (currentScore > 30 && currentAudio < 2)
        {
            currentAudio = 2;
            MusicManager.Instance.PlayMusic2();
        }
        else if (currentScore > 15 && currentAudio < 1)
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

    private void UploadScore()
    {
        string leaderboardKey = "endlessjumperboard";

        LootLockerSDKManager.SubmitScore("", highScore, leaderboardKey, "username:"+username , (response) =>
        {
            if (!response.success)
            {
                Debug.Log("Could not submit score!");
                Debug.Log(response.errorData.ToString());
                return;
            }
            Debug.Log("Successfully submitted score!");

        });
    }


    // Function to get saved data from json 

    // Function to save data to json


    void LoadGame() 
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            string jsonText = "";
            foreach (string line in lines)
            {
                jsonText += line;
            }
            jsonDataStorage = new JsonDataStorage();
            jsonDataStorage = JsonUtility.FromJson<JsonDataStorage>(jsonText);
        }
        else jsonDataStorage = new JsonDataStorage();
        highScore = jsonDataStorage.highScore;
        volume = jsonDataStorage.volume;
        username = jsonDataStorage.username;


    }

    public void SaveGame()
    {
        if (jsonDataStorage == null)
        {
            LoadGame();
        }
        //jsonDataStorage = new JsonDataStorage();

        jsonDataStorage.highScore = highScore;
        jsonDataStorage.username = username;
        jsonDataStorage.volume = volume;
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "save.json"), JsonUtility.ToJson(jsonDataStorage));
        UploadScore();
    }

    void Start()
    {
        if(string.IsNullOrEmpty(username))
        {
            UserNamePrompt();

        }

        else
        {
            mainMenuPanel.SetActive(true);
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (!response.success)
                {
                    Debug.Log("Error starting LootLocker Guest Session");

                    return;
                }

                Debug.Log("Successfully started LootLocker Session");
                Leaderboard.instance.FetchLootlockerScores();
            });
        }
        
    }

    void UserNamePrompt()
    {
        usernamePanel.SetActive(true);
        usernameConfirmButton.onClick.AddListener(SetUserName);

        Time.timeScale = 0;
    }
    void SetUserName()
    {
        string inputName = usernameInputField.text.Trim();

        if(string.IsNullOrEmpty(inputName))
        {
            usernameErrorText.text = "Name Cannot Be Empty";
            return;
        }

        if (inputName.Length>10)
        {
            usernameErrorText.text = "Max 10 Characters!";
            return;
        }

        username = inputName;
        usernameErrorText.text = "";
        usernamePanel.SetActive(false);
        Time.timeScale = 1;

        SaveGame();
        
    }
}



[Serializable]
public class JsonDataStorage
{
    public int highScore = 0;
    public float volume = 1f;
    public string username = "";
}

