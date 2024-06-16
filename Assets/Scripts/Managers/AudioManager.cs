using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SetUpSounds();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
           Destroy(gameObject);
        }
    }


    public void Start()
    {
        Sound[] array = sounds;
        foreach (Sound sound in array)
        {
            if (sound.playOnAwake)
            {
                sound.source.Play();
            }
        }
    }

    public void Play(string name)
    {
        Sound sound2 = Array.Find(sounds, (Sound sound) => sound.name == name);
        if (sound2 == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
        }
        else
        {
            sound2.source.Play();
        }
    }

    public void PausePlay(string name, bool paused)
    {
        Sound sound2 = Array.Find(sounds, (Sound sound) => sound.name == name);
        if (sound2 == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
        }
        else if (paused)
        {
            sound2.source.Pause();
        }
        else if (!paused)
        {
            sound2.source.UnPause();
        }
    }

    public void StopPlay(string name)
    {
        Sound sound2 = Array.Find(sounds, (Sound sound) => sound.name == name);
        if (sound2 == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
        }
        else
        {
            sound2.source.Stop();
        }
    }

    private void SetUpSounds()
    {
        Sound[] array = sounds;
        foreach (Sound sound in array)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }
    }

    public void PlayWhenGameStop()
    {
        StopPlay(AudioName.TimeRunningOut);
        PausePlay(AudioName.BG, paused: true);
        Play(AudioName.GameEnd);
    }

    public void PlayWhenWinGame()
    {
        StopPlay(AudioName.TimeRunningOut);
        PausePlay(AudioName.BG, paused: true);
        Play(AudioName.Win);
    }
}
