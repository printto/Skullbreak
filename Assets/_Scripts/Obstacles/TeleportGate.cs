using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour {
    [SerializeField]
    private GameObject teleportEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("PlayerFace"))
        {
            Debug.Log("teleportable");
            PlayerNew playerScript = other.gameObject.GetComponent<PlayerNew>();
            PlayerNew.teleportEnd = teleportEnd;
            playerScript.teleportable = true;
            //playerScript.playSound(playerScript.soundEffect.TeleportReadySounds);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("PlayerFace"))
        {
            PlayerNew playerScript = other.gameObject.GetComponent<PlayerNew>();
            playerScript.teleportable = false;
        }
    }
}
