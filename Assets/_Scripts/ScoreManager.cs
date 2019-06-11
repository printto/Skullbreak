using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private static int scorePoints = 0;

    public static int GetScore()
    {
        return scorePoints;
    }

    public static void SetScore(int score)
    {
        scorePoints = score;
    }

    public static void AddScore(int score)
    {
        scorePoints += score;
    }

}
