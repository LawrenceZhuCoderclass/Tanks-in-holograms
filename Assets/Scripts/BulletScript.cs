using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
       transform.Translate(Vector3.up * Time.deltaTime * 3);
    }
}
