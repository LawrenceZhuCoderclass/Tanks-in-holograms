using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorPlayerChange : MonoBehaviour
{
    private MeshRenderer ballMeshRenderer;
    public Color damageColor;
    private float hp;
    private float startHP;

    void Start()
    {
        ballMeshRenderer = GetComponent<MeshRenderer>();
        startHP = gameObject.GetComponent<PlayerController>().hp;
    }

    void Update()
    {
        hp = gameObject.GetComponent<PlayerController>().hp;
        ballMeshRenderer.material.color = Color.Lerp(Color.white, damageColor, (startHP-hp) / startHP);
    }
}
