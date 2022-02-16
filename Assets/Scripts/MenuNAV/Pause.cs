using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    //[SerializeField] private CursorManager.CursorType cursorType;

    private Player player;

    public CrossHairCursor crosshair;
    //public static bool GameisPaused = false;


    public GameObject OptionMenuUI;
    public GameObject PauseMenuUI;
    public GameObject PlayerUI;

    public GameObject[] uiToTurnOffOrOn;

    public bool isButtonObject = false;
    //public GameObject StatusIndicator;

    /*
    public void Start()
    {
        CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Target);
        Debug.Log("Setting Cursor To Target START");
    }
    */


    public void resume() 
    {
        Time.timeScale = 1f;
        if(PlayerUI != null)
            PlayerUI.SetActive(true);
        OptionMenuUI.SetActive(false);
        PauseMenuUI.SetActive(false);

        for (int i = 0; i < uiToTurnOffOrOn.Length; i++)
        {
            uiToTurnOffOrOn[i].SetActive(false);
        }


        OptionSettings.GameisPaused = false;
        //Cursor.visible = false;

        //CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Target);
        //Debug.Log("Setting Cursor To Target RESUME");
    }

    public void button()
    {
        GameObject Temp = GameObject.Find("GameMusicandPause");
        Pause pauseButton = Temp.GetComponent<Pause>();
        pauseButton.resume();
    }


    void pause() 
    {
        //PauseOption.SetActive(true);
        PauseMenuUI.SetActive(true);
        PlayerUI.SetActive(false);
        OptionSettings.GameisPaused = true;
        Time.timeScale = 0f;

        //CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);
        //Debug.Log("Setting Cursor To ARROW PAUSE");
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isButtonObject == false)
        {
            if (player.Utilities.EscapeButtonPressed && GlobalPlayerVariables.GameOver == false && GlobalPlayerVariables.inActiveUI == false)
            {
                if (OptionSettings.GameisPaused == true)
                    resume();
                else
                    pause();

            }
        }

    }
}
