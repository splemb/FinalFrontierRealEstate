using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tracks the number of completed levels and displays current level to player
public class LevelTracker : MonoBehaviour
{
    //Components
    public TMPro.TextMeshProUGUI levelText;
    public static int completedLevels = 0;

    void Start()
    {
        ResetLevelCount(); //Reset completed levels count from last session
    }

    void Update()
    {
        //Update player's level count
        levelText.text = "Client " + (completedLevels + 1).ToString("D2");
    }

    public static void AddCompletedLevel()
    {
        completedLevels++; //Add one to the number of completed levels
    }

    public static void ResetLevelCount()
    {
        completedLevels = 0; //Reset the number of completed levels
    }
}
