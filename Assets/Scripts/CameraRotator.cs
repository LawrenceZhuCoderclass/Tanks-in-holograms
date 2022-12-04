using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    //private Rigidbody rb;
    private float rotx;
    public float rotateSpeed = 1.0f;
    public GameController Gamecontroller;
    private string cameraInput;
    public PlayerController player1;
    public PlayerController player2;
    private float startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.eulerAngles.y;
        //rb = GetComponent<Rigidbody>();
        if (Gamecontroller.controllerUsed)
        {
            cameraInput = "ControllerHorizontalCamera";
        }
        else
        {
            cameraInput = "HorizontalCamera";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float RotateHorizontal = Input.GetAxis(cameraInput);
        rotx -= RotateHorizontal * rotateSpeed;

        transform.eulerAngles = new Vector3(0.0f, rotx, 0.0f);
        player1.cameraAngle = (transform.eulerAngles.y-startRotation) * Mathf.Deg2Rad;
        player2.cameraAngle = (transform.eulerAngles.y-startRotation) * Mathf.Deg2Rad;
    }
}