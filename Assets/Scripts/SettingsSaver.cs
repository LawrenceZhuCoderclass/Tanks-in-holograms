using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSaver : MonoBehaviour
{
    public static bool HolofilUsed = false;
    public static bool PyramidUsed = false;
    public static bool ControllerUsed = false;

    private GameObject Game_Controller;
    private GameController gameController;

    private void Start()
    {        
        int NumSavers = GameObject.FindGameObjectsWithTag("SettingsSaver").Length;
        if (NumSavers == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
        Game_Controller = GameObject.FindWithTag("GameController");
        gameController = Game_Controller.GetComponent<GameController>();
        RetainSettings();
    }
    public void RetainSettings()
    {
        //Calls the functions in the GameController because the previous bools where set to true.
        if (HolofilUsed)
        {
            gameController.ChangeToHolofil();
        }
        else if (PyramidUsed)
        {
            gameController.ChangeToPyramid();
        }
        if (ControllerUsed)
        {
            gameController.ChangeToController();
        }
    }
}
