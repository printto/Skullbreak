using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashable : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Dashable: Hit something");
        if (collision.gameObject.tag.Equals("PlayerFace") && Player.isDashing)
        {
            Destroy(gameObject);
        }
    }

}
