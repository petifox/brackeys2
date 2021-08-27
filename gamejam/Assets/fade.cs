using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class fade : MonoBehaviour
{
    [HideInInspector] public static fade instance;
    public float speed;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        time = 100000;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, (Time.time - time) * speed, 0);
    }
    [Button("notify")]
    public void Notify()
    {
        time = Time.time;
    }
}
