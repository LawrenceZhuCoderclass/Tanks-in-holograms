using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindScript : MonoBehaviour
{
    private float windStrength;
    public float maxStartStrength;
    public float windStrengthChange;
    public float realWindStrength;
    public TextMeshPro windtext;
    void Start()
    {
        windStrength = Random.Range(0, maxStartStrength);
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 90f);
        realWindStrength = Mathf.Pow((windStrength*Mathf.Sqrt(5)), 2);
        windtext.SetText(((int)(realWindStrength/5*120)).ToString());
    }
    public void changeWind()
    {
        windStrength += Random.Range(-windStrengthChange, windStrengthChange);
        transform.rotation = Quaternion.Euler(0, transform.rotation.y + Random.Range(-45f, 45f), 90f);

        if (windStrength > 1)
        {
            windStrength = 1 + Random.Range(-0.05f, 0);
        }
        else if (windStrength < 0)
        {
            windStrength = -windStrength;
            transform.rotation = Quaternion.Euler(0, transform.rotation.y + 180f, 90f);
        }
        realWindStrength = Mathf.Pow((windStrength * Mathf.Sqrt(5)), 2);
        windtext.SetText(((int)(realWindStrength / 5 * 120)).ToString());
    }
}
