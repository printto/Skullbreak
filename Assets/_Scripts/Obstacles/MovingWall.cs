using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour {

    private float wallSize;
    private float startPosition;
    private float direction;

	// Use this for initialization
	void Start () {
        wallSize = 4f;
        startPosition = gameObject.transform.position.z;

        if (startPosition < 0)
        {
            direction = 1;
        } else
        {
            direction = -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        var zpos = gameObject.transform.position.z;
        var distance = Mathf.Abs(zpos - startPosition);

        //Moves wall until it gets to the right position.
        /*if(distance < 6.2)
        {
            transform.Translate(0, 0, 1 * wallSpeed * direction * Time.deltaTime);
        }
        */

        if(Mathf.Abs(transform.localScale.z) < wallSize)
        {
            transform.Translate(0, 0, 2.4f * direction * Time.deltaTime);
            transform.localScale += new Vector3(0, 0, 0.08f * direction);
        }
    }
}
