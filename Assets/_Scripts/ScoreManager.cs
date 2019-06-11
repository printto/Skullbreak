using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private static float  scorePoints = 0;

    public Text scoreText;

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

    private void Start()
    {
        
    }

    private void Update()
    {
        scorePoints += Time.deltaTime;
        scoreText.text = ((int)scorePoints).ToString();
    }

}
