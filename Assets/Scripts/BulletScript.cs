using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject gameObject;
    public SphereCollider Collider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Collider = GetComponent<SphereCollider>();
    }

    void Update()
    {
       transform.Translate(Vector3.forward * Time.deltaTime * 10);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player_1" || collision.gameObject.tag == "Player_2")
        {
            Debug.Log("Player hit!!!");
        }
        Destroy(gameObject);
    }
}
