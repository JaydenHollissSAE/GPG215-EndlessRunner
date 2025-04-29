using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class LeaderboardSetup : MonoBehaviour
{
    List<GameObject> placements = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Leaderboard.instance.FetchLootlockerScores();
        placements = GameObject.FindGameObjectsWithTag("LeaderboardItem").ToList();

        List<LeaderBoardItems> leaderBoardItems = Leaderboard.instance.leaderBoardParse.items.ToList();

        for (int i = 0; i < placements.Count; i++)
        {
            if (i >= leaderBoardItems.Count) placements[i].active = false;
            else
            {
                for (int o = 0; o < placements[i].transform.childCount; o++)
                {
                    GameObject item = placements[i].transform.GetChild(o).gameObject;
                    if (item.name == "Number")
                    {
                        item.GetComponent<TextMeshProUGUI>().text = leaderBoardItems[i].rank.ToString();
                    }
                    else if (item.name == "Score")
                    {
                        item.GetComponent<TextMeshProUGUI>().text = "Score: "+leaderBoardItems[i].score.ToString();
                    }
                    else if (item.name == "Name")
                    {
                        string name = leaderBoardItems[i].metadata.Replace("username:", "");
                        if (name == "") name = "Anonymous";
                        item.GetComponent<TextMeshProUGUI>().text = name;
                    }
                }
            }
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
