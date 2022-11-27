using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Rigidbody rbBullet;

    private bool shootMode = false;

    public float speed;

    public GameObject bullet;
    public GameObject barrel;
    public GameObject hole;
    public GameObject cameraRotator;

    public float rotateSpeed = 5.0f;
    private float rotx = 0.0f;
    private float roty = 0.0f;
    private float MoveHorizontal;
    private float MoveVertical;
    private float CorrectMoveX;
    private float CorrectMoveZ;
    private float cameraAngle;
    private bool IsGrounded = false;
    public GameController Gamecontroller;
    public bool OwnTurn;
    private bool TouchOnce = true;
    private float Fuel = 10.0f;
    private float BrunRate = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbBullet = bullet.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        OwnTurn = false;
    }


    void FixedUpdate()
    {
        if (OwnTurn == true)
        {
            if (shootMode == true)
            {
                if (Input.GetKeyDown("k"))
                {
                    shootMode = false;
                    rb.constraints = RigidbodyConstraints.None;
                }
                float RotateHorizontal = Input.GetAxis("Horizontal");
                float RotateVertical = Input.GetAxis("Vertical");
                rotx -= RotateHorizontal * rotateSpeed;
                roty -= RotateVertical * rotateSpeed;

                roty = Mathf.Clamp(roty, -90.0f, 40.0f);
                barrel.transform.eulerAngles = new Vector3(roty, -rotx, 0.0f);
            }

            else if (shootMode == false)
            {
                MoveHorizontal = Input.GetAxis("Horizontal");
                MoveVertical = Input.GetAxis("Vertical");
                if (MoveHorizontal == 0.0f && MoveVertical == 0.0f && IsGrounded == true)
                {
                    rb.constraints = RigidbodyConstraints.FreezePosition;
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.None;
                }
                if (MoveHorizontal != 0.0f || MoveVertical != 0.0f)
                {
                    Fuel -= BrunRate * Time.deltaTime;
                }
                if (Input.GetKeyDown("k"))
                {
                    shootMode = true;
                    rb.constraints = RigidbodyConstraints.FreezePosition;
                    MoveHorizontal = 0.0f;
                    MoveVertical = 0.0f;
                }
                if (Fuel > 0.0f)
                {
                    cameraAngle = (cameraRotator.transform.eulerAngles.y) * Mathf.Deg2Rad;
                    CorrectMoveX = MoveHorizontal * Mathf.Cos(cameraAngle) + MoveVertical * Mathf.Sin(cameraAngle);
                    CorrectMoveZ = MoveVertical * Mathf.Cos(cameraAngle) - MoveHorizontal * Mathf.Sin(cameraAngle);

                    Vector3 move = new Vector3(CorrectMoveX, -1.0f, CorrectMoveZ);
                    rb.velocity = move * speed;
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.FreezePosition;
                }

            }
        }
        else
        {
            Vector3 move = new Vector3(0.0f, -1.0f, 0.0f);
            rb.velocity = move * speed;
        }
    }
    void Update()
    {
        if (shootMode == true)
        {
            if (Input.GetKeyDown("l"))
            {
                Instantiate(bullet, hole.transform.position, barrel.transform.rotation);
                shootMode = false;
                rb.constraints = RigidbodyConstraints.None;
                Gamecontroller.NextTurn();
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
            if (this.gameObject.tag == "Player_2" && TouchOnce == true)
            {
                OwnTurn = false;
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }
            else if (TouchOnce == true)
            {
                OwnTurn = true;
                rb.constraints = RigidbodyConstraints.None;
            }
            TouchOnce = false;
        }
    }
}
