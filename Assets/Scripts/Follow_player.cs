using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_player : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.position.y + 0.8f, player.position.z);
    }
}
