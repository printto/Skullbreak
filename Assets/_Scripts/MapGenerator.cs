using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject roadSegment;

    // Use this for initialization
    void Start () {
		
	}

    int countDown = 60 * 3;
	// Update is called once per frame
	void FixedUpdate () {
        if ( countDown-- <= 0)
        {
            Destroy(gameObject);
        }
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
        var lastTileBounds = roadSegment.GetComponent<MeshFilter>().mesh.bounds;

        Instantiate(roadSegment,
            new Vector3(roadSegment.transform.position.x - lastTileBounds.size.x * roadSegment.transform.localScale.x,
            roadSegment.transform.position.y,
            roadSegment.transform.position.z),
            roadSegment.transform.rotation
            );
    }

}
