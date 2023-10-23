using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI textComponent;
    [SerializeField]
    public GameObject diagBackground;


    public string[] lines;

    private int index;

    [SerializeField]
    private GameObject diagBoxGameObject;
   
    void Start()
    {
        textComponent.text = string.Empty;
        Hide();
    }

    
    void Update()
    {
        //Insert code to trigger dialogue box and text.
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return null;
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        diagBackground.SetActive(true);
        textComponent.gameObject.SetActive(true);
    }

    private void Hide()
    {
        diagBackground.SetActive(false);
        textComponent.gameObject.SetActive(false);
    }
}
