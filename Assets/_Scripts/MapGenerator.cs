using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject CurrentSegment;
    public GameObject[] RoadSegments;
    bool createdNext;

    // Use this for initialization
    void Start () {
        createdNext = false;
    }

    int countDown = 60 * 10;
	// Update is called once per frame
	void FixedUpdate () {
        if ( countDown-- <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("Hit something");
        if (collision.gameObject.tag.Equals("Player") && !createdNext)
        {
            SpawnMap();
        }

    }

    // Spawn new map
    void SpawnMap()
    {

        int randomed = Random.Range(0, RoadSegments.Length - 1);

        var lastTileBounds = RoadSegments[randomed].GetComponent<MeshFilter>().mesh.bounds;

        Instantiate(RoadSegments[randomed],
            new Vector3(RoadSegments[randomed].transform.position.x - lastTileBounds.size.x * RoadSegments[randomed].transform.localScale.x,
            RoadSegments[randomed].transform.position.y,
            RoadSegments[randomed].transform.position.z),
            RoadSegments[randomed].transform.rotation
            );

        createdNext = true;

    }

}
