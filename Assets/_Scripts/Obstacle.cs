using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour {

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Obstacle: Hit something");
        /*
         * //Currently unused
         * 
        if (collision.gameObject.tag.Equals("PlayerFace"))
        {
            //DeadScene();
        }
        */
    }

    private void DeadScene()
    {
        SceneManager.LoadScene(2);
    }

}
