using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitFace : MonoBehaviour {

    string[] obstacleTags = { "Obstacle", "Dashable"};

    public Animator animator;


    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
         * if (collision.gameObject.tag.Equals("Dashable") && Player.isDashing && !Player.isTeleporting)
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
         */
        if (Array.IndexOf(obstacleTags, collision.gameObject.tag) > -1 && GameMaster.lifePoint > 0)
        {
            Bounce();
            GameMaster.removeLife(1);
            StartCoroutine(Stop());
        }
        else if (Array.IndexOf(obstacleTags, collision.gameObject.tag) > -1 && GameMaster.lifePoint <= 0)
        {
            Bounce();
            DeadScene();
        }
    }

    private void Bounce()
    {
        transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed = -transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed;
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
        float forward = -transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed;
        yield return new WaitForSeconds(0.75f);
        transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed = 0;
        yield return new WaitForSeconds(0.75f);
        transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed = forward;
    }
}
