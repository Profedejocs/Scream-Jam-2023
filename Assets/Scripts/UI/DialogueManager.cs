using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText; //character's name object
    public Text dialogueText; //dialogue text object
    private Queue<string> sentences;
    public GameObject dialogueBox;
    float gate;
    //Insert reference to trigger here
    public bool isDialoging;
    
    void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            //Trigger implementation
        }

        sentences = new Queue<string>();
    }

    public void Startdialogue(AltDialogue dialogue)
    {
        isDialoging = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        dialogueBox.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string curSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(curSentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        gate = 5;

        foreach (char letter in sentence.ToCharArray())
        {
            if(gate == 5)
            {
                //Insert AudioSource
                gate = 0;
            }
            else
            {
                gate += 1;
            }
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        isDialoging = false;
        dialogueBox.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
