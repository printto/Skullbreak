using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneButtons : MonoBehaviour
{

    public Animator animator;

    public GameObject ChooseLevel;

    public GameObject MenuUI;

    public Text title;

    private bool ChooseLevelIsOpened;

    private void Start()
    {
        if (!SaveManager.Load())
        {
            SceneManager.LoadScene(5);
        }
        Cursor.lockState = CursorLockMode.None;
    }

    public void StageMode()
    {
        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("StageMode");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
        //SceneManager.LoadScene(4);
        //Initiate.Fade("StageMode",Color.black,1.5f);
    }

    public void EndlessMode()
    {

        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("EndlessMode");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
   
    }

    public void GridMode()
    {

        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("GridMode");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());


    }

    public void BetaMap()
    {
        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("TestMechanical");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());


    }

    public void ChooseLevelFunc()
    {   

        ChooseLevelIsOpened = true;
        title.gameObject.SetActive(false);
        Debug.Log("Setting here");
        ChooseLevel.gameObject.SetActive(true);
        MenuUI.gameObject.SetActive(false);
    }


    public void Back()
    {
        title.gameObject.SetActive(true);
        ChooseLevelIsOpened = false;
        ChooseLevel.gameObject.SetActive(false);
        MenuUI.gameObject.SetActive(true);
    }

    public void Mainmenu()
    {
        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("Mainmenu");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
    }

    public void ExitGame()
    {   
        Application.Quit();
    }


}
