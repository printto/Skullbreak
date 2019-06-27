using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public static bool SettingIsOpened = false;

    public GameObject pauseMenuUI;

    public GameObject settingUI;

    public Animator animator;

    public void openPauseMenu()
    {
        if(GameIsPaused && SettingIsOpened)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("Mainmenu");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
        //SceneManager.LoadScene(0);
        pauseMenuUI.SetActive(false); 
        GameIsPaused = false;
    }

    public void Exit()
    {   
        
        Application.Quit();
        //pauseMenuUI.SetActive(false);
       // Time.timeScale = 1f;
        //GameIsPaused = false;
    }

    public void setting()
    {
        SettingIsOpened = true;
        Debug.Log("Setting here");
        settingUI.gameObject.SetActive(true);
        pauseMenuUI.gameObject.SetActive(false);
    }


    public void Back()
    {
        SettingIsOpened = false;
        settingUI.gameObject.SetActive(false);
        pauseMenuUI.gameObject.SetActive(true);
    }
}
