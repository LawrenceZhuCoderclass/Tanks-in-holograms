using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Script : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 eulerAngleVelocity;
    public GameController gameController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eulerAngleVelocity = new Vector3(0, 25, 0);
    }

    void FixedUpdate()
    {
        if (gameController.pyramidUsed)
        {
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        else if (gameController.mirrorControls)
        {
            transform.eulerAngles = new Vector3(-30.0f, 180f, 0.0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(30.0f, 0.0f, 0.0f);
        }
    }
}
