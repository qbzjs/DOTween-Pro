using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{

    public static SoundFX instance = null;

    // Loaded audio clips
    public AudioClip click;
    public AudioClip click2;
    public AudioClip zoomSlide;
    public AudioClip accept;
    public AudioClip delete;



    AudioSource[] audioSources;

    AudioSource lastFuseSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(instance.gameObject);
    }

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void playSound(ref AudioClip sound, float volume = 1, bool randomPitch = false)
    {
        AudioSource source = GetAvailableAudioSource();
        source.loop = false;
        source.clip = sound;

        if (randomPitch)
        {
            source.pitch = Random.Range(0.9f, 1.1f);
        }

        source.volume = volume;
        source.Play();
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying && source != audioSources[0])
            {
                return source;
            }
        }
        return audioSources[0];
    }


}
