using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_CameraFollow : MonoBehaviour
{
    // the player to target
    public Transform target;

    // the higher the value, the fast it will lock on, the smaller, the longer it will take to get to it. MAKE THIS 0-1
    public float smoothSpeed = 0.125f;

    // to preserve the bird-eye's-view
    public Vector3 offset;

    // to avoid weird jitteriness
    private void FixedUpdate()
    {
        // get the position we want the camera to snap to
        Vector3 desiredPosition = target.position + offset;

        // slowly get to that desired position with a travel time
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // make that slowly happen
        transform.position = smoothedPosition;

        // have the camera always look at the player. probably don't need this
        //transform.LookAt(target);
    }
}
