using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : BaseState
{
    private PlayerSM _sm;

    public PauseState(PlayerSM stateMachine) : base("PauseState", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        _sm.pausePanel.SetActive(true);

        _sm.firstPersonInputModule.enabled = false;
        _sm.inputSystemUIInputModule.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        _sm.crosshair.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1f;
        _sm.pausePanel.SetActive(false);

        _sm.firstPersonInputModule.enabled = true;
        _sm.inputSystemUIInputModule.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _sm.crosshair.SetActive(true);
    }
}
