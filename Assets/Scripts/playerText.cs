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
    private float startHP;
    private float currentHP;
    private float startFuel;
    private float currentFuel;

    private int percentPow;
    private int percentHP;
    private int percentFuel;

    public bool ownTurn;
    public bool shootMode;
    public bool gameOver;
    
    private TextMeshPro textMesh;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eulerAngleVelocity = new Vector3(0, 25, 0);
        textMesh = GetComponent<TextMeshPro>();
        textMesh.SetText("<br>");
        startPow = transform.GetComponentInParent<PlayerController>().beginPower;
        startHP = transform.GetComponentInParent<PlayerController>().startHP;
        startFuel = transform.GetComponentInParent<PlayerController>().Fuel;
        gameOver = false;
    }

    void FixedUpdate()
    {
        //rotate the score
        transform.position = new Vector3(transform.parent.transform.position.x,
                                         transform.parent.transform.position.y + 2.0f,
                                         transform.parent.transform.position.z);
        currentHP = transform.GetComponentInParent<PlayerController>().hp;
        percentHP = (int)(currentHP / startHP * 100);
        if (!gameOver && ownTurn && shootMode)
        {
            currentPow = transform.GetComponentInParent<PlayerController>().power;
            percentPow = (int)(currentPow / startPow * 100);
            textMesh.SetText("Power: " + percentPow.ToString() + "%<br>" +
                             "Health: " + percentHP.ToString() + "%");
        }
        else if (!gameOver && ownTurn && !shootMode)
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

    public void WinnerText()
    {
        //This function gets called when a player wins
        textMesh.SetText("Winner!");
    }

    public void LoserText()
    {
        //This function is called when a player loses
        textMesh.SetText("<br>");
    }
}
