using LootLocker.Requests;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    void Start()
    {
        //FetchLootlockerScores();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void FetchLootlockerScores()
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
            Debug.Log(response.text.ToString());
            Debug.Log("Successfully got score list!");
        });
    }
}
