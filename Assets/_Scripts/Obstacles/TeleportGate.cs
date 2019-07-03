﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour {
    public GameObject teleportEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("PlayerFace"))
        {
            Debug.Log("teleportable");
            PlayerNew.teleportable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("PlayerFace"))
        {
            PlayerNew.teleportable = false;
        }
    }

    public GameObject getTeleportEnd()
    {
        return teleportEnd;
    }
}
