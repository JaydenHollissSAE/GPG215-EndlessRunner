using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame()
    {
        MusicManager.Instance.ClickSound();
        SceneManager.LoadScene("MainGameLoop");
        Debug.Log("Loading Main Game Loop");
    }

    public void QuitGame()
    {
        MusicManager.Instance.ClickSound();
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        MusicManager.Instance.ClickSound();
        SceneManager.LoadScene("StartMenu");
    }
    public void CharacterCreator()
    {
        MusicManager.Instance.ClickSound();
        SceneManager.LoadScene("CharacterCreator");
    }

}
