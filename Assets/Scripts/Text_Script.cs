using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Script : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 eulerAngleVelocity;
    public GameController gameController;
    public float Xrotation = 30.0f;
    public float Yrotation = 0.0f;
    public int normalstopper = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eulerAngleVelocity = new Vector3(0, 25, 0);
    }
    void FixedUpdate()
    {
        RotateText(normalstopper);
        /*Debug.Log(Xrotation);
        Debug.Log(Yrotation);
        Debug.Log(normalstopper);*/
       /* if (gameController.pyramidUsed)
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
        }*/
    }
    public void RotateText(int stop)
    {
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime * stop);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
    public void SetToHolofil()
    {
        transform.eulerAngles = new Vector3(-30.0f, 180f, 0.0f);
    }
    public void SetToNormal()
    {
        transform.eulerAngles = new Vector3(30.0f, 0.0f, 0.0f);
    }

}
