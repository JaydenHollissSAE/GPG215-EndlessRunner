using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RetryGame()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene("MainGameLoop");

    }
    public void ReturnToHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
}
