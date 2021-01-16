using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps track of score per session and saves new high scores
public class Score : MonoBehaviour
{
    //Components
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI highScoreText;

    public static int score;

    void Start()
    {
        ResetScore(); //Reset score from last session
    }

    void Update()
    {
        //Update score and high score HUD text
        scoreText.text = "Score: " + (score).ToString();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    public static void AddScore(int addedScore)
    {
        score += addedScore; //Add to score

        //If the current score is higher than the saved high score, update that value
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public static void ResetScore()
    {
        score = 0;
    }
}
