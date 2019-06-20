using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPad : MonoBehaviour {

    public float DegreeToTurn = 0;
    public float Delay = 1;

    //bool turned = false;

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

    /*
   private void OnCollisionEnter(Collision collision)
   {
       if (collision.gameObject.tag.Equals("Player") && !turned)
       {
           turned = true;
           StartCoroutine(TurnPlayer(collision));
       }
   }
   */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            StartCoroutine(TurnPlayer(other));
        }

    }
    IEnumerator TurnPlayer(Collider collider)
    {
        yield return new WaitForSeconds(Delay);
        Vector3 temp = collider.gameObject.transform.eulerAngles + new Vector3(0, DegreeToTurn, 0);
        collider.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        collider.gameObject.GetComponent<Transform>().eulerAngles = temp;
        Vector3 temp2 = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Transform>().eulerAngles + new Vector3(0, DegreeToTurn, 0);
        GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Transform>().eulerAngles = temp2 ;
    }

}
