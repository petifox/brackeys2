using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class sword : MonoBehaviour
{
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        destructable ds = collision.gameObject.GetComponent<destructable>();
        if (ds != null)
        {
            ds.destruct();
        }
    }
}
