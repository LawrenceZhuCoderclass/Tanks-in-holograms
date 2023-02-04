using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Script : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 eulerAngleVelocity;

    public GameController gameController;

    public int normalstopper = 0;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eulerAngleVelocity = new Vector3(0, 25, 0);
    }
    void FixedUpdate()
    {
        RotateText(normalstopper);
    }
    private void RotateText(int stop)
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