using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_script : MonoBehaviour
{
    private SphereCollider collider;
    private AudioSource BoomSound;
    private GameController Gamecontroller;
    private bool nextTurnStarted;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        BoomSound = GetComponent<AudioSource>();
        Gamecontroller = Object.FindObjectOfType<GameController>();
        nextTurnStarted = false;
    }

    void FixedUpdate()
    {
        collider.radius += 0.2f;
        if (collider.radius >= 1.6f)
        {
            if (!nextTurnStarted)
            {
                Gamecontroller.NextTurn();
                nextTurnStarted = true;
            }
            collider.enabled = false;
        }
    }

    private void Update()
    {
        if (!collider.enabled && !BoomSound.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        //Debug.Log("Hit");
        if (collision.gameObject.tag == "Player_1" || collision.gameObject.tag == "Player_2")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(Mathf.Pow(1/collider.radius, 1.2f));
            //collision.gameObject.GetComponent<PlayerController>().TakeDamage(2.0f);
        }
    }
}
