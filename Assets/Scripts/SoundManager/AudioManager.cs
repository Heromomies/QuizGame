using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;

    public AudioMixerGroup group;

    public Slider slider;
    public AudioMixer mixer;

    public List<String> soundsToPlayOnAwake;
    private string _volumeParameter = "MasterVolume";
    void Awake()
    {
        mixer.SetFloat(_volumeParameter,PlayerPrefs.GetFloat("Volume"));
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    public void HandleSliderValueChanged(float value)
    { 
        value  = slider.value;
        mixer.SetFloat(_volumeParameter, value);
        PlayerPrefs.SetFloat("Volume",value);
    }
    private void Start()
    {
        PlayerPrefs.GetFloat("Volume", slider.value);
      
        foreach (Sound s in sounds)
        {
            s.source = s.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.maxDistance = s.maxDist;
            s.source.spatialBlend = 1;
        }

        foreach (var sound in soundsToPlayOnAwake)
        {
            Play(sound);
        }

        foreach (var sound in sounds)
        {
            sound.GetComponent<AudioSource>().outputAudioMixerGroup = group;
        }
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Le son n'a pas été trouvé");
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning("Le son n'a pas été trouvé");
        }
    }
}