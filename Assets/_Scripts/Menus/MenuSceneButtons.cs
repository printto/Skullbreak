using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneButtons : MonoBehaviour
{

    public Animator animator;

    public GameObject CreditUI;

    public GameObject MenuUI;

    public Text title;

    private bool creditIsOpened;

    private void Start()
    {
        SceneTransition.setAnimator(animator);

        if (!SaveManager.Load())
        {
            SceneTransition.setScene("TutorialLevel");
            SceneTransition.getScene();
            StartCoroutine(SceneTransition.LoadScene());
        }
        Cursor.lockState = CursorLockMode.None;
    }


    public void GridMode()
    {

        SceneTransition.setScene("GridStageMode");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());


    }

    /*
    public void ChooseLevelFunc()
    {   

        ChooseLevelIsOpened = true;
        title.gameObject.SetActive(false);
        Debug.Log("Setting here");
        ChooseLevel.gameObject.SetActive(true);
        MenuUI.gameObject.SetActive(false);
    }
    */

    public void CreditOpen()
    {

        creditIsOpened = true;
        title.gameObject.SetActive(false);
        Debug.Log("Credit here");
        CreditUI.gameObject.SetActive(true);
        MenuUI.gameObject.SetActive(false);
    }

    public void Back()
    {
        title.gameObject.SetActive(true);
        creditIsOpened = false;
        //ChooseLevelIsOpened = false;
        //ChooseLevel.gameObject.SetActive(false);
        CreditUI.gameObject.SetActive(false);
        MenuUI.gameObject.SetActive(true);
    }

   

    public void TutorialMode()
    {
        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("TutorialLevel");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
    }


    public void Level1()
    {
        SceneTransition.setScene("GridMode");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
    }

    public void Mainmenu()
    {
        SceneTransition.setScene("Mainmenu");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
    }

    public void ExitGame()
    {   
        Application.Quit();
    }


}
