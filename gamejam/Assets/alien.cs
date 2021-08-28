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
    public string PopName = "pop";
    public string PunchName = "punch";
    public override void destruct()
    {
        ResourceCounter.instance.Add(amountToAdd);
        AudioManager.instance.Play(PopName + Random.Range(0, 5).ToString());
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
            AudioManager.instance.Play(PunchName + Random.Range(0, 5).ToString());
            health.instance.Damage(damageAmount);
            onDestruct.Invoke();
            Destroy(gameObject);
        }
    }
}
