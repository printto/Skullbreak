using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    
    private static float scorePoints = 0;
    private static int coins = 0;

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

    public static int GetCoin()
    {
        return coins;
    }

    public static void SetCoin(int coin)
    {
        coins = coin;
    }

    public static void AddCoin(int coin)
    {
        coins += coin;
    }

}
