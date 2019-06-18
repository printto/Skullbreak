using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public static float lifePoint = 3;
    public Text lifeText;


    public static float GetScore()
    {
        return lifePoint;
    }

    public static void SetLife(float score)
    {
        lifePoint = score;
    }

    public static void AddLife(float score)
    {
        lifePoint += score;
    }

    public static void removeLife(float score)
    {
        lifePoint -= score;
    }

    public void Update()
    {
        lifeText.text = "LIVES: "+((int)lifePoint).ToString();
    }
}
