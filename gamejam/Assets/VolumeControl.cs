using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] string _volumeParameter = "MasterVolume";
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider slider;
    [SerializeField] float _multiplier = 30;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(_volumeParameter, slider.value);
    }
    private void OnEnable()
    {
        slider.onValueChanged.AddListener(SetVolume);
    }
    public void SetVolume(float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * _multiplier);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, slider.value); 
    }
}
