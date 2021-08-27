using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blade : destructable
{
    float startTime;
    public float delay;
    private void OnEnable()
    {
        startTime = Time.time;
    }
    public override void destruct()
    {
        if(startTime + delay < Time.time)
        {
            Destroy(gameObject);
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
