using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn : MonoBehaviour {


    //use this to turn 90 degree but we want player to auto turn
    void Update()
    {
        if(Input.GetKeyDown("k"))
        {
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -2, 0);
            StartCoroutine(RotationLeft());
        }

        if (Input.GetKeyDown("l"))
        {
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 2, 0);
            StartCoroutine(RotationRight());
        }
    }

    IEnumerator RotationLeft()
    {
        yield return new WaitForSeconds(.8f);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        GetComponent<Transform>().eulerAngles = new Vector3(0,-90,0);
    }

    IEnumerator RotationRight()
    {
        yield return new WaitForSeconds(.8f);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        GetComponent<Transform>().eulerAngles = new Vector3(0, 90, 0);
    }
}
