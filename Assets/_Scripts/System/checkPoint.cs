using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkPoint : MonoBehaviour {


    private static float x = 0;
    private static float y = 0;

    public int lane = 1;
    
    static Transform ThisTransform;

    static GameObject playerObject;

    private void Start()
    {
        
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    public static void setSavePoint(float x, float y, float z)
    {
        checkPoint.x = x;
        checkPoint.y = y;
        //checkPoint.z = z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ThisTransform = transform;
            //Debug.Log("Checkpoint!");
            Vector3 checkPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            setSavePoint(checkPos.x, checkPos.y, checkPos.z);
            //Debug.Log(checkPos.x.ToString()+", "+ checkPos.y.ToString()+", "+ checkPos.z.ToString());
            PlayerNew.checkPoint = this;
            Debug.Log(checkPos.x.ToString() + ", " + checkPos.y.ToString());
        }
    }

    public void respawnPlayerAtCheckPoint()
    {   
        playerObject.transform.SetPositionAndRotation(new Vector3(ThisTransform.position.x, ThisTransform.position.y + 0.5f, playerObject.GetComponent<PlayerNew>().LaneZs[lane]), ThisTransform.rotation);
        playerObject.GetComponent<PlayerNew>().currentLane = lane;
        Debug.Log("Respawned!");
        Debug.Log(checkPoint.x.ToString() + ", " + checkPoint.y.ToString() + ", " + playerObject.GetComponent<PlayerNew>().LaneZs[lane]);
    }
}
