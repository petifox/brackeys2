using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour
{
    [HideInInspector] public static ResourceCounter instance;
    [SerializeField] int startAmount;
    [ReadOnly, SerializeField] int amount;
    [SerializeField] Text text;
    [SerializeField] string display = "BITS: {0}";

    private void Start()
    {
        instance = this;

        amount = startAmount;
        text.text = string.Format(display, 0);
    }
    [Button("add 10")]
    public void Add(int _amount = 10)
    {
        amount += _amount;
        text.text = string.Format(display, amount);
    }
    [Button("remove 10")]
    public void Remove(int _amount = 10)
    {
        amount -= _amount;
        amount = Mathf.Max(amount, 0);
        text.text = string.Format(display, amount);
    }
    public int CheckAmount()
    {
        return amount;
    }
}
