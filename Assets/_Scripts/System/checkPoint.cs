using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkPoint : MonoBehaviour {


    private static float x = 0;
    private static float y = 0;
    private static float z = 0;

    public Transform DragTransform;
    public Transform player;
    static Transform checkPointTransform;
    static Transform playerTransform;

    private void Start()
    {
        playerTransform = player;
        checkPointTransform = DragTransform;
    }

    public static void setSavePoint(float x, float y, float z)
    {
        checkPoint.x = x;
        checkPoint.y = y;
        checkPoint.z = z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            checkPointTransform = DragTransform;
            Debug.Log("Checkpoint!");
            Vector3 checkPos = new Vector3(checkPointTransform.transform.position.x, checkPointTransform.transform.position.y, checkPointTransform.transform.position.z);
            setSavePoint(checkPos.x, checkPos.y, checkPos.z);
            Debug.Log(checkPos.x.ToString()+", "+ checkPos.x.ToString()+", "+ checkPos.z.ToString());
        }
    }

    public static void respawnPlayerAtCheckPoint()
    {
        playerTransform.SetPositionAndRotation(new Vector3(checkPointTransform.transform.position.x, checkPointTransform.transform.position.y + 0.5f, checkPointTransform.transform.position.z), checkPointTransform.gameObject.transform.rotation);
        Debug.Log("Respawned!");
        Debug.Log(checkPoint.x.ToString() + ", " + checkPoint.x.ToString() + ", " + checkPoint.z.ToString());
    }
}
