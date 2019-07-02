using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fallDamage : MonoBehaviour
{
    
    private static float x = 0;
    private static float y = 0;
    private static float z = 0;

    public Animator animator;


    public static void setSavePoint(float x, float y, float z)
    {
        fallDamage.x = x;
        fallDamage.y = y;
        fallDamage.z = z;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player") && GameMaster.lifePoint > 0)
        {
            Debug.Log("Respawned");
            //collision.gameObject.transform.SetPositionAndRotation(new Vector3(x,y,z) , collision.gameObject.transform.rotation);
            checkPoint.respawnPlayerAtCheckPoint();
            GameMaster.removeLife(1);
        }
        else if (collision.gameObject.tag.Equals("Player") && GameMaster.lifePoint <= 0)
        {

            SceneTransition.setAnimator(animator);
            SceneTransition.setScene("DeadScene");
            SceneTransition.getScene();
            StartCoroutine(SceneTransition.LoadScene());
        }
        
    }
}
