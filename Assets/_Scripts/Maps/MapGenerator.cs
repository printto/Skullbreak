using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public GameObject CurrentSegment;
    public GameObject[] RoadSegments;
    public bool createdNext;

    // Use this for initialization
    void Start()
    {
        createdNext = false;
    }

    int countDown = 60 * 20;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (countDown-- <= 0)
        {
            //gameObject.SetActive(false);
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

        var lastTileBounds = GetComponent<MeshFilter>().mesh.bounds;

        string randomName = RoadSegments[randomed].name;

        //Object pool fetching
        /*
        ObjectPooler.Instance.FetchGO_Pos(randomName, new Vector3(transform.position.x - lastTileBounds.size.x * transform.localScale.x,
            transform.position.y,
            transform.position.z));
        if (fetchedObject == null)
        {
        */
            Instantiate(RoadSegments[randomed],
                new Vector3(transform.position.x - lastTileBounds.size.x * transform.localScale.x,
                transform.position.y,
                transform.position.z),
                transform.rotation
                );
        /*
        }
        */

        createdNext = true;

    }

}
