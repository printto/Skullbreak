using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportation : MonoBehaviour {
    private bool isTeleporting;
    private float energy;
    private PlayerNew player;
    

	// Use this for initialization
	void Start () {
        isTeleporting = false;
        energy = 4f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isTeleporting)
        {
            RestoreEnergy();
        }
	}

    // Restore player's teleport energy every second if not teleporitng
    void RestoreEnergy()
    {
        if (energy < 4f)
        {
            energy += Time.deltaTime;
        }
    }

    // Start the teleportation if not run out of energy
    void StartTeleport ()
    {
        if (energy != 0f)
        {
            player.MoveSpeed += player.speedUp;
            isTeleporting = true;
        }
    }

    // Stop the teleportation
    void StopTeleport ()
    {
        player.MoveSpeed -= player.speedUp;
        isTeleporting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals(""))
        {

        }
    }
}
