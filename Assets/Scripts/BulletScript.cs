using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject gameObject;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
       transform.Translate(Vector3.forward * Time.deltaTime * 10);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
