using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimatorHelper))]
public class NpcController : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float collisonDelay = 2f;
    private BoxCollider collider;
    public UnityEvent OnTriggerEnter;
    public UnityEvent OnTriggerExit;


    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    public void LookAtTarget(Transform target)
    {
        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0; // Ensure the rotation is only in the horizontal plane

        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public IEnumerator TriggerDelay()
    {
        collider.isTrigger = false;
        yield return new WaitForSeconds(collisonDelay);
        collider.isTrigger = true;
    }


}

public class NpcAnimatorHelper : MonoBehaviour
{
    public Animator animator;
    public string paramName = "isTalking";

    public void SetAnimationBool(bool value)
    {
        animator.SetBool(paramName, value);
    }
}