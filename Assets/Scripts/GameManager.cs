using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages main game loop
public class GameManager : MonoBehaviour
{
    void Awake()
    {
        //Prevent duplicates, and set to persist between levels
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");
        if (objs.Length > 1) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        //Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (SceneManager.GetActiveScene().name == "Galaxy") LevelTracker.completedLevels = 26;
    }

    void Update()
    {
        //Destroy self once the game has left the main game loop scene
        if (SceneManager.GetActiveScene().name == "title") Destroy(this.gameObject);

        //Press ESC to title
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("title");
    }

    //Coroutine to start the next level while displaying results for the completed one
    //Can be called anywhere
    public static IEnumerator CompleteLevel(int delay)
    {
        Timer.PauseTimer(); //Stop timer
        Time.timeScale = 0f; //Freeze time
        yield return new WaitForSecondsRealtime(delay); //Wait a given time in seconds
        Time.timeScale = 1f; //Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Load next level
        LevelTracker.AddCompletedLevel(); //Add one to the count of completed levels
        Timer.StartTimer(); //Resume timer
    }

    //Corountine to stop the game and transition back to the title screen
    //Can be called anywhere
    public static IEnumerator CompleteGame(int delay)
    {
        GameObject.FindGameObjectWithTag("GameplayHUD").GetComponent<CanvasGroup>().alpha = 0; //Hide the gameplay HUD
        GameObject.FindGameObjectWithTag("FinishedHUD").GetComponent<CanvasGroup>().alpha = 1f; //Show the "Time's Up" HUD
        Timer.PauseTimer(); //Stop timer
        Time.timeScale = 0f; //Freeze time
        yield return new WaitForSecondsRealtime(delay); //Wait a given time in seconds
        SceneManager.LoadScene("title"); //Return the title screen
    }
}
