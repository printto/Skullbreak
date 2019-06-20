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
            Debug.Log("Enter the gate");
            if (anim != null)
            {
                Debug.Log("playing map animation");
                anim.SetTrigger("Player");
            }
        }
    }

    // Spawn new map
    void SpawnMap()
    {

        if (!transform.parent.gameObject.GetComponent<MapGenerator>().createdNext)
        {
            transform.parent.gameObject.GetComponent<MapGenerator>().SpawnMap();
        }

    }
}
