using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    private static ScoreManager scoreManager;
    private int scorePoints = 0;

    private ScoreManager()
    {

    }

    public static ScoreManager getManger()
    {
        if(scoreManager != null)
        {
            return scoreManager;
        } else
        {
            scoreManager = new ScoreManager();
            return scoreManager;
        }
    }

    public int GetScore()
    {
        return scorePoints;
    }

    public void SetScore(int score)
    {
        scorePoints = score;
    }

    public void AddScore(int score)
    {
        scorePoints += score;
    }

}
