using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class PlayerScript : MonoBehaviour
{
    const string RUN = "Run";

    NavMeshAgent agent;
    Animator animator;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 8f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check for left mouse button or touch input
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector2 inputPosition = Input.GetMouseButtonDown(0) ? Input.mousePosition : Input.GetTouch(0).position;
            ClickToMove(inputPosition);
        }

        FaceTarget();
        SetAnimation();
    }

    void ClickToMove(Vector2 inputPosition)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);

        if (Physics.Raycast(ray, out hit, 100, clickableLayers))
        {
            agent.destination = hit.point;

            if (clickEffect != null)
            {
                Instantiate(clickEffect, hit.point + new Vector3(0, .1f, 0), clickEffect.transform.rotation);
            }
        }
    }

    private void SetAnimation()
    {
        if (agent.velocity == Vector3.zero)
        {
            animator.SetBool(RUN, false);
        }
        else
        {
            animator.SetBool(RUN, true);
        }
    }

    private void FaceTarget()
    {
        if (agent.velocity != Vector3.zero)
        {
            Vector3 dir = (agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }
}






















// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;
// using UnityEngine.InputSystem;

// public class PlayerScript : MonoBehaviour
// {
//     const string RUN = "Run";

//     CustomActions input;
//     NavMeshAgent agent;

//     Animator animator;

//     [Header("Movement")]
//     [SerializeField] ParticleSystem clickEffect;
//     [SerializeField] LayerMask clickableLayers;

//     float lookRotationSpeed = 8f;

//     void Awake()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         animator = GetComponent<Animator>();
//         input = new CustomActions();
//         AssignInputs();
//     }


//     void AssignInputs()
//     {
//         input.Main.Move.performed += ctx => ClickToMove();
//     }

//     void ClickToMove()
//     {
//         RaycastHit hit;
//         if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
//         {
//             agent.destination = hit.point;
//             if (clickEffect != null)
//             {
//                 Instantiate(clickEffect, hit.point += new Vector3(0, .1f, 0), clickEffect.transform.rotation);
//             }
//         }
//     }

//     private void OnEnable()
//     {
//         input.Enable();
//     }

//     private void OnDisable()
//     {
//         input.Disable();
//     }

//     private void Update()
//     {
//         FaceTarget();
//         SetAnimation();
//     }

//     private void SetAnimation()
//     {
//         if (agent.velocity == Vector3.zero)
//         {
//             animator.SetBool(RUN, false);

//         }
//         else
//         {
//             animator.SetBool(RUN, true);
//         }
//     }

//     private void FaceTarget()
//     {
//         if(agent.velocity != Vector3.zero){
//         Vector3 dir = (agent.destination - transform.position).normalized;
//         Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
//         transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
//         }
//     }


// }
