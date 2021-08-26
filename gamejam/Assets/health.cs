using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.Events;

public class health : MonoBehaviour
{
    public Slider _slider;
    float _health;

    [MinMaxSlider(0, 200)]
    public Vector2 MinMaxHealth;

    public UnityEvent onDie;
    private void Start()
    {
        _health = MinMaxHealth.y;
        _slider.minValue = MinMaxHealth.x;
        _slider.maxValue = MinMaxHealth.y;
        _slider.value = _health;
    }
    [Button("damage 10")]
    public void Damage(float amount = 10)
    {
        _health -= amount;
        _slider.value = _health;
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
