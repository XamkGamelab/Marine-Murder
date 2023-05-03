using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomState : ZoomState
{
    public CameraZoomState(PlayerSM stateMachine) : base("CameraZoomState", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter(GameObject zoomVirtualCamera)
    {
        ZoomVirtualCamera = zoomVirtualCamera;
        _sm.PlayerFollowCamera.SetActive(false);
        zoomVirtualCamera.SetActive(true);
    }
}
