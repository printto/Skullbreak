using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGateSound : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("PlayerFace"))
        {
            PlayerNew playerScript = other.gameObject.GetComponent<PlayerNew>();
            playerScript.playSound(playerScript.soundEffect.TeleportReadySounds);
        }
    }

}
