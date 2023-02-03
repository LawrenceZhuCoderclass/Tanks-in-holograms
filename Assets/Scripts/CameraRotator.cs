using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private float roty;
    private float rotx;
    public float rotateSpeed = 1.0f;
    private float mirrormultiplier;
    private float pyramidtextrotation;
    private float startRotation;

    public GameController Gamecontroller;

    private string cameraInputY;
    private string cameraInputX;

    public PlayerController player1;
    public PlayerController player2;

    public GameObject player_1_text;
    public GameObject player_2_text;

    public bool pyramidCamera;

    void Start()
    {
        pyramidtextrotation = 0f;
        startRotation = transform.eulerAngles.y;
        if (Gamecontroller.controllerUsed)
        {
            cameraInputX = "ControllerHorizontalCamera";
            cameraInputY = "ControllerVerticalCamera";
        }
        else
        {
            cameraInputX = "HorizontalCamera";
            cameraInputY = "VerticalCamera";
        }
        if (Gamecontroller.mirrorControls && !Gamecontroller.pyramidUsed)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y + 7f, transform.position.z);
            this.gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(30.0f, 0.0f, 90.0f);
        }
        else if (Gamecontroller.pyramidUsed)
        {
            pyramidtextrotation = -90f;
        }
    }

    void FixedUpdate()
    {
        float RotateHorizontal = Input.GetAxis(cameraInputX);
        roty -= mirrormultiplier * RotateHorizontal * rotateSpeed;
        if (!pyramidCamera)
        {
            float RotateVertical = Input.GetAxis(cameraInputY);
            rotx -= RotateVertical * rotateSpeed;
            rotx = Mathf.Clamp(rotx, -30, 45);
        }
        else 
        {
            rotx = 0.0f;
        } 

        transform.eulerAngles = new Vector3(rotx, roty, 0.0f);
        player1.cameraAngle = (transform.eulerAngles.y-startRotation) * Mathf.Deg2Rad;
        player2.cameraAngle = (transform.eulerAngles.y-startRotation) * Mathf.Deg2Rad;
        player_1_text.transform.eulerAngles = new Vector3(0.0f, roty - 90f + pyramidtextrotation + mirrormultiplier * 90f, 0.0f);
        player_2_text.transform.eulerAngles = new Vector3(0.0f, roty - 90f + pyramidtextrotation + mirrormultiplier * 90f, 0.0f);
    }

    public void startGame()
    {
        if (Gamecontroller.mirrorControls)
        {
            mirrormultiplier = -1f;
        }
        else
        {
            mirrormultiplier = 1f;
        }
        if (Gamecontroller.controllerUsed)
        {
            cameraInputX = "ControllerHorizontalCamera";
            cameraInputY = "ControllerVerticalCamera";
        }
        else
        {
            cameraInputX = "HorizontalCamera";
            cameraInputY = "VerticalCamera";
        }
    }
}