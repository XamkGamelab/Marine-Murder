using DS.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DS;
using TMPro;
using DS.Enumerations;
using DS.Data;
using UnityEngine.InputSystem.UI;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private FirstPersonInputModule firstPersonInputModule;
    [SerializeField] private InputSystemUIInputModule inputSystemUIInputModule;
    [SerializeField] private InventoryScript inventory;
    [SerializeField] private TMP_Text textUI;
    [SerializeField] private Transform choiceButtonParent;
    [SerializeField] private GameObject textArea;
    [SerializeField] private GameEventSO endDialogue;
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
            Debug.Log("text should be: " + currentDialogue.Text);
            textUI.text = currentDialogue.Text;
            choiceButtonParent.gameObject.SetActive(false);
        }

        else if (currentDialogue.DialogueType == DSDialogueType.MultipleChoice)
        {
            // go through all buttons
            for (int i = 0; i < buttons.Count; i++)
            {
                // only enable needed amount of buttons
                if (i < currentDialogue.Choices.Count)
                {
                    if (currentDialogue.Choices[i].NeedsItem == null)
                    {
                        buttons[i].gameObject.SetActive(true);
                        buttons[i].GetComponentInChildren<TMP_Text>().text = currentDialogue.Choices[i].Text;
                    }
                    else if (inventory.items.Contains(currentDialogue.Choices[i].NeedsItem))
                    {
                        buttons[i].gameObject.SetActive(true);
                        buttons[i].GetComponentInChildren<TMP_Text>().text = currentDialogue.Choices[i].Text;
                    }
                    else
                        buttons[i].gameObject.SetActive(false);
                }
                else
                    buttons[i].gameObject.SetActive(false);
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
        Debug.Log("Option chosen: " + choiceIndex);
        DSDialogueSO nextDialogue = currentDialogue.Choices[choiceIndex].NextDialogue;

        // no more dialogues
        if (nextDialogue == null)
        {
            firstPersonInputModule.enabled = true;
            inputSystemUIInputModule.enabled = false;

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
        firstPersonInputModule.enabled = false;
        inputSystemUIInputModule.enabled = true;
        Cursor.visible = true;

        currentDialogue = dialogueNode;
        ShowText();
        Cursor.lockState = CursorLockMode.Confined;
        crosshair.SetActive(false);
        textArea.SetActive(true);
    }
}
