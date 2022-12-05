using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    //private Rigidbody rb;
    private float roty;
    private float rotx;
    public float rotateSpeed = 1.0f;
    public GameController Gamecontroller;
    private string cameraInputY;
    private string cameraInputX;
    public PlayerController player1;
    public PlayerController player2;
    private float startRotation;
    public GameObject player_1_text;
    public GameObject player_2_text;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.eulerAngles.y;
        //rb = GetComponent<Rigidbody>();
        if (Gamecontroller.controllerUsed)
        {
            cameraInputY = "ControllerHorizontalCamera";
            cameraInputX = "ControllerVerticalCamera";
        }
        else
        {
            cameraInputY = "HorizontalCamera";
            cameraInputX = "VerticalCamera";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float RotateHorizontal = Input.GetAxis(cameraInputY);
        roty -= RotateHorizontal * rotateSpeed;
        float RotateVertical = Input.GetAxis(cameraInputX);
        rotx -= RotateVertical * rotateSpeed;
        rotx = Mathf.Clamp(rotx, -30, 45);

        transform.eulerAngles = new Vector3(rotx, roty, 0.0f);
        player1.cameraAngle = (transform.eulerAngles.y-startRotation) * Mathf.Deg2Rad;
        player2.cameraAngle = (transform.eulerAngles.y-startRotation) * Mathf.Deg2Rad;
        player_1_text.transform.eulerAngles = new Vector3(0.0f, roty, 0.0f);
        player_2_text.transform.eulerAngles = new Vector3(0.0f, roty, 0.0f);
    }
}