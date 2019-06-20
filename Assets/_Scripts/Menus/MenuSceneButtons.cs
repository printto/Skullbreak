using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneButtons : MonoBehaviour {

   

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public void StageMode()
        {
            SceneManager.LoadScene(4);
        }

        public void EndlessMode()
        {
            SceneManager.LoadScene(3);
        }

        public void BetaMap()
        {
            SceneManager.LoadScene(1);
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
