using UnityEngine;

public class DataSetter : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        MusicManager.Instance.PlayMusic1();
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.speed = 1f;
        gameManager.gameStartTime = Time.time;
        gameManager.currentScore = 0;
        gameManager.resetScores = true;
    }
}
