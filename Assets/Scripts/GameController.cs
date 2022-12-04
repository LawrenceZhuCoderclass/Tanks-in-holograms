using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player_1;
    public GameObject player_2;

    private PlayerController player_1_script;
    private PlayerController player_2_script;
    private GameState gameState;

    public bool mirrorControls;
    public bool pyramidDisplay;
    public bool controllerUsed;
    
    private bool currentturn;

    public enum GameState
    {
        //Start,
        Playing,
        //Paused,
        End
    }

    void Start()
    {
        player_1_script = player_1.GetComponent<PlayerController>();
        player_2_script = player_2.GetComponent<PlayerController>(); 
        currentturn = true;
        //true means player-1's turn, false means player_2's turn
    }

    void Update()
    {
        switch(gameState)
        {
            case GameState.Playing:
                // Here comes the code where you check if something is clicked for the pause mode
                break;
            case GameState.End:
                if (Input.GetKeyDown("r"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    Debug.Log(Time.timeScale);
                    Time.timeScale = 1;
                }
                break;

        }
    }
    public void Winner_Declaration(string name)
    {
        Debug.Log(name + "is the winner!!!");
        gameState = GameState.End;
        Time.timeScale = 0;
        player_1_script.enabled = false;
        player_2_script.enabled = false;
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
