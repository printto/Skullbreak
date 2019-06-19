using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateEnter : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Enter the gate");
            SpawnMap();
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
