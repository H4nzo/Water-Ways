using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using TMPro;


public class PlayerScript : MonoBehaviour
{
    const string RUN = "Run";
    const string DAMAGE = "Damaged";

    NavMeshAgent agent;
    Animator animator;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 8f;

    bool isOnAcidSteam = false;

    [Header("Foot Step")]
    public Transform footAnchor;
    public GameObject footStepFX;

    [Header("Quest Functions")]
    public Quest quest;
    public TextMeshProUGUI scoreText;

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

        if (quest != null && quest.isActive == true)
        {
            UpdateUI();
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
        if (isOnAcidSteam)
        {
            // Play DAMAGE animation
            animator.SetBool(RUN, false);
            animator.SetBool(DAMAGE, true);
        }
        else
        {
            // Play RUN animation
            if (agent.velocity == Vector3.zero)
            {
                animator.SetBool(RUN, false);
            }
            else
            {
                animator.Play("Run");
                animator.SetBool(RUN, true);
            }

            // Reset DAMAGE animation
            animator.SetBool(DAMAGE, false);
        }
    }

    private void FaceTarget()
    {
        if (agent.velocity != Vector3.zero)
        {
            Vector3 dir = (agent.steeringTarget - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mission"))
        {
            other.GetComponent<NpcController>().OnTriggerEnter?.Invoke();
            other.GetComponent<NpcController>().LookAtTarget(transform);

        }
        if (other.CompareTag("AcidSteam"))
        {
            Debug.Log("Detected " + other.name);
            isOnAcidSteam = true;
        }

        if (other.CompareTag("Well"))
        {
            if (quest.isActive)
            {
                quest.goal.ItemFound();
                other.GetComponent<CustomEvent>().Trigger?.Invoke();
                other.GetComponent<Collider>().enabled = false;
                if (quest.goal.isReached())
                {
                    //Activate Checkmark
                    UpdateUI();
                    quest.Complete();
                    quest = null;

                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mission"))
        {
            other.GetComponent<NpcController>().OnTriggerExit?.Invoke();

        }
        if (other.CompareTag("AcidSteam"))
        {
            Debug.Log("Exited " + other.name);
            isOnAcidSteam = false;
        }
    }


    void UpdateUI()
    {
        scoreText.text = $"{quest.goal.currentAmount}/{quest.goal.requiredAmount}";
    }


    //Animator Event System
    void FootStep()
    {
        Instantiate(footStepFX, footAnchor.position, footAnchor.rotation);
    }
   




}





