using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Rigidbody rbBullet;
    public Transform Barrel_rotator;

    private bool shootMode = false;
    
    public float speed;
    
    public GameObject bullet;
    public GameObject barrel;
    public GameObject hole;
    
    public float rotateSpeed = 5.0f;
    private float rotx = 0.0f;
    private float roty = 0.0f;
    private float VertRotation;
    private float HorizontalRotation;
    private float MoveHorizontal;
    private float MoveVertical;
    private bool IsGrounded = false;
    public GameController Gamecontroller;
    public bool OwnTurn;
    private bool TouchOnce = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbBullet = bullet.GetComponent<Rigidbody>();
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
                    rb.isKinematic = false;
                }
                float RotateHorizontal = Input.GetAxis("Horizontal");
                float RotateVertical = Input.GetAxis("Vertical");
                if (this.gameObject.tag == "Player_1")
                {
                    rotx -= RotateHorizontal * rotateSpeed;
                    roty -= RotateVertical * rotateSpeed;
                    VertRotation -= rotx;
                    HorizontalRotation -= roty;
                    
                    VertRotation = Mathf.Clamp(roty, -90.0f, 0.0f);
                    HorizontalRotation = Mathf.Clamp(rotx, -180.0f, 180.0f);
                    barrel.transform.eulerAngles = new Vector3 (VertRotation, -HorizontalRotation, 0.0f);
                }
                else 
                {
                    rotx -= RotateHorizontal * rotateSpeed;
                    roty -= -RotateVertical * rotateSpeed;
                    VertRotation -= rotx;
                    HorizontalRotation -= roty;
                    
                    VertRotation = Mathf.Clamp(roty, -90.0f, 0.0f);
                    HorizontalRotation = Mathf.Clamp(rotx, -180.0f, 180.0f);
                    barrel.transform.eulerAngles = new Vector3 (VertRotation, -HorizontalRotation +180.0f, 0.0f);
                }
            }

            else if (shootMode == false)
            {         
                MoveHorizontal = Input.GetAxis("Horizontal");
                MoveVertical = Input.GetAxis("Vertical");
                if (MoveHorizontal == 0.0f && MoveVertical == 0.0f && IsGrounded == true)
                {
                    rb.isKinematic = true;
                }
                else 
                {
                    rb.isKinematic = false;
                }
                if (Input.GetKeyDown("k"))
                {
                    shootMode = true;
                    rb.isKinematic = true;
                    MoveHorizontal = 0.0f;
                    MoveVertical = 0.0f;
                }  
                Vector3 move = new Vector3(MoveHorizontal, -1.0f, MoveVertical);
                rb.velocity = move * speed;
            }
        }

    }
    void Update()
    {
        if (shootMode == true)
        {
            if (Input.GetKeyDown("l"))
            {
                Instantiate(bullet, hole.transform.position, Barrel_rotator.rotation);
                shootMode = false;
                rb.isKinematic = false;
                Gamecontroller.NextTurn();
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.tag == "Ground")
            {
                IsGrounded = true;              
                if(this.gameObject.tag == "Player_2" && TouchOnce == true)
                {
                    OwnTurn = false;
                    rb.isKinematic = true;
                }
                TouchOnce = false;
            }

    }
}
