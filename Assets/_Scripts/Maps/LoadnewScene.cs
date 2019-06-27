using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadnewScene : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            Debug.Log(SceneManager.GetSceneAt(0).ToString());
            SceneManager.LoadScene(0);
        }       
    }

}
