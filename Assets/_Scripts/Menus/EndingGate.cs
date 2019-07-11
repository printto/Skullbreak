using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingGate : MonoBehaviour {

    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("PlayerFace"))
        {
            SceneTransition.setAnimator(animator);
            SceneTransition.setScene("EndingScene");
            StartCoroutine(SceneTransition.LoadScene());
        }
    }
}
