using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodelockZoomState : ControllableZoomState
{
    float cinemachineTargetPitch;
    float cinemachineTargetYaw;

    public CodelockZoomState(PlayerSM stateMachine) : base("CodelockZoomState", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Exit()
    {
        base.Exit();

        cinemachineTargetPitch = 0;
        cinemachineTargetYaw = 0;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (Input.GetKeyDown(KeyCode.Escape))
            ExitState();
    }

    public override void CameraRotation(StarterAssetsInputs input, float rotationSpeed, float deltaTimeMultiplier, float bottomClamp, float topClamp, GameObject cinemachineCameraTarget, Transform playerTransform)
    {
        cinemachineTargetPitch += input.look.y * rotationSpeed * deltaTimeMultiplier;
        cinemachineTargetYaw += input.look.x * rotationSpeed * deltaTimeMultiplier;
        ZoomVirtualCamera.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, cinemachineTargetYaw - 180f, 0.0f);

        // horizontal gradient calculations
        float t = Mathf.Clamp(Mathf.Abs(cinemachineTargetYaw), _sm.HorizontalStartGradientLock, _sm.HorizontalEndGradientLock);
        t -= _sm.HorizontalStartGradientLock;
        t /= (_sm.HorizontalEndGradientLock - _sm.HorizontalStartGradientLock);
        // left gradient
        if (cinemachineTargetYaw < 0)
            _sm.leftGradient.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
        // right gradient
        else
            _sm.rightGradient.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));

        if (t >= 1)
            ExitState();

        // vertical gradient calculations
        t = Mathf.Clamp(Mathf.Abs(cinemachineTargetPitch), _sm.VerticalStartGradientLock, _sm.VerticalEndGradientLock);
        t -= _sm.VerticalStartGradientLock;
        t /= (_sm.VerticalEndGradientLock - _sm.VerticalStartGradientLock);
        //top gradient
        if (cinemachineTargetPitch < 0)
            _sm.topGradient.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
        // bottom gradient
        else
            _sm.bottomGradient.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));

        if (t >= 1)
            ExitState();
    }

    private void ExitState()
    {
        _sm.leftGradient.color = new Color(1, 1, 1, 0);
        _sm.rightGradient.color = new Color(1, 1, 1, 0);
        _sm.topGradient.color = new Color(1, 1, 1, 0);
        _sm.bottomGradient.color = new Color(1, 1, 1, 0);

        _sm.ChangeState(_sm.defaultState);
    }
}
