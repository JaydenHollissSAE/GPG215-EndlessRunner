using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RetryGame()
    {
        MusicManager.Instance.ClickSound();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainGameLoop");

    }
    public void ReturnToHome()
    {
        MusicManager.Instance.ClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
}
