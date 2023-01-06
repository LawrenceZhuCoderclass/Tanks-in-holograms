using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player_1;
    public GameObject player_2;
    public GameObject StartText;
    public GameObject OptionsText;
    public GameObject PauseText;
    public GameObject Field;
    
    private PlayerController player_1_script;
    private PlayerController player_2_script;
    
    private GameState gameState;

    public playerText player_1_text;
    public playerText player_2_text;

    public bool mirrorControls;
    public bool pyramidUsed;
    public bool controllerUsed;
    
    private bool currentturn;

    public GameObject pyramidDisplay;
    public GameObject normalCamera;

    public GameObject pyramidCameraRotator;

    public WindScript windController;

    private AudioSource Select;

    public Camera mainCamera;

    public enum GameState
    {
        Start,
        Options,
        Playing,
        Paused,
        End
    }

    void Start()
    {
        player_1_script = player_1.GetComponent<PlayerController>();
        player_2_script = player_2.GetComponent<PlayerController>();
        Select = GetComponent<AudioSource>();
        currentturn = true;
        //true means player-1's turn, false means player_2's turn

    }

    void Update()
    {
        switch(gameState)
        {
            case GameState.Start:
                if (Input.GetKeyDown("space"))
                {
                    gameState = GameState.Playing;
                    StartText.SetActive(false);
                    Field.SetActive(true);
                    if (pyramidUsed)
                    {
                        pyramidDisplay.SetActive(true);
                        normalCamera.SetActive(false);
                        //pyramidCameraRotator.startGame();
                        pyramidCameraRotator.GetComponent<CameraRotator>().startGame();
                    }
                    else
                    {
                        normalCamera.SetActive(true);
                        pyramidDisplay.SetActive(false);
                        normalCamera.GetComponent<CameraRotator>().startGame();
                    }
                }
                else if (Input.GetKeyDown("o"))
                {
                    gameState = GameState.Options;
                    Select.Play();
                    StartText.SetActive(false);
                    OptionsText.SetActive(true);
                }
                else if (Input.GetKeyDown("e"))
                {
                    gameState = GameState.End;
                }
                break;
            case GameState.Options:
                if (Input.GetKeyDown("e"))
                {
                    Select.Play();
                    gameState = GameState.Start;
                    OptionsText.SetActive(false);
                    StartText.SetActive(true);
                }
                if (Input.GetKeyDown("h"))
                {
                    Select.Play();
                    mirrorControls = true;
                    mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 90);
                    Screen.SetResolution(720, 1334, true);
                    //controls to that of the holofil
                }
                else if (Input.GetKeyDown("p"))
                {
                    Select.Play();
                    mirrorControls = true;
                    pyramidUsed = true;
                    mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 0);
                    //controls to that of the pepper's cone
                }
                else if (Input.GetKeyDown("n"))
                {
                    Select.Play();
                    mirrorControls = false;
                    pyramidUsed = false;
                    controllerUsed = false;
                    mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y, 0);
                    //return to the normal controls
                }
                else if (Input.GetKeyDown("c"))
                {
                    Select.Play();
                    controllerUsed = true;
                }
                break;

            case GameState.Playing:
                Time.timeScale = 1;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Paused;
                    Field.SetActive(false);
                    PauseText.SetActive(true);
                }
                break;
            case GameState.Paused:
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Playing;
                    PauseText.SetActive(false);
                    Field.SetActive(true);
                }
                if(Input.GetKeyDown("e"))
                {
                    gameState = GameState.Start;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                break;
            case GameState.End:
                if (Input.GetKeyDown("r"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    Time.timeScale = 1;
                }
                break;

        }
    }
    public void Winner_Declaration(string name)
    {
        Debug.Log(name + "is the winner!!!");
        gameState = GameState.End;
        //Time.timeScale = 0;
        if (name == "player_1")
        {
            player_1_text.winner = true;
            player_2_text.dead = true;
        }
        else if (name == "player_2")
        {
            player_2_text.winner = true;
            player_1_text.dead = true;
        }
        player_1_script.enabled = false;
        player_2_script.enabled = false;
    }
    public void NextTurn()
    {
        windController.changeWind();
        if (currentturn == true)
        {
            currentturn = false;
            player_2_script.ChangePlayerState("Driving");
            player_2_text.shootMode = false;
            player_2_script.OwnTurn = true;
            player_2_text.ownTurn = true;
            //player_2_script.MoveHorizontal = 0.0f;
            //player_2_script.MoveVertical = 0.0f;
        }
        else if (currentturn == false)
        {
            currentturn = true;
            player_1_script.ChangePlayerState("Driving");
            player_1_text.shootMode = false;
            player_1_script.OwnTurn = true;
            player_1_text.ownTurn = true;
            //player_1_script.MoveHorizontal = 0.0f;
            //player_1_script.MoveVertical = 0.0f;
        }
    }
}
