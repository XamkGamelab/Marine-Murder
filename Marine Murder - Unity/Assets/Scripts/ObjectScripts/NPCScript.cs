using DS.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class NPCScript : MonoBehaviour , IDirectInteract
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

    public void Interact(PlayerSM playerSM)
    {
        gameManager.StartDialogue(startingNode);
        playerSM.ChangeState(playerSM.dialogueState);
    }
}
