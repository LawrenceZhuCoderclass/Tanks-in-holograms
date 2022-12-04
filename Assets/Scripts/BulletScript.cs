using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject Explosion;
    private GameController Gamecontroller;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Gamecontroller = Object.FindObjectOfType<GameController>();
    }

    void Update()
    {
        //rb.AddForce(windObject.transform.forward * 10);
        if (transform.position.y < -100 || transform.position.x < -50 || transform.position.x > 50 || transform.position.z < -50 || transform.position.z > 50)
        {
            Gamecontroller.NextTurn();
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
        Gamecontroller.NextTurn();
        Destroy(gameObject);
    }
}
