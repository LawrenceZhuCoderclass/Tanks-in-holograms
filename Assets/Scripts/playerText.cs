using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerText : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 eulerAngleVelocity;

    private float startPow;
    private float currentPow;
    private int percentPow;
    private float startHP;
    private float currentHP;
    private int percentHP;
    private float startFuel;
    private float currentFuel;
    private int percentFuel;
    public bool ownTurn;
    public bool shootMode;
    public bool dead;
    public bool winner;
    private TextMeshPro textMesh;

    //public GameController gameController;
    //public CameraRotator cameraAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eulerAngleVelocity = new Vector3(0, 25, 0);

        textMesh = GetComponent<TextMeshPro>();
        textMesh.SetText("<br>");
        startPow = transform.GetComponentInParent<PlayerController>().beginPower;
        startHP = transform.GetComponentInParent<PlayerController>().startHP;
        startFuel = transform.GetComponentInParent<PlayerController>().Fuel;
    }

    void FixedUpdate()
    {
        //rotate the score
        //Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
        //rb.MoveRotation(rb.rotation * deltaRotation);

        transform.position = new Vector3(transform.parent.transform.position.x,
                                         transform.parent.transform.position.y + 2.0f,
                                         transform.parent.transform.position.z);

        currentHP = transform.GetComponentInParent<PlayerController>().hp;
        percentHP = (int)(currentHP / startHP * 100);
        if (winner)
        {
            textMesh.SetText("Winner!");
        }
        else if (dead)
        {
            textMesh.SetText("<br>");
        }
        else if (ownTurn && shootMode)
        {
            currentPow = transform.GetComponentInParent<PlayerController>().power;
            percentPow = (int)(currentPow / startPow * 100);
            textMesh.SetText("Power: " + percentPow.ToString() + "%<br>" +
                             "Health: " + percentHP.ToString() + "%");
        }
        else if (ownTurn && !shootMode)
        {
            currentFuel = transform.GetComponentInParent<PlayerController>().Fuel;
            percentFuel = (int)(currentFuel / startFuel * 100);
            textMesh.SetText("Fuel: " + percentFuel.ToString() + "%<br>" +
                             "Health: " + percentHP.ToString() + "%");
        }
    }

    public void noText()
    {
        textMesh.SetText("<br>");
    }
}
