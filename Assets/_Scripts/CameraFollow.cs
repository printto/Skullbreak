using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;

    
    private float transition = 0.0f;
    private float animationDuration = 2.0f;
    private Vector3 animatonOffset = new Vector3(0, 5, 5);
    
    
    // Use this for initialization
	void Start () {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update () {
       //transform.position = lookAt.position + startOffset;
        
       moveVector = lookAt.position + startOffset;
        
        //Y
        moveVector.y = Mathf.Clamp(moveVector.y,3,5);
   

        //Z
        moveVector.z = 0;


        if (transition > 1.0f)
       {
           transform.position = moveVector;
       }
       else
       {
           //Animation at the start of the game
           transform.position = Vector3.Lerp(moveVector + animatonOffset, moveVector, transition);
           transition += Time.deltaTime * 1 / animationDuration;
           transform.LookAt(lookAt.position + Vector3.up);

        }
        

    }
}
