using UnityEngine;

public class DataSetter : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.speed = 1f;
        gameManager.gameStartTime = Time.time;
    }
}
