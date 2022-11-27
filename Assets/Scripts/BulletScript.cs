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
       transform.Translate(Vector3.forward * Time.deltaTime * 10);
    }
    void OnCollisionEnter(Collision collision)
    {
        Instantiate(Explosion, this.transform.position, this.transform.rotation);
        /*if (collision.gameObject.tag == "Player_1" || collision.gameObject.tag == "Player_2")
        {
            Debug.Log("Player hit!!!");
        }*/
        Destroy(this.gameObject);
    }
}
