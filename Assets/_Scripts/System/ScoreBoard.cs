using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    public Text ScoreText;
    public Text CoinText;

    // Update is called once per frame
    void Update () {
        if(ScoreText != null)
        {
            ScoreText.text = ((int)ScoreManager.GetScore()).ToString();
        }
        if (CoinText != null)
        {
            CoinText.text = "Coins: " + ((int)ScoreManager.GetCoin()).ToString();
        }
    }
}
