using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitFace : MonoBehaviour {

      private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Obstacle: Hit something");

        if (collision.gameObject.tag.Equals("Obstacle") && GameMaster.lifePoint >= 0)
        {
            GameMaster.removeLife(1);
        }
        else if ((collision.gameObject.tag.Equals("Obstacle") && GameMaster.lifePoint < 0))
        {
            DeadScene();
        }
    }

    private void DeadScene()
    {
        SceneManager.LoadScene(2);
    }
}
