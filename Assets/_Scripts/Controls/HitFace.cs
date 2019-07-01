using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitFace : MonoBehaviour {

    public Animator animator;

    string[] obstacleTags = { "Obstacle", "Dashable", "Monster"};

    private void Update()
    {
        // Make this playerface vanish when player is teleporting 
        GetComponent<MeshRenderer>().enabled = !Player.isTeleporting;
    }

    private void OnCollisionEnter(Collision collision)
    { 
        //Debug.Log("Obstacle: Hit something");
        if (collision.gameObject.tag.Equals("Dashable") && Player.isDashing && !Player.isTeleporting)
        {
            //Do nothing
        }
        if (collision.gameObject.tag.Equals("Monster") && GameMaster.lifePoint > 0 && !Player.isTeleporting)
        {
            transform.parent.gameObject.GetComponent<Player>().Slowdown();
            Debug.Log("Hit Monster : Player Face");
            //GameMaster.removeLife(1);
        }
        else if (Array.IndexOf(obstacleTags, collision.gameObject.tag) > -1 && GameMaster.lifePoint > 0 && !Player.isTeleporting)
        {
            Debug.Log("Hit obstacle");
            Bounce();
            GameMaster.removeLife(1);
            StartCoroutine(Stop());

        }
        else if (Array.IndexOf(obstacleTags, collision.gameObject.tag) > -1 && GameMaster.lifePoint <= 0 && !Player.isTeleporting)
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
        //SceneManager.LoadScene(2);
        //Initiate.Fade("DeadScene", Color.black, 6f);
        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("DeadScene");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
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
