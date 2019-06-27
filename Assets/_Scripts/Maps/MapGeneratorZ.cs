using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorZ : MonoBehaviour {

    public GameObject CurrentSegment;
    public GameObject[] RoadSegments;
    public bool createdNext;

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
    public void SpawnMap()
    {

        int randomed = Random.Range(0, RoadSegments.Length);
        Debug.Log(randomed);

        var lastTileBounds = GetComponent<MeshFilter>().mesh.bounds;

        Instantiate(RoadSegments[randomed],
            new Vector3(transform.position.x - lastTileBounds.size.z * transform.localScale.z,
            transform.position.y,
            transform.position.z),
            transform.rotation
            );

        createdNext = true;

    }

}
