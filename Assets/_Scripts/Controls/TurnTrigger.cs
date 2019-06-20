using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTrigger : MonoBehaviour {

    public float DegreeToTurn = 0;
    public float Delay = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            StartCoroutine(TurnPlayer(other));
        }
    }

    IEnumerator TurnPlayer(Collider collision)
    {
        yield return new WaitForSeconds(Delay);
        Vector3 temp = collision.gameObject.transform.eulerAngles + new Vector3(0, DegreeToTurn, 0);
        collision.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        collision.gameObject.GetComponent<Transform>().eulerAngles = temp;
    }

}
