using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    //private Rigidbody rb;
    private float rotx;
    public float rotateSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float RotateHorizontal = Input.GetAxis("HorizontalCamera");
        rotx -= RotateHorizontal * rotateSpeed;

        transform.eulerAngles = new Vector3(0.0f, rotx, 0.0f);
    }
}