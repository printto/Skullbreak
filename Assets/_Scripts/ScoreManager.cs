using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager
{
    private static ScoreManager scoreManager;

    private static float scorePoints = 0;

    public Text scoreText;

    private bool isDead = false;

    public DeathMenu deathMenu;

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
        if (isDead)
            return;

        AddScore(Time.deltaTime);
        scoreText.text = ((int)scorePoints).ToString();
    }

    public void OnDeath()
    {
        isDead = true;
        deathMenu.ToggleEndMenu(scorePoints);
    }

}
