using DS.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DS;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private DSDialogueSO startingDialogue;

    private DSDialogueSO currentDialogue;

    private void Awake()
    {
        currentDialogue = startingDialogue;
    }

    private void ShowText()
    {
        textUI.text = currentDialogue.Text;
    }

    private void OnOptionChosen(int choiceIndex)
    {
        DSDialogueSO nextDialogue = currentDialogue.Choices[choiceIndex].NextDialogue;

        if (nextDialogue == null)
        {
            return; // No more dialogues to show, do whatever you want, like setting the currentDialogue to the startingDialogue
        }

        currentDialogue = nextDialogue;

        ShowText();
    }
}
