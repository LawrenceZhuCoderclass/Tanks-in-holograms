using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_script : MonoBehaviour
{
    private SphereCollider collider;

    void Start()
    {
        collider = GetComponent<SphereCollider>();

    }

    void Update()
    {
        collider.radius += 0.2f;
        if (collider.radius >= 1.6f)
        {
            Destroy(this.gameObject);
        }

    }
    void OnCollisionEnter ()
    {
        Debug.Log("Hit");
    }
}
