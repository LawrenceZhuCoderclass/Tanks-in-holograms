using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject Explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < -100 || transform.position.x < -50 || transform.position.x > 50 || transform.position.z < -50 || transform.position.z > 50)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        /*if (collision.gameObject.tag == "Player_1" || collision.gameObject.tag == "Player_2")
        {
            Debug.Log("Player hit!!!");
        }*/
        Destroy(gameObject);
    }
}
