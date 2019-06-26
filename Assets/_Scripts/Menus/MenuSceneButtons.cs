using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneButtons : MonoBehaviour
{
 

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        
    }

    public void StageMode()
    {
        //SceneManager.LoadScene(4);
        Initiate.Fade("StageMode",Color.black,1.5f);
    }

    public void EndlessMode()
    {
        //SceneManager.LoadScene(3);
        Initiate.Fade("EndlessMode", Color.black, 1.5f);

    }

    public void BetaMap()
    {
        //SceneManager.LoadScene(1);
        Initiate.Fade("TestMechanical", Color.black, 1.5f);

    }

    public void Mainmenu()
    {

        //SceneManager.LoadScene(0);
        Initiate.Fade("Mainmenu", Color.black, 1.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
