using DS.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DS;
using TMPro;
using DS.Enumerations;
using DS.Data;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private DSDialogueSO startingDialogue;
    [SerializeField] private TMP_Text textUI;
    [SerializeField] private Transform choiceButtonParent;
    [SerializeField] private GameObject textArea;
    [SerializeField] private GameEvent endDialogue;
    [SerializeField] private GameObject crosshair;

    private DSDialogueSO currentDialogue;
    private List<Transform> buttons = new List<Transform>();

    private void Awake()
    {
        //currentDialogue = startingDialogue;

        foreach (Transform child in choiceButtonParent)
            buttons.Add(child);
    }

    private void Start()
    {
        //ShowText();
    }

    private void ShowText()
    {
        if (currentDialogue.DialogueType == DSDialogueType.SingleChoice)
        {
            textUI.text = currentDialogue.Text;
            choiceButtonParent.gameObject.SetActive(false);
        }

        else if (currentDialogue.DialogueType == DSDialogueType.MultipleChoice)
        {
            for (int i = 0; i < currentDialogue.Choices.Count; i++)
            {
                buttons[i].GetComponentInChildren<TMP_Text>().text = currentDialogue.Choices[i].Text;
            }
            choiceButtonParent.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Unknown dialogue type.");
        }
    }

    public void OnOptionChosen(int choiceIndex)
    {
        DSDialogueSO nextDialogue = currentDialogue.Choices[choiceIndex].NextDialogue;

        // no more dialogues
        if (nextDialogue == null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            textArea.SetActive(false);
            choiceButtonParent.gameObject.SetActive(false);
            crosshair.SetActive(true);
            endDialogue.Raise();

            return;
        }

        currentDialogue = nextDialogue;

        ShowText();
    }

    // called with left click during dialogue
    public void NextDialogue()
    {
        // only allow this if there is no multiple choice
        if (currentDialogue.DialogueType == DSDialogueType.SingleChoice)
            OnOptionChosen(0);
    }

    public void StartDialogue(DSDialogueSO dialogueNode)
    {
        currentDialogue = dialogueNode;
        ShowText();
        Cursor.lockState = CursorLockMode.Confined;
        crosshair.SetActive(false);
        textArea.SetActive(true);
    }
}
