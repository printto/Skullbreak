using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour {

    public Text ScoreText;
    public Text SecText;
    public Text CoinText;
    public Text HighscoreText;
    public Text HighscoreTextTitle;

    public int CoinMultiplyer = 1;
    int sec;
    int coins;
    float score;

    // Update is called once per frame
    private void Start() {
        //sec = (int) ScoreManager.GetScore();
        sec = 0;
        coins = ScoreManager.GetCoin();
        score = sec + (coins * CoinMultiplyer);
        SaveManager.Load();
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
            //CoinText.text = "Coins\n" + coins + " x " + CoinMultiplyer + " = " + (coins * CoinMultiplyer);
            CoinText.text = "LIGHTS\n" + (coins * CoinMultiplyer);
        }
        if (SecText != null)
        {
            SecText.text = "Time Left\n" + sec + " sec";
        }
        if (HighscoreText != null)
        {
            /*
            if (HighscoreManager.UpdateHighscore(score, LoadPrevScene.getLastLevel()))
            {
                if(HighscoreTextTitle != null)
                {
                    HighscoreTextTitle.text = "";
                }
                HighscoreText.fontSize = 30;
                HighscoreText.text = "New Highscore!";
            }
            else
            {
                HighscoreText.text = "" + ((int) HighscoreManager.GetHighscore(LoadPrevScene.getLastLevel()));
            }
            */
            if (HighscoreManager.UpdateHighscore(score, LoadPrevScene.getLastLevel()))
            {
                HighscoreText.text = "New\nHighscore!";
            }
            else
            {
                HighscoreText.text = "Highscore\n" + ((int)HighscoreManager.GetHighscore(LoadPrevScene.getLastLevel()));
            }
        }
    }

}
