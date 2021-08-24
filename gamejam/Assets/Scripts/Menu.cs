using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [HideInInspector] public int id;
    public void Activate()
    {
        gameObject.SetActive(true);
        //animation
    }
    public void DeActivate()
    {
        gameObject.SetActive(false);
        //animation
    }
}
