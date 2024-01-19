using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NpcController : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public UnityEvent OnTriggerEnter;
    public UnityEvent OnTriggerExit;

    public void LookAtTarget(Transform target)
    {
        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0; // Ensure the rotation is only in the horizontal plane

        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    
}
