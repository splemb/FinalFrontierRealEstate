using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Handles title screen
public class TitleController : MonoBehaviour
{
    //Components
    public TMPro.TextMeshProUGUI highScoreText;

    void Start()
    {
        //Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }

    void Update()
    {
        //Return to full speed from being paused at the end of a game
        //Keeps triggering because it doesn't work if in start
        Time.timeScale = 1f;

        //Load high score
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");

        //Press left mouse to start new game
        if (Input.GetMouseButtonDown(0)) { SceneManager.LoadScene("SolarSystem"); }
        //Press escape to exit
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        //Press F12 to reset high score
        if (Input.GetKeyDown(KeyCode.F12)) PlayerPrefs.SetInt("HighScore",0);
    }
}
