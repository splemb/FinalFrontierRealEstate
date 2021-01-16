using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //Components
    public TMPro.TextMeshProUGUI timerText;

    //Timer variables
    static float startTime;
    static float lastTimeValue;
    public static bool timerIsRunning = false;
    public int maxTimeSeconds;

    public bool enable;

    void Start()
    {
        ResetTimer(); //Reset timer from last session
    }

    void Update()
    {
        if (timerIsRunning) UpdateTimer(); //Run timer
        if (enable)
        {
            if (Time.realtimeSinceStartup - startTime >= maxTimeSeconds) { StartCoroutine(GameManager.CompleteGame(5)); } //If the timer has reached the limit, end the game
        }
    }

    //Reset timer to zero
    public static void ResetTimer()
    {
        lastTimeValue = 0;
        startTime = Time.realtimeSinceStartup;
        timerIsRunning = true;
    }

    //Pause timer
    public static void PauseTimer()
    {
        lastTimeValue = Time.realtimeSinceStartup - startTime;
        timerIsRunning = false;
    }
    
    //Resume timer from being paused
    public static void StartTimer()
    {
        startTime = Time.realtimeSinceStartup - lastTimeValue;
        timerIsRunning = true;
    }

    //Convert time elapsed in seconds to a readable format for the HUD
    //Also triggers win music and rounds off timer once limit is hit
    void UpdateTimer()
    {
        int seconds = Mathf.FloorToInt(Time.realtimeSinceStartup - startTime);
        int minutes = Mathf.FloorToInt(seconds / 60);
        int milliseconds = Mathf.FloorToInt((Time.realtimeSinceStartup - startTime - seconds) * 100);
        timerText.text = minutes.ToString("D2") + ":" + (seconds - (minutes * 60)).ToString("D2") + ":" + milliseconds.ToString("D2");

        if (Time.realtimeSinceStartup - startTime > maxTimeSeconds && enable) { 
            timerText.text = "02:30:00";
            GetComponent<MusicController>().WinMusic();
        }
    }
}
