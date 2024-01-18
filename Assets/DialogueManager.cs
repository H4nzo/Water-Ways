using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    public GameObject dialogueBox;

    public GameObject player;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        animator.SetBool("isOpen", true);
        player.GetComponent<PlayerScript>().enabled = false;
        player.GetComponent<Animator>().SetBool("Idle", true);
        player.GetComponent<Animator>().SetBool("Run", false);


        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;

        }
    }

    void EndDialogue()
    {
        // Debug.Log("End of Convo");
        animator.enabled = false;
        dialogueBox.SetActive(false);
        animator.SetBool("isOpen", !true);
        player.GetComponent<PlayerScript>().enabled = true;
    }

}
