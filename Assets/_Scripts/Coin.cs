using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int ScoreToAdd = 5;

    //Unused at the moment
    public int WalletValue = 1;

    public void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag.Equals("Player"))
        {
            ScoreManager.AddScore(ScoreToAdd);
            Destroy(gameObject);
        }

    }
}
