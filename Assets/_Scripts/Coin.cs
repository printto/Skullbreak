using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
        ScoreManager.AddScore(1);
        Debug.Log(ScoreManager.GetScore());
    }
}
