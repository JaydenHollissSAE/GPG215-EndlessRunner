using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource activeAudio = null;
    private List<AudioSource> musicSources = new List<AudioSource>();
    [SerializeField] AudioClip clickSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (AudioSource source in GetComponents(typeof(AudioSource)))
            {
                musicSources.Add(source);
                source.volume = 0.0f;
            }
            musicSources[0].volume = GameManager.instance.volume;
            PlayMenu();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void ChangeVolume(float volume)
    {
        activeAudio.volume = volume;
    }



    public void PlayMenu()
    {
        if (activeAudio != musicSources[0])
        {
            StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
            activeAudio = musicSources[0];
            StartCoroutine(AudioFadeEffects.FadeIn(activeAudio, 0.05f, false));
        }
    }
    public void PlayMusic1()
    {
        if (activeAudio != musicSources[1])
        {
            StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
            activeAudio = musicSources[1];
            StartCoroutine(AudioFadeEffects.FadeIn(activeAudio, 0.05f, false));
        }
    }
    public void PlayMusic2()
    {
        if (activeAudio != musicSources[2])
        {
            StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
            activeAudio = musicSources[2];
            StartCoroutine(AudioFadeEffects.FadeIn(activeAudio, 0.05f, false));
        }
    }
    public void PlayMusic3()
    {
        if (activeAudio != musicSources[3])
        {
            StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
            activeAudio = musicSources[3];
            StartCoroutine(AudioFadeEffects.FadeIn(activeAudio, 0.05f, false));
        }
    }
    public void PlayMusic4()
    {
        if (activeAudio != musicSources[4])
        {
            StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
            activeAudio = musicSources[4];
            StartCoroutine(AudioFadeEffects.FadeIn(activeAudio, 0.05f, false));
        }
    }
    public void StopMusic()
    {
        StartCoroutine(AudioFadeEffects.FadeOut(activeAudio, 0.1f, false));
        activeAudio = null;
    }

    public void ClickSound()
    {
        musicSources[5].volume = 1.0f*GameManager.instance.volume;
        musicSources[5].PlayOneShot(clickSound);
    }



}
