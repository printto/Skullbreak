using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static float scorePoints = 0;

    public Text ScoreText;

    public static float GetScore()
    {
        return scorePoints;
    }

    public static void SetScore(float score)
    {
        scorePoints = score;
    }

    public static void AddScore(float score)
    {
        scorePoints += score;
    }

    public void Update()
    {
        ScoreText.text = "Score: " + ((int)scorePoints).ToString();
    }

}
