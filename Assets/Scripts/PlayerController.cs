using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Rigidbody rbBullet;

    public bool shootMode = false;

    public float speed;

    public GameObject bullet;
    public GameObject barrelRotator;
    public GameObject barrel;
    public GameObject hole;
    //public GameObject cameraRotator;

    public float rotateSpeed = 5.0f;
    private float rotx = 0.0f;
    private float roty = 0.0f;
    private float MoveHorizontal;
    private float MoveVertical;
    private float CorrectMoveX;
    private float CorrectMoveZ;
    public float cameraAngle;
    private bool IsGrounded = false;
    public GameController Gamecontroller;
    public bool OwnTurn;
    private bool TouchOnce = true;
    public float Fuel = 5.0f;
    private float BrunRate = 1.0f;
    public float hp;
    public float startHP = 10.0f;
    public float beginPower;
    private float maxPower;
    public float power;
    public string otherPlayer;
    private string playerXInput;
    private string playerYInput;
    private string modeInput;
    private string shootInput;
    private string posPowerInput;
    private string negPowerInput;
    public playerText statsText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbBullet = bullet.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        OwnTurn = false;
        hp = startHP;
        maxPower = beginPower;
        power = maxPower/2;
        if (tag == "player_1")
        {
            otherPlayer = "player_2";
        }
        else 
        {
            otherPlayer = "Player_1";
        }

        if (Gamecontroller.controllerUsed)
        {
            playerXInput = "ControllerPlayerHorizontal";
            playerYInput = "ControllerPlayerVertical";
            modeInput = "ControllerSwitchMode";
            shootInput = "ControllerShoot";
            posPowerInput = "ControllerPositivePowerInput";
            negPowerInput = "ControllerNegativePowerInput";
        }
        else
        {
            playerXInput = "PlayerHorizontal";
            playerYInput = "PlayerVertical";
            modeInput = "SwitchMode";
            shootInput = "Shoot";
            posPowerInput = "positivePowerInput";
            negPowerInput = "negativePowerInput";
        }
    }


    void FixedUpdate()
    {
        if (OwnTurn == true)
        {
            if (shootMode == true)
            {
                //if (Input.GetKeyDown("k"))
                //{
                //    shootMode = false;
                //    rb.constraints = RigidbodyConstraints.None;
                //}
                float RotateHorizontal = Input.GetAxis(playerXInput);
                float RotateVertical = Input.GetAxis(playerYInput);
                rotx -= RotateHorizontal * rotateSpeed;
                roty -= RotateVertical * rotateSpeed;

                roty = Mathf.Clamp(roty, -40.0f, 90.0f);
                barrelRotator.transform.eulerAngles = new Vector3(-roty, -rotx, 0.0f);

                float PosChangePower = 1.5f * Input.GetAxis(posPowerInput);
                float NegChangePower = 1.5f * Input.GetAxis(negPowerInput);
                power += PosChangePower - NegChangePower;
                power = Mathf.Clamp(power, 0, maxPower);
            }

            else if (shootMode == false)
            {
                if (Gamecontroller.mirrorControls) { MoveHorizontal = -Input.GetAxis(playerXInput); }
                else { MoveHorizontal = Input.GetAxis(playerXInput); }
                MoveVertical = Input.GetAxis(playerYInput);

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
                //if (Input.GetKeyDown("k"))
                //{
                //    shootMode = true;
                //    rb.constraints = RigidbodyConstraints.FreezePosition;
                //    MoveHorizontal = 0.0f;
                //    MoveVertical = 0.0f;
                //}
                if (Fuel > 0.0f)
                {
                    //cameraAngle = (cameraRotator.transform.eulerAngles.y) * Mathf.Deg2Rad;
                    CorrectMoveX = MoveHorizontal * Mathf.Cos(cameraAngle) + MoveVertical * Mathf.Sin(cameraAngle);
                    CorrectMoveZ = MoveVertical * Mathf.Cos(cameraAngle) - MoveHorizontal * Mathf.Sin(cameraAngle);

                    Vector3 move = new Vector3(CorrectMoveX, -1.0f, CorrectMoveZ);
                    rb.velocity = move * speed;
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.FreezePosition;
                    Fuel = 0;
                }
            }
        }
        else
        {
            Vector3 move = new Vector3(0.0f, -1.0f, 0.0f);
            rb.velocity = move * speed;
        }
    }

    public void TakeDamage(float damage)
    {
        hp = hp - damage;
        hp = Mathf.Clamp(hp, 0, Mathf.Infinity);
        if (hp <= 0)
        {
            Gamecontroller.Winner_Declaration(otherPlayer);
        }
        maxPower = beginPower * hp / startHP;
        power = Mathf.Clamp(power, 0, maxPower);
    }

    void Update()
    {
        if (OwnTurn == true)
        {
            if (shootMode == true)
            {
                if (Input.GetButtonDown(shootInput))
                {
                    GameObject shotBullet = Instantiate(bullet, hole.transform.position, barrel.transform.rotation);
                    shotBullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, power, 0));
                    //shootMode = false;
                    rb.constraints = RigidbodyConstraints.FreezePosition;
                    OwnTurn = false;
                    statsText.ownTurn = false;
                    statsText.noText();
                    //Gamecontroller.NextTurn();
                }
                if (Input.GetButtonDown(modeInput))
                {
                    shootMode = false;
                    rb.constraints = RigidbodyConstraints.None;
                    statsText.shootMode = false;
                }
            }
            else
            {
                if (Input.GetButtonDown(modeInput))
                {
                    shootMode = true;
                    rb.constraints = RigidbodyConstraints.FreezePosition;
                    MoveHorizontal = 0.0f;
                    MoveVertical = 0.0f;
                    statsText.shootMode = true;
                }
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
            if (gameObject.tag == "Player_2" && TouchOnce == true)
            {
                OwnTurn = false;
                statsText.ownTurn = false;
                statsText.noText();
                statsText.shootMode = false;
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }
            else if (TouchOnce == true)
            {
                OwnTurn = true;
                statsText.ownTurn = true;
                statsText.shootMode = false;
                rb.constraints = RigidbodyConstraints.None;
            }
            TouchOnce = false;
        }
    }
}
