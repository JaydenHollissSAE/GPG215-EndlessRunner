using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] PlayerController player;


    public void Pause()
    {
        MusicManager.Instance.ClickSound();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        player.noJump = true;
    }

    public void Resume()
    {
        MusicManager.Instance.ClickSound();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToHome(int sceneID)
    {
        MusicManager.Instance.ClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

}
