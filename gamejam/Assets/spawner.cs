using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject _object;
    public Transform point;
    public Transform _parent;
    public float multi;
    public float _time;
    public float max = 25;

    //transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    //Instantiate(_object, point.position, point.rotation, _parent);

    private void Update()
    {
        if(Time.time > _time)
        {
            _time = +Time.time + max;
            for (int i = 0; i < Time.time / 4; i++)
            {
                transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                Instantiate(_object, point.position, point.rotation, _parent);
            }
        }
    }
}
