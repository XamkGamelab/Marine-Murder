using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroscopePuzzleState : BaseState
{
    private PlayerSM _sm;
    private GameObject _zoomVirtualCamera;

    public MicroscopePuzzleState(PlayerSM stateMachine) : base("MicroscopePuzzleState", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter(GameObject zoomVirtualCamera)
    {
        _sm.firstPersonInputModule.enabled = false;
        _sm.inputSystemUIInputModule.enabled = true;
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.Confined;
        _sm.crosshair.SetActive(false);

        _sm.inventoryPanel.SetActive(false);
        _sm.puzzleButtonsParent.SetActive(true);
        _sm.PlayerFollowCamera.SetActive(false);
        zoomVirtualCamera.SetActive(true);

        _zoomVirtualCamera = zoomVirtualCamera;
        _sm.puzzleScript.StartPuzzle();
    }

    public override void Exit()
    {
        _sm.firstPersonInputModule.enabled = true;
        _sm.inputSystemUIInputModule.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        _sm.crosshair.SetActive(true);

        _sm.inventoryPanel.SetActive(true);
        _sm.puzzleButtonsParent.SetActive(false);
        _sm.PlayerFollowCamera.SetActive(true);
        _zoomVirtualCamera.SetActive(false);
    }

    public override void UpdateLogic()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            _sm.ChangeState(_sm.defaultState);
        if (Input.GetKeyDown(KeyCode.A))
            _sm.puzzleScript.Rotate(1);
        if (Input.GetKeyDown(KeyCode.D))
            _sm.puzzleScript.Rotate(0);
    }

    public override void OnInteract(Camera mainCamera, DialogueScript dialogueScript) { }
}
