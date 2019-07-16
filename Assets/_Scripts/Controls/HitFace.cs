using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitFace : MonoBehaviour {

    string[] obstacleTags = { "Obstacle", "Dashable"};

    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Player hit " + other.gameObject.name);
        if (Array.IndexOf(obstacleTags, other.gameObject.tag) > -1 )
        {
            Bounce();
            StartCoroutine(blinking());
            StartCoroutine(Stop());
        }

    }

    private void Bounce()
    {
        transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed = -transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed;
    }

 

    private void DeadScene()
    {
        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("DeadScene");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
    }


    IEnumerator Stop()
    {
        float forward = -transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed;
        yield return new WaitForSeconds(0.5f);
        transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed = 0;
        yield return new WaitForSeconds(2f);
        transform.parent.gameObject.GetComponent<PlayerNew>().MoveSpeed = forward ;


    }

    IEnumerator blinking()
    {
        GameObject skullo = GameObject.Find("again-im-testing-rigging");
        skullo.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        skullo.SetActive(true);


    }
}
