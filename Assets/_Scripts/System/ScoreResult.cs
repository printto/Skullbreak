using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour {

    public Text ScoreText;
    public Text SecText;
    public Text CoinText;
    public int CoinMultiplyer = 5;
    int sec;
    int coins;
    float score;

    // Update is called once per frame
    private void Start() {
        sec = (int) ScoreManager.GetScore();
        coins = ScoreManager.GetCoin();
        score = sec + (coins * CoinMultiplyer);
        UpdateText();
    }

    void UpdateText()
    {
        if (ScoreText != null)
        {
            ScoreText.text = score + "";
        }
        if (CoinText != null)
        {
            CoinText.text = "Coins\n" + coins + " x " + CoinMultiplyer + " = " + (coins * CoinMultiplyer);
        }
        if (SecText != null)
        {
            SecText.text = "Time\n" + sec + " sec";
        }
    }

}
