using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showInstructions : MonoBehaviour
{

    public Transform DragTransform;
    public Transform player;
    public Text instruction;
    static Transform checkPointTransform;
    static Transform playerTransform;

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
        if (other.gameObject.tag.Equals("Player") && other.GetComponent<Player>().getTutorialCount() == 0)
        {
            Debug.Log(other.GetComponent<Player>().getTutorialCount());
            instruction.text = "WELCOME TO DOGWALK!";
            other.GetComponent<Player>().AddCount(1);
            saveCheckPoint();
        }

        else if (other.gameObject.tag.Equals("Player") && other.GetComponent<Player>().getTutorialCount() == 1)
        {
            Debug.Log(other.GetComponent<Player>().getTutorialCount());
            instruction.fontSize = 30;
            instruction.text = "LET'S US SHOW YOU HOW TO PLAY!";
            other.GetComponent<Player>().AddCount(1);
            saveCheckPoint();
        }

        else if (other.gameObject.tag.Equals("Player") && other.GetComponent<Player>().getTutorialCount() == 2)
        {
            Debug.Log(other.GetComponent<Player>().getTutorialCount());
            instruction.text = "TO STEER \n DRAG MONSTER TO LEFT AND RIGHT!";
            other.GetComponent<Player>().AddCount(1);
            saveCheckPoint();
        }

        else if (other.gameObject.tag.Equals("Player") && other.GetComponent<Player>().getTutorialCount() == 3)
        {
            Debug.Log(other.GetComponent<Player>().getTutorialCount());
            instruction.text = "GOOD JOB!!";
            other.GetComponent<Player>().AddCount(1);
            saveCheckPoint();
        }

        else if (other.gameObject.tag.Equals("Player") && other.GetComponent<Player>().getTutorialCount() == 4)
        {
            Debug.Log(other.GetComponent<Player>().getTutorialCount());
            instruction.text = "NOW LET'S TRY JUMPING!   SWIPE UP TO JUMP";
            other.GetComponent<Player>().AddCount(1);
            saveCheckPoint();
        }

        else if (other.gameObject.tag.Equals("Player") && other.GetComponent<Player>().getTutorialCount() == 5)
        {
            Debug.Log(other.GetComponent<Player>().getTutorialCount());
            StartCoroutine(displayText());
            other.GetComponent<Player>().AddCount(1);
            saveCheckPoint();
        }


        else if (other.gameObject.tag.Equals("Player") && other.GetComponent<Player>().getTutorialCount() == 6)
        {
            Debug.Log(other.GetComponent<Player>().getTutorialCount());
            instruction.text = "NOW YOU ARE READY!!";
            other.GetComponent<Player>().AddCount(1);
            saveCheckPoint();
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
        instruction.text = "GOOD STUFF! NOW USE WHAT YOU LEARN AND TRY COLLECTING THESE COINS";
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