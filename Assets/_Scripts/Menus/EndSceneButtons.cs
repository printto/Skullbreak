using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneButtons : MonoBehaviour {

    public Animator animator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneTransition.setAnimator(animator);

    }

    public void RetryGame()
    {
        SceneTransition.setScene(LoadPrevScene.getLastLevel());
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
        //LoadPrevScene.changeToPreviousLvl();
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
