using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int ScoreToAdd = 1;

    /*
    public void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag.Equals("Player"))
        {
            ScoreManager.AddCoin(ScoreToAdd);
            Destroy(gameObject);
        }

    }
    */

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerNew playerScript = other.GetComponent<PlayerNew>();
            playerScript.playSound(playerScript.soundEffect.CoinSounds);
            ScoreManager.AddCoin(ScoreToAdd);
            Destroy(gameObject);
        }
    }
}
