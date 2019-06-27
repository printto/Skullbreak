﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadnewScene : MonoBehaviour {

    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            SceneTransition.setAnimator(animator);
            SceneTransition.setScene("Mainmenu");
            SceneTransition.getScene();
            StartCoroutine(SceneTransition.LoadScene());
        }       
    }

}
