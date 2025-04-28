using System.Collections;
using UnityEngine;

public static class AudioFadeEffects
{
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime, bool stopAudio = true)
    {
        if (audioSource != null)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime * GameManager.instance.volume;

                yield return null;
            }


            if (stopAudio)
            {
                audioSource.Stop();
                audioSource.volume = startVolume;
            }
            else audioSource.volume = 0;
        }

    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, bool stopAudio = true)
    {
        if (audioSource != null)
        {
            float startVolume = 0.2f * GameManager.instance.volume;

            audioSource.volume = 0;
            if (stopAudio) audioSource.Play();

            while (audioSource.volume < 1.0f * GameManager.instance.volume)
            {
                audioSource.volume += startVolume * Time.deltaTime / FadeTime * GameManager.instance.volume;

                yield return null;
            }

            audioSource.volume = 1f * GameManager.instance.volume;
        }


    }
}