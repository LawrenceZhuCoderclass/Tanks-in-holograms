using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    public float windStrength;
    public float maxStartStrength;
    public float windStrengthChange;
    void Start()
    {
        //goede waardes voor de wind moeten worden gevonden
        windStrength = Random.Range(0, maxStartStrength);
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 90f);
    }
    public void changeWind()
    {
        windStrength += Random.Range(-windStrengthChange, windStrengthChange);
        transform.rotation = Quaternion.Euler(0, transform.rotation.y + Random.Range(-45f, 45f), 90f);
    }
}
