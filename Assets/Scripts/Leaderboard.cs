using LootLocker.Requests;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Leaderboard : MonoBehaviour
{
    private string jsonInput;
    public static Leaderboard instance;
    public LeaderBoardParse leaderBoardParse = new LeaderBoardParse();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }


    void Start()
    {
        //FetchLootlockerScores();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void FetchLootlockerScores()
    {
        string leaderboardKey = "endlessjumperboard";
        int count = 50;

        LootLockerSDKManager.GetScoreList(leaderboardKey, count, 0, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("Could not get score list!");
                Debug.Log(response.errorData.ToString());
                //FetchLootlockerScores();
                return;
            }
            ParseLootlocker(response.text.ToString());
            Debug.Log("Successfully got score list!");
        });
        
    }


    void ParseLootlocker(string input)
    {
        //Debug.Log("running");
        //Debug.Log(input);

        leaderBoardParse = new LeaderBoardParse();
        leaderBoardParse = JsonUtility.FromJson<LeaderBoardParse>(input);
    }

}



[Serializable]
public class LeaderBoardParse
{
    public LeaderBoardItems[] items;
}

[Serializable]
public class LeaderBoardItems
{
    public string metadata;
    public int rank;
    public int score;
}