using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class resource : destructable
{
    public UnityEvent onDestruct;
    public int amountToAdd = 1;
    public override void destruct()
    {
        ResourceCounter.instance.Add(amountToAdd);
        onDestruct.Invoke();
        Destroy(gameObject);
    }
}
