using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource activeAudio;
    private List<AudioSource> musicSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (AudioSource source in GetComponents(typeof(AudioSource)))
        {
            musicSources.Add(source);
            source.volume = 0.0f;
        }
        PlayMenu();
    }



    public void PlayMenu()
    {
        StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
        activeAudio = musicSources[0];
        StartCoroutine(AudioFadeEffects.FadeIn(activeAudio, 0.1f, false));
    }
    public void PlayMusic1()
    {
        StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
        activeAudio = musicSources[1];
        StartCoroutine(AudioFadeEffects.FadeIn(activeAudio, 0.1f, false));
    }
    public void PlayMusic2()
    {
        StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
        activeAudio = musicSources[2];
        StartCoroutine(AudioFadeEffects.FadeIn(activeAudio, 0.1f, false));
    }
    public void PlayMusic3()
    {
        StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
        activeAudio = musicSources[3];
        StartCoroutine(AudioFadeEffects.FadeIn(activeAudio, 0.1f, false));
    }
    public void PlayMusic4()
    {
        StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
        activeAudio = musicSources[4];
        StartCoroutine(AudioFadeEffects.FadeIn(activeAudio, 0.1f, false));
    }
    public void StopMusic()
    {
        StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
    }



}
