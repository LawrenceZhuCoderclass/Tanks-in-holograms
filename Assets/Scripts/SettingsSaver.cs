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
    private bool createOnce = false;
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
        /*Debug.Log(HolofilUsed);
        Debug.Log(PyramidUsed);
        Debug.Log(ControllerUsed);*/
        if (HolofilUsed)
        {
            Debug.Log("This is the truth of holofil");
            Debug.Log(HolofilUsed);
            gameController.ChangeToHolofil();
        }
        else if (PyramidUsed)
        {
            Debug.Log("This is the truth of the pyramid");
            Debug.Log(PyramidUsed);
            gameController.ChangeToPyramid();
        }
        if (ControllerUsed)
        {
            Debug.Log("This is truth of the controller");
            Debug.Log(ControllerUsed);
            gameController.ChangeToController();
        }
    }
}
