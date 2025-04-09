using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGameLoop");
        Debug.Log("Loading Main Game Loop");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void CharacterCreator()
    {
        SceneManager.LoadScene("CharacterCreator");
    }

}
