using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player_1;
    public GameObject player_2;

    private PlayerController player_1_script;
    private PlayerController player_2_script;

    private bool currentturn;
    void Start()
    {
        player_1_script = player_1.GetComponent<PlayerController>();
        player_2_script = player_2.GetComponent<PlayerController>(); 
        currentturn = true;
        //true means player-1's turn, false means player_2's turn
    }
    
    public void NextTurn()
    {
        if (currentturn == true)
        {
            currentturn = false;
            player_1_script.OwnTurn = false;
            player_2_script.OwnTurn = true;
        }
        else if (currentturn == false)
        {
            currentturn = true;
            player_1_script.OwnTurn = true;
            player_2_script.OwnTurn = false;
        }
    }
}
