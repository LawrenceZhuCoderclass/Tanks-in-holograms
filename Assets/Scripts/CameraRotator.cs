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
    public bool pyramidCamera;
    private float mirrormultiplier;
    private float pyramidtextrotation;

    // Start is called before the first frame update
    void Start()
    {
        //mirrormultiplier = 1f;
        pyramidtextrotation = 0f;
        startRotation = transform.eulerAngles.y;
        //mirrormultiplier = 0.0f;
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

    // Update is called once per frame
    void FixedUpdate()
    {
        float RotateHorizontal = Input.GetAxis(cameraInputY);
        roty -= mirrormultiplier * RotateHorizontal * rotateSpeed;
        if (!pyramidCamera)
        {
            float RotateVertical = Input.GetAxis(cameraInputX);
            rotx -= RotateVertical * rotateSpeed;
            rotx = Mathf.Clamp(rotx, -30, 45);
        }
        else { rotx = 0.0f; }

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
    }
}