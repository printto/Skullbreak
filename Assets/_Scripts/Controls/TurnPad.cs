using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPad : MonoBehaviour {

    public float DegreeToTurn = 0;
    public float Delay = 1;

    bool turned = false;

    void Update()
    {
        /* //use this to turn 90 degree but we want player to auto turn
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
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !turned)
        {
            turned = true;
            StartCoroutine(TurnPlayer(collision));
        }
    }

    IEnumerator TurnPlayer(Collision collision)
    {
        yield return new WaitForSeconds(Delay);
        Vector3 temp = collision.gameObject.transform.eulerAngles + new Vector3(0, DegreeToTurn, 0);
        collision.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        collision.gameObject.GetComponent<Transform>().eulerAngles = temp;
    }

}
