using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSmooth : MonoBehaviour
{
    public Transform target;
    public Transform holder;

    public float smoothSpeed = 0.125f;
    public float yPosRestriction = -1;
    public Vector3 offset;

    void Update()
    {
        Vector3 desiredPosition = holder.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);        

    }

}