using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blade : destructable
{
    float startTime;
    public float delay;
    public int amountToAdd = 1;
    private void OnEnable()
    {
        startTime = Time.time;
    }
    public override void destruct()
    {
        if(startTime + delay < Time.time)
        {
            Destroy(gameObject);
            ResourceCounter.instance.Add(amountToAdd);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
