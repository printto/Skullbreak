using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showInstructions : MonoBehaviour
{

    public Transform DragTransform;
    public Transform player;
    public Text instruction;
    public string TextToShow;
    static Transform checkPointTransform;
    static Transform playerTransform;
    public bool isCoroutine = false;

    float duration = 5;


    private static float x = 0;
    private static float y = 0;
    private static float z = 0;

    private void Start()
    {

        playerTransform = player;
        checkPointTransform = DragTransform;
    }

    public static void setSavePoint(float x, float y, float z)
    {
        showInstructions.x = x;
        showInstructions.y = y;
        showInstructions.z = z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (!isCoroutine)
            {
                instruction.text = TextToShow;
                saveCheckPoint();
            }
            else
            {
                StartCoroutine(displayText());
                saveCheckPoint();
            }
        }
            
    }

    private void saveCheckPoint()
    {
        checkPointTransform = DragTransform;
        Debug.Log("Checkpoint!");
        Vector3 checkPos = new Vector3(checkPointTransform.transform.position.x, checkPointTransform.transform.position.y, checkPointTransform.transform.position.z);
        setSavePoint(checkPos.x, checkPos.y, checkPos.z);
        Debug.Log(checkPos.x.ToString() + ", " + checkPos.x.ToString() + ", " + checkPos.z.ToString());
    }

    IEnumerator displayText()
    {
        instruction.text = TextToShow;
        yield return new WaitForSeconds(3f);
        instruction.text = "";
    }


    public static void respawnPlayerAtCheckPoint()
    {   

        playerTransform.SetPositionAndRotation(new Vector3(checkPointTransform.transform.position.x - 3f, checkPointTransform.transform.position.y + 1f, checkPointTransform.transform.position.z + 1f), checkPointTransform.gameObject.transform.rotation);
        Debug.Log("Respawned!");
        Debug.Log(showInstructions.x.ToString() + ", " + showInstructions.x.ToString() + ", " + showInstructions.z.ToString());
    }

}