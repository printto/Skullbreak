using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        /*
         * //Currently unused
         * 
        if (collision.gameObject.tag.Equals("PlayerFace"))
        {
            //DeadScene();
        }
        */
    }

}
