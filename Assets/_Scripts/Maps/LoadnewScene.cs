using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadnewScene : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.Equals("Player"))
        {
            SceneManager.LoadScene(0);
        }       
    }

}
