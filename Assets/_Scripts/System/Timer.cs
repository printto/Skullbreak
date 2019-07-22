using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;
    public Animator animator;

    private float timer;
    public static bool canCount = true;
    //private bool doOnce = false;

     void Start()
    {
        timer = mainTimer;
    }

    void Update()
    {    
        ScoreManager.AddScore(Time.deltaTime);
        Debug.Log(ScoreManager.GetScore());
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
}
