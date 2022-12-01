using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class rotate : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 eulerAngleVelocity;

    private float startHP;
    private float currentHP;
    private int percentHP;
    private TextMeshPro textMesh;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eulerAngleVelocity = new Vector3(0, 25, 0);

        textMesh = GetComponent<TextMeshPro>();
        startHP = transform.GetComponentInParent<PlayerController>().hp;
    }

    void FixedUpdate()
    {
        //rotate the score
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        currentHP = transform.GetComponentInParent<PlayerController>().hp;
        percentHP = (int)(currentHP / startHP * 100);
        textMesh.SetText(percentHP.ToString() + "%");

        transform.position = new Vector3(transform.parent.transform.position.x, 
                                         transform.parent.transform.position.y,
                                         transform.parent.transform.position.z);
    }
}
