using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    //public AudioMixerGroup mixer;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;
            s.source.outputAudioMixerGroup = s.mixer;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
            if (s.dontStop == true)
            {
                s.source.ignoreListenerPause = true;
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.isPlaying = true;
        //s.source.Play();
        s.source.PlayOneShot(s.Clip);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.isPlaying = false;
        s.source.Stop();
    }
    public void PlayIf(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (s.isPlaying == true)
        {
            return;
        }
        s.isPlaying = true;
        s.source.PlayOneShot(s.Clip);
    }
    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            if (s.dontStop == false)
            {
                s.isPlaying = false;
                s.source.Stop();
            }
        }
    }

    public void PauseAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Pause();
        }
    }

    public void StartAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.UnPause();
        }
    }


    private void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}