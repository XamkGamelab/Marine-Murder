using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ZoomState : BaseState
{
    protected GameObject ZoomVirtualCamera;
    protected float Yaw, Pitch;
    protected PlayerSM _sm;

    private GameObject _targetGO;
    private Collider _targetCollider;
    private Vector3 _targetDirection;
    private Quaternion dir;


    public ZoomState(PlayerSM stateMachine) : base("ZoomState", stateMachine)
    {
        _sm = stateMachine;
    }

    public ZoomState(string name, PlayerSM stateMachine) : base(name, stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter(GameObject zoomVirtualCamera, GameObject playerFollowCamera, GameObject targetGO, float zoomInPercentage)
    {
        _sm.PlayerFollowCamera.SetActive(false);
        zoomVirtualCamera.SetActive(true);

        Vector3 targetCenter = targetGO.GetComponent<Collider>().bounds.center;
        zoomVirtualCamera.transform.position = Vector3.LerpUnclamped(playerFollowCamera.transform.position, targetCenter, zoomInPercentage);
        //zoomVirtualCamera.transform.LookAt(targetGO.transform.position);
        zoomVirtualCamera.transform.LookAt(targetCenter);
        ZoomVirtualCamera = zoomVirtualCamera;
    }

    public override void Exit()
    {
        _sm.PlayerFollowCamera.SetActive(true);
        ZoomVirtualCamera.SetActive(false);
    }

    public override void UpdateLogic()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _sm.ChangeState(_sm.defaultState);
    }

    public override void CameraRotation(StarterAssetsInputs input, float rotationSpeed, float deltaTimeMultiplier, float bottomClamp, float topClamp, GameObject cinemachineCameraTarget, Transform playerTransform)
    {

    }
}
