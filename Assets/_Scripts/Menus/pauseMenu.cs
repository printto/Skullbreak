using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public static bool SettingIsOpened = false;

    public GameObject pauseMenuUI;

    public GameObject settingUI;

    public Animator animator;

    public Text countDownUnpauseText;

    private float saveMoveSpeed;

    private void Start()
    {
        SceneTransition.setAnimator(animator);
    }

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
        countDownUnpauseText.enabled = true;
        StartCoroutine(countDown());
        GameIsPaused = false;
    }

    public void Pause()
    {
        GameObject.FindGameObjectWithTag("PlayerAnim").GetComponent<Animator>().enabled = false;
        Timer.canCount = false;
        saveMoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNew>().MoveSpeed;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNew>().MoveSpeed = 0;
        GameIsPaused = true;
    }

    public void Menu()
    {   

        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNew>().MoveSpeed = 0;
        SceneTransition.setScene("Mainmenu");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
        pauseMenuUI.SetActive(false); 
        GameIsPaused = false;
    }

    public void Exit()
    {   
        
        Application.Quit();

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

    IEnumerator countDown()
    {
        Time.timeScale = 1f;
        countDownUnpauseText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownUnpauseText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownUnpauseText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownUnpauseText.text = "";
        Timer.canCount = true;
        GameObject.FindGameObjectWithTag("PlayerAnim").GetComponent<Animator>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNew>().MoveSpeed = saveMoveSpeed;
    }
}
