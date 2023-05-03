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
        if (Input.GetKeyDown(KeyCode.Escape))
            ExitState();

        if (Input.GetKeyDown(KeyCode.A))
            _sm.puzzleScript.Rotate(1);
        if (Input.GetKeyDown(KeyCode.D))
            _sm.puzzleScript.Rotate(0);

        Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        // horizontal gradient calculations
        float t = Mathf.Clamp(Mathf.Abs(mousePos.x), _sm.HorizontalStartGradientMicroscope, _sm.HorizontalEndGradientMicroscope);
        t -= _sm.HorizontalStartGradientMicroscope;
        t /= (_sm.HorizontalEndGradientMicroscope - _sm.HorizontalStartGradientMicroscope);
        // left gradient
        if (mousePos.x < 0)
            _sm.leftGradient.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
        // right gradient
        else
            _sm.rightGradient.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));

        if (t >= 1)
            ExitState();

        // vertical gradient calculations
        t = Mathf.Clamp(Mathf.Abs(mousePos.y), _sm.VerticalStartGradientMicroscope, _sm.VerticalEndGradientMicroscope);
        t -= _sm.VerticalStartGradientMicroscope;
        t /= (_sm.VerticalEndGradientMicroscope - _sm.VerticalStartGradientMicroscope);
        //top gradient
        if (mousePos.y > 0)
            _sm.topGradient.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
        // bottom gradient
        else
            _sm.bottomGradient.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));

        if (t >= 1)
            ExitState();
    }

    public override void OnInteract(Camera mainCamera, DialogueScript dialogueScript) { }

    private void ExitState()
    {
        _sm.leftGradient.color = new Color(1, 1, 1, 0);
        _sm.rightGradient.color = new Color(1, 1, 1, 0);
        _sm.topGradient.color = new Color(1, 1, 1, 0);
        _sm.bottomGradient.color = new Color(1, 1, 1, 0);

        _sm.ChangeState(_sm.defaultState);
    }
}
