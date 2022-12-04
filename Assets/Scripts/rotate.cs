using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class rotate : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 eulerAngleVelocity;

    private float startPow;
    private float currentPow;
    private int percentPow;
    private TextMeshPro textMesh;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eulerAngleVelocity = new Vector3(0, 25, 0);

        textMesh = GetComponent<TextMeshPro>();
        startPow = transform.GetComponentInParent<PlayerController>().beginPower;
    }

    void FixedUpdate()
    {
        //rotate the score
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        currentPow = transform.GetComponentInParent<PlayerController>().power;
        percentPow = (int)(currentPow / startPow * 100);
        textMesh.SetText(percentPow.ToString() + "%");

        transform.position = new Vector3(transform.parent.transform.position.x, 
                                         transform.parent.transform.position.y,
                                         transform.parent.transform.position.z);
    }
}
