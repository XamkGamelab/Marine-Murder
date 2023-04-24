using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueState : BaseState
{
    private PlayerSM _sm;

    public DialogueState(PlayerSM stateMachine) : base("DialogueState", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _sm.inventoryPanel.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();

        _sm.inventoryPanel.SetActive(true);
    }

    public override void CameraRotation(StarterAssetsInputs input, float rotationSpeed, float deltaTimeMultiplier, float bottomClamp, float topClamp, GameObject cinemachineCameraTarget, Transform playerTransform){ }

    public override void OnInteract(Camera mainCamera, DialogueScript dialogueScript)
    {
        dialogueScript.NextDialogue();
    }
}
