using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneButtons : MonoBehaviour
{

    public Animator animator;

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
        
        
        //SceneManager.LoadScene(3);
        //Initiate.Fade("EndlessMode", Color.black, 1.5f);

    }

    public void GridMode()
    {

        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("GridMode");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());


        //SceneManager.LoadScene(3);
        //Initiate.Fade("EndlessMode", Color.black, 1.5f);

    }

    public void BetaMap()
    {
        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("TestMechanical");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());

        //SceneManager.LoadScene(1);
        //Initiate.Fade("TestMechanical", Color.black, 1.5f);

    }

    public void Mainmenu()
    {

        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("Mainmenu");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());

        //SceneManager.LoadScene(0);
        //Initiate.Fade("Mainmenu", Color.black, 1.5f);
    }

    public void ExitGame()
    {   
        Application.Quit();
    }


}
