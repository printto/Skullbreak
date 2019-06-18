using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitFace : MonoBehaviour {

    string[] obstacleTags = { "Obstacle", "Dashable" };

      private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Obstacle: Hit something");

        if (collision.gameObject.tag.Equals("Dashable") && Player.isDashing)
        {

        }
        else if (Array.IndexOf(obstacleTags, collision.gameObject.tag) > -1 && GameMaster.lifePoint > 0)
        {
           
            Bounce();
            Invoke("Bounce", 0.5f);
            GameMaster.removeLife(1);
        }
        else if (Array.IndexOf(obstacleTags, collision.gameObject.tag) > -1 && GameMaster.lifePoint == 0)
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
}
