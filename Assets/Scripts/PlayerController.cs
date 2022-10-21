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
    public GameObject hole;
    public float rotateSpeed = 5.0f;
    private float rotx = 0.0f;
    private float roty = 0.0f;
    private float VertRotation;
    private float HorizontalRotation;

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
            }
            if (Input.GetKeyDown("l"))
            {
                Debug.Log("trying to shoot");
                Instantiate(bullet, hole.transform.position, barrel.transform.rotation);
            }
            float RotateHorizontal = Input.GetAxis("Horizontal");
            float RotateVertical = Input.GetAxis("Vertical");
            rotx -= RotateHorizontal * rotateSpeed;
            roty -= RotateVertical * rotateSpeed;
            Debug.Log("rotx: " + rotx);
            Debug.Log("roty: " + roty);
            VertRotation -= rotx;
            HorizontalRotation -= roty;
            VertRotation = Mathf.Clamp(roty, -90.0f, 0.0f);
            HorizontalRotation = Mathf.Clamp(rotx, -180.0f, 180.0f);
            barrel.transform.eulerAngles = new Vector3 (VertRotation, -HorizontalRotation, 0.0f);
        }

        else if (shootMode == false)
        {
            if (Input.GetKeyDown("k"))
            {
                shootMode = true;
            }
            
            float MoveHorizontal = Input.GetAxis("Horizontal");
            float MoveVertical = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(MoveHorizontal, -1.0f, MoveVertical);
            rb.velocity = move * speed;
        }
    }
}
        /*if (shootMode = true)
        {
            float RotateHorizontal = Input.GetAxis("Horizontal");
            float RotateVertical = Input.GetAxis("Vertical");
            Quaternion rotation = Quaternion.Euler(RotateHorizontal, RotateVertical, 0);
            Debug.Log(rotation);
            barrel.transform.rotation = Quaternion.Slerp(barrel.transform.rotation, rotation, Time.deltaTime * rotateSpeed);

        }*/
        /*else if (shootMode = false)
        {
            Debug.Log("you are now out of shoot mode");
            if (Input.GetKeyDown("j"))
            {
                shootMode = true;
                Debug.Log("you are now out of shootmode");
            }
            Debug.Log("you are now playing");*/
    //}