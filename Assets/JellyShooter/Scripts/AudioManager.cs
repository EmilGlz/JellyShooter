using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region Singleton
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    #endregion

    public Sound[] sounds;

    public void Play(int soundIndex)
    {
        Sound s = sounds[soundIndex];
        if (s == null || !CommonData.Instance.soundOn)
        {
            return;
        }
        s.source.Play();
    }

    public void Stop(int soundIndex)
    {
        //Sound s = Array.Find(sounds, sound => sound.name == name); // find in sounds array a sound whose name is name
        Sound s = sounds[soundIndex];
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }

    public void SetVolume(int soundIndex, float soundVolume)
    {
        //Sound s = Array.Find(sounds, sound => sound.name == name); // find in sounds array a sound whose name is name
        Sound s = sounds[soundIndex];
        if (s == null)
        {
            return;
        }
        s.source.volume = soundVolume;
    }

    public bool IsPlaying(int soundIndex)
    {
        Sound s = sounds[soundIndex];
        if (s == null)
        {
            return false;
        }
        if (s.source.isPlaying)
        {
            Debug.Log("Is playing");
            return true;
        }
        else
        {
            Debug.Log("No music");
            return false;
        }
    }
}
