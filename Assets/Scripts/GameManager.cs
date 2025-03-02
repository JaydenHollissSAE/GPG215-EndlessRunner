using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float highScore;
    public static GameManager instance;
    public int coinsCollected;
    public List<string> unlockedThemes = new List<string>();
    public string selectedTheme;
    public bool gameActive;
    public string activePowerup;
    public float powerupEndTime;
    public float gameStartTime;
    public float speed = 1.0f;

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

    // Function to get saved data from json 

    // Function to save data to json

}
