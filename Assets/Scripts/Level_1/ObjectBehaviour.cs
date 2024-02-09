using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public UnityEvent onTrigger;
    private Quest quest;
    private Collider targetCol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetCol = other;
            onTrigger?.Invoke();
        }
    }

    public void GatheringCount()
    {

        PlayerScript playerScript = targetCol.GetComponent<PlayerScript>();
        quest = playerScript.quest;

        if (quest.isActive)
        {
            quest.goal.ItemFound();
            GetComponent<Collider>().enabled = false;
            if (quest.goal.isReached())
            {
                //Activate Checkmark
                playerScript.UpdateUI();
                quest.Complete();
                quest = null;

            }
        }
    }

    public void InvestigateCount()
    {

        PlayerScript playerScript = targetCol.GetComponent<PlayerScript>();
        quest = playerScript.quest;

        if (quest.isActive)
        {
            quest.goal.ItemInvestigated();
            
            GetComponent<Collider>().enabled = false;
            if (quest.goal.isReached())
            {
                //Activate Checkmark
                playerScript.UpdateUI();
                quest.Complete();
                quest = null;

            }
        }
    }

}
