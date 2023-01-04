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
        
        //kan waarschijnlijk beter geschreven
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
        //Gamecontroller.NextTurn(); (now placed in explosion script, because the next bullet would collide with the explosion if shot too early)
        Instantiate(Explosion, transform.position, transform.rotation);
        /*if (collision.gameObject.tag == "Player_1" || collision.gameObject.tag == "Player_2")
        {
            Debug.Log("Player hit!!!");
        }*/
        Destroy(gameObject);
    }
}
