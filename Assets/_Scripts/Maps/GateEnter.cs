using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateEnter : MonoBehaviour {

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (anim != null)
            {
                anim.SetTrigger("Player");
            }
        }
    }
}
