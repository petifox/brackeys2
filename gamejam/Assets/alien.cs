using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using NaughtyAttributes;

public class alien : destructable
{
    public UnityEvent onDestruct;
    public int amountToAdd = 1;
    public float damageAmount;
    [Tag] public string ShipTag;
    GameObject _obj;
    public float speed;
    public override void destruct()
    {
        ResourceCounter.instance.Add(amountToAdd);
        onDestruct.Invoke();
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        _obj = GameObject.FindGameObjectsWithTag(ShipTag)[0];
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _obj.transform.position, Time.deltaTime * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ShipTag)
        {
            health.instance.Damage(damageAmount);
            onDestruct.Invoke();
            Destroy(gameObject);
        }
    }
}
