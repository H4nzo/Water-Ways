using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public UnityEvent OnConversationStart;
    public UnityEvent OnConversationEnd;
    
    public void TriggerDialogue()
    {
        OnConversationStart?.Invoke();
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, OnConversationEnd);
    }
}
