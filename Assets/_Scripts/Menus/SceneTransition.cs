using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition {
    
    public static Animator dragTransitionAnim;
    private static string sceneName;


    public static void setAnimator(Animator transitionAnim)
    {
        dragTransitionAnim = transitionAnim;
    }

    public static void setScene(string name)
    {
       sceneName = name;
    }

    public static string getScene()
    {
        return sceneName;
    }

    public static IEnumerator LoadScene()
    {

        Debug.Log("fadeout");
        dragTransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
