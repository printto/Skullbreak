using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorForComponent : MonoBehaviour {


    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("Hit something");
        if (collision.gameObject.tag.Equals("Player"))
        {
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
