using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class resource : destructable
{
    public UnityEvent onDestruct;
    public override void destruct()
    {
        //add objects
        onDestruct.Invoke();
        Destroy(gameObject);
    }
}
