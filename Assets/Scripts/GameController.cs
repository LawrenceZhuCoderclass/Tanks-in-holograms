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

    //public Text_Script Text_Script;
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

    Text_Script[] scripts; 
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
        scripts = Resources.FindObjectsOfTypeAll(typeof(Text_Script)) as Text_Script[];
        Select = GetComponent<AudioSource>();
        currentturn = true;
        
        /*for (int i = 0; i < scripts.Length; i++)
        {
            Debug.Log(scripts[i]);
        }*/
        //true means player-1's turn, false means player_2's turn

    }

    void Update()
    {
        switch(gameState)
        {
            case GameState.Start:
                if (Input.GetKeyDown("space"))
                {
                    //check what projection is used and if controllers are conected and change the controls accordingly
                    gameState = GameState.Playing;
                    StartText.SetActive(false);
                    Field.SetActive(true);
                    if (pyramidUsed)
                    {
                        for (int i = 0; i < scripts.Length; i++)
                        {
                            scripts[i].normalstopper = 1;
                        }
                        pyramidDisplay.SetActive(true);
                        normalCamera.SetActive(false);
                        pyramidCameraRotator.GetComponent<CameraRotator>().startGame();
                    }
                    else if (mirrorControls)
                    {/*
                        for (int i = 0; i < scripts.Length; i++)
                        {
                            scripts[i].Xrotation = -30.0f;
                            scripts[i].Yrotation = 100.0f;
                            scripts[i].normalstopper = 0;
                        }*/
                        normalCamera.SetActive(true);
                        pyramidDisplay.SetActive(false);
                        normalCamera.transform.position = new Vector3(normalCamera.transform.position.x, normalCamera.transform.position.y - 5, normalCamera.transform.position.z);
                    }
                    else
                    {
                        /*for (int i = 0; i < scripts.Length; i++)
                        {
                            scripts[i].Xrotation = 30.0f;
                            scripts[i].Yrotation = 0.0f;
                            scripts[i].normalstopper = 0;
                        }*/
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
                    //This is called when controllers are used
                }
                break;

            case GameState.Playing:
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Paused;
                    if (mirrorControls && !pyramidUsed)
                    {
                        for (int i = 0; i < scripts.Length; i++)
                        {
                            scripts[i].SetToHolofil();
                        }
                    }
                    else if (!pyramidUsed)
                    {
                        for (int i = 0; i < scripts.Length; i++)
                        {
                            scripts[i].SetToNormal();
                        }
                    }
                    Field.SetActive(false);
                    PauseText.SetActive(true);
                }
                break;
            case GameState.Paused:
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Playing;
                    Time.timeScale = 1;
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
        player_1_text.gameOver = true;
        player_2_text.gameOver = true;
        if (name == "player_1")
        {
            player_1_text.WinnerText();
            player_2_text.LoserText();
        }
        else if (name == "player_2")
        {
            player_1_text.LoserText();
            player_2_text.WinnerText();
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
        }
        else if (currentturn == false)
        {
            currentturn = true;
            player_1_script.ChangePlayerState("Driving");
            player_1_text.shootMode = false;
            player_1_script.OwnTurn = true;
            player_1_text.ownTurn = true;
        }
    }

}
