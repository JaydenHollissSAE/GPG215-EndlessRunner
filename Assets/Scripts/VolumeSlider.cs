using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = GameManager.instance.volume;
    }
    public void UpdateVolume()
    {
        GameManager.instance.volume = slider.value;
        MusicManager.Instance.ChangeVolume(slider.value);
        GameManager.instance.savedGame = false;
    }
}
