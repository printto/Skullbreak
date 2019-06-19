using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadPrevScene  {

  
        private static string lastLevel;

        public static void setLastLevel(string level)
        {
            lastLevel = level;
        }

        public static string getLastLevel()
        {
            Debug.Log(lastLevel.ToString());
            return lastLevel;
        }

        public static void changeToPreviousLvl()
        {
            SceneManager.LoadScene(lastLevel);
        }
}
