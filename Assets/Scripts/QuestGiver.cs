using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerScript player;
    public TextMeshProUGUI descriptionText;
    public Button missionButton;

    public bool questCompleted = false;

    public QuestManager questManager;

    void Start()
    {

        questManager = FindObjectOfType<QuestManager>();
        if (questManager == null)
        {
#if UNITY_EDITOR
            Debug.LogError("QuestManager not found in the scene. Make sure to add it.");
#endif
        }
    }

    public void ShowQuest()
    {
        if (!questCompleted)
        {
            descriptionText.text = quest.description;
            // AcceptQuest();
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Quest is already completed. Cannot show again.");
#endif
        }
    }


    public void AcceptQuest()
    {
        if (!questCompleted)
        {
            if (!quest.isActive)
            {
                quest.isActive = true;
                player.quest = quest;
#if UNITY_EDITOR
                Debug.Log("Quest accepted: " + quest.description);
#endif

                questManager.AddQuestDescription(quest.description);
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Quest is already active");
#endif
        }
    }


    public void MarkQuestCompleted()
    {
        questCompleted = true;
        Debug.Log("Quest marked as completed");
    }

    public void AssignMethod()
    {
        if (questCompleted == false)
        {
            missionButton.onClick.AddListener(ShowQuest);

#if UNITY_EDITOR
            Debug.Log("Assign Successful");
#endif

        }
    }



}