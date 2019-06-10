using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject NextMap;
    public GameObject RoadSpawnPoint;
    public GameObject ThisMap;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
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
        Vector3 localOffset = new Vector3(0, 0, ThisMap.transform.localPosition.z); //offset for first road block
        Vector3 spawnPosition = transform.position + localOffset;
        Instantiate(NextMap, spawnPosition, transform.rotation);  //instantiate first new block, this works fine
        Vector3 newRoadSpawnPosition = RoadSpawnPoint.transform.position;
        newRoadSpawnPosition.z = RoadSpawnPoint.transform.position.z + 1000; //trying to create a new point of instantiation for the road 1000 units along from the previous one
        //Debug.Log("Straight Road spawned.");
    }



}
