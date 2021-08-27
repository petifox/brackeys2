using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine.Audio;

public class health : MonoBehaviour
{
    [HideInInspector] public static health instance;
    public Slider _slider;
    float _health;

    [MinMaxSlider(0, 200)]
    public Vector2 MinMaxHealth;

    [SerializeField] string _volumeParameter = "CutOff";
    [SerializeField] AudioMixer _mixer;

    public AnimationCurve multiplyer;

    [MinMaxSlider(10, 22000)]
    public Vector2 MinMaxHZ;

    public UnityEvent onDie;
    private void Start()
    {
        instance = this;
        _health = MinMaxHealth.y;
        _slider.minValue = MinMaxHealth.x;
        _slider.maxValue = MinMaxHealth.y;
        _slider.value = _health;

        _mixer.SetFloat(_volumeParameter, MinMaxHZ.y);
    }
    [Button("damage 10")]
    public void Damage(float amount = 10)
    {
        _health -= amount;
        _slider.value = _health;

        _mixer.SetFloat(_volumeParameter, Mathf.Lerp(MinMaxHZ.x, MinMaxHZ.y, multiplyer.Evaluate(_health / 100)));

        Debug.Log(MinMaxHZ + " " + _health / 100);
        if (_health <= MinMaxHealth.x)
        {
            onDie.Invoke();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            Damage(10);
        }
    }
}
