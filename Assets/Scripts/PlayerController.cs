using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Rigidbody rbBullet;

    public bool shootMode = false;
    public bool OwnTurn;
    private bool TouchOnce = true;
    private bool IsGrounded = false;

    public float speed;
    private float mirrorcontrol;
    public float Fuel = 5.0f;
    private float BrunRate = 1.0f;
    public float hp;
    public float startHP = 10.0f;
    public float beginPower;
    private float maxPower;
    public float power;
    public float rotateSpeed = 5.0f;
    private float rotx = 0.0f;
    private float roty = 0.0f;
    private float MoveHorizontal;
    private float MoveVertical;
    private float CorrectMoveX;
    private float CorrectMoveZ;
    public float cameraAngle;

    public GameObject bullet;
    public GameObject barrelRotator;
    public GameObject barrel;
    public GameObject hole;

    public GameController Gamecontroller;

    public string otherPlayer;
    private string playerXInput;
    private string playerYInput;
    private string modeInput;
    private string shootInput;
    private string posPowerInput;
    private string negPowerInput;
    
    public playerText statsText;
    
    private PlayerState playerState;

    public enum PlayerState
    {
        Driving,
        Shooting
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbBullet = bullet.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        OwnTurn = false;
        hp = startHP;
        maxPower = beginPower;
        power = maxPower/2;
        if (tag == "Player_1")
        {
            otherPlayer = "player_2";
        }
        else 
        {
            otherPlayer = "player_1";
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
        if (Gamecontroller.mirrorControls) { mirrorcontrol = -1f; }
        else { mirrorcontrol = 1f; }
    }


    void FixedUpdate()
    {
        if (OwnTurn == true)
        {
            switch (playerState)
            {
                case PlayerState.Driving:
                    Drive();
                    break;
                case PlayerState.Shooting:
                    RotateBarrel();
                    break;

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
        if (OwnTurn == true)
        {
            switch(playerState)
            {
                case PlayerState.Driving:
                    if (Input.GetButtonDown(modeInput))
                    {
                        playerState = PlayerState.Shooting;
                        rb.constraints = RigidbodyConstraints.FreezePosition;
                        MoveHorizontal = 0.0f;
                        MoveVertical = 0.0f;
                        statsText.shootMode = true;
                    }
                    break;
                case PlayerState.Shooting:
                    ShootBullet();
                    break;

            }
        }
    }
    //--------------------------------------Touching the Ground Function--------------------------------------
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
    //--------------------------------------Driving Function--------------------------------------
    void Drive()
    {
        MoveHorizontal = mirrorcontrol * Input.GetAxis(playerXInput);
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

        if (Fuel > 0.0f)
        {
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
    //--------------------------------------Shooting Function--------------------------------------
    void ShootBullet()
    {
        if (Input.GetButtonDown(shootInput))
        {
            GameObject shotBullet = Instantiate(bullet, hole.transform.position, barrel.transform.rotation);
            shotBullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, power, 0));
            rb.constraints = RigidbodyConstraints.FreezePosition;
            OwnTurn = false;
            statsText.ownTurn = false;
            statsText.noText();
        }
        if (Input.GetButtonDown(modeInput))
        {
            playerState = PlayerState.Driving;
            rb.constraints = RigidbodyConstraints.None;
            statsText.shootMode = false;
        }
    }
    //--------------------------------------Barrel Rotation Function--------------------------------------
    void RotateBarrel()
    {
        float RotateHorizontal = mirrorcontrol * Input.GetAxis(playerXInput);
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
    //--------------------------------------Taking Damage Function--------------------------------------
    public void TakeDamage(float damage)
    {
        hp = hp - damage;
        if (hp <= 0)
        {
            Gamecontroller.Winner_Declaration(otherPlayer);
        }
        maxPower = beginPower * hp / startHP;
        power = Mathf.Clamp(power, 0, maxPower);
    }
    //--------------------------------------Change playerstate--------------------------------------
    public void ChangePlayerState(string newState)
    {
        if (newState == "Shooting") { playerState = PlayerState.Shooting; }
        else if (newState == "Driving") { playerState = PlayerState.Driving; }
    }
}
