using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class EndGame : MonoBehaviour
{
    public GameObject gameOverPanel;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.name == "Player")
        {
            MusicManager.Instance.StopMusic();
            gameOverPanel.SetActive(true);
            GameManager.instance.gameActive = false;
            audioSource.Play();
         // Time.timeScale = 0f;

        }
    }
}
