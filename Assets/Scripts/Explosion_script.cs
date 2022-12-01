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
    void OnCollisionEnter (Collision collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.tag == "Player_1" || collision.gameObject.tag == "Player_2")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(Mathf.Pow(1/collider.radius, 1.2f));
            //collision.gameObject.GetComponent<PlayerController>().TakeDamage(2.0f);
        }
    }
}
