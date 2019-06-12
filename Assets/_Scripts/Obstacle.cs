using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour {

    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("Obstacle: Hit something");
        if (collision.gameObject.tag.Equals("PlayerFace"))
        {
            DeadScene();
            Debug.Log("Obstacle: Hit Player face");
        }

    }

    private void DeadScene()
    {
        SceneManager.LoadScene(1);
    }

}
