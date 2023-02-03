using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject Explosion;
    private GameController Gamecontroller;
    public GameObject[] windObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        windObject = GameObject.FindGameObjectsWithTag("Wind");
        Gamecontroller = Object.FindObjectOfType<GameController>();
    }

    void Update()
    {
        rb.AddForce(-windObject[0].transform.forward * windObject[0].GetComponent<WindScript>().realWindStrength);
        if (transform.position.y < -100 || transform.position.x < -50 || transform.position.x > 50 || transform.position.z < -50 || transform.position.z > 50)
        {
            Gamecontroller.NextTurn();
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
