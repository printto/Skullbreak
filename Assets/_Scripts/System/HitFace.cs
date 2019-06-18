using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitFace : MonoBehaviour {

      private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Obstacle: Hit something");

        if (collision.gameObject.tag.Equals("Obstacle") && GameMaster.lifePoint > 0)
        {

            Bounce();
            GameMaster.removeLife(1);
            StartCoroutine(Stop());

        }
        else if ((collision.gameObject.tag.Equals("Obstacle") && GameMaster.lifePoint == 0))
        {
            DeadScene();
        }
    }

    private void Bounce()
    { 
        transform.parent.gameObject.GetComponent<Player>().MoveSpeed = -transform.parent.gameObject.GetComponent<Player>().MoveSpeed;
    }

 

    private void DeadScene()
    {
        SceneManager.LoadScene(2);
    }

   

    //not yet implemented
    //For when crashing, it will pause for a seconds then move forward.
    IEnumerator Stop()
    {
        float forward = -transform.parent.gameObject.GetComponent<Player>().MoveSpeed;
        yield return new WaitForSeconds(0.75f);
        transform.parent.gameObject.GetComponent<Player>().MoveSpeed = 0;
        yield return new WaitForSeconds(2);
        transform.parent.gameObject.GetComponent<Player>().MoveSpeed = forward;
    }
}
