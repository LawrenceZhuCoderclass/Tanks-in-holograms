using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool shootMode = false;
    public float speed;
    public GameObject bullet;
    private Rigidbody rbBullet;
    public GameObject barrel;
    public Transform Barrel_rotator;
    public GameObject hole;
    public float rotateSpeed = 5.0f;
    private float rotx = 0.0f;
    private float roty = 0.0f;
    private float VertRotation;
    private float HorizontalRotation;
    private float MoveHorizontal;
    private float MoveVertical;
    private bool IsGrounded = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbBullet = bullet.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        Debug.Log(shootMode);
        if (shootMode == true)
        {
            if (Input.GetKeyDown("k"))
            {
                shootMode = false;
                rb.isKinematic = false;
            }
            Debug.Log(rb.velocity);
            float RotateHorizontal = Input.GetAxis("Horizontal");
            float RotateVertical = Input.GetAxis("Vertical");
            rotx -= RotateHorizontal * rotateSpeed;
            roty -= RotateVertical * rotateSpeed;
            VertRotation -= rotx;
            HorizontalRotation -= roty;
            VertRotation = Mathf.Clamp(roty, -90.0f, 0.0f);
            HorizontalRotation = Mathf.Clamp(rotx, -180.0f, 180.0f);
            barrel.transform.eulerAngles = new Vector3 (VertRotation, -HorizontalRotation, 0.0f);
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
    void Update()
    {
        if (shootMode == true)
        {
            if (Input.GetKeyDown("l"))
            {
                Debug.Log("trying to shoot");
                Instantiate(bullet, hole.transform.position, Barrel_rotator.rotation);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }
}
