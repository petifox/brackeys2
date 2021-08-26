using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        destructable ds = collision.gameObject.GetComponent<destructable>();
        if (ds != null)
        {
            ds.destruct();
        }
    }
}
