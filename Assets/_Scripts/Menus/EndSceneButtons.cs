using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneButtons : MonoBehaviour {

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void RetryGame()
    {
        // SceneManager.LoadScene(1);
        LoadPrevScene.changeToPreviousLvl();
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
