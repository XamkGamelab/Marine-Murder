using DS.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class NPCScript : MonoBehaviour , IInteract
{
    [SerializeField] private DSDialogueSO startingNode;
    [SerializeField] private DialogueScript gameManager;
    [SerializeField] private FirstPersonController firstPersonController;

    public string GetExamineText()
    {
        throw new System.NotImplementedException();
    }

    public string GetInteractText()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        gameManager.StartDialogue(startingNode);
        firstPersonController.playerState = PlayerState.dialogue;
    }
}
