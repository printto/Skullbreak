using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int ScoreToAdd = 1;

    public void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag.Equals("Player"))
        {
            ScoreManager.AddCoin(ScoreToAdd);
            Destroy(gameObject);
        }

    }
}
