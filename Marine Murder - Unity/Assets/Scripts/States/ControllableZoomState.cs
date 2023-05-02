using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ControllableZoomState : ZoomState
{
    private GameObject _targetGO;
    protected bool _key;

    protected Quaternion _startingRotation;

    public ControllableZoomState(PlayerSM stateMachine) : base("CodelockZoomState", stateMachine)
    {
        _sm = stateMachine;
    }

    public ControllableZoomState(string name, PlayerSM stateMachine) : base(name, stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter(GameObject zoomVirtualCamera, GameObject targetGO)
    {
        //Vector3 direction = targetGO.transform.position - zoomVirtualCamera.transform.position;
        Vector3 direction = targetGO.GetComponent<Collider>().bounds.center - zoomVirtualCamera.transform.position;
        _startingRotation = Quaternion.LookRotation(direction, zoomVirtualCamera.transform.up);
        zoomVirtualCamera.transform.rotation = _startingRotation;

        _sm.PlayerFollowCamera.SetActive(false);
        zoomVirtualCamera.SetActive(true);
        ZoomVirtualCamera = zoomVirtualCamera;

        // interactables layer
        //int layer1 = 1 << 6;
        //int layer2 = 1 << 7;
        //LayerMask = layer1 | layer2;
        LayerMask = 1 << 6;

        _targetGO = targetGO;
        targetGO.GetComponent<Collider>().enabled = false;
    }

    public override void Exit()
    {
        base.Exit();

        _targetGO.GetComponent<Collider>().enabled = true;
        ZoomVirtualCamera.transform.rotation = _startingRotation;
    }

    public override void CameraRotation(StarterAssetsInputs input, float rotationSpeed, float deltaTimeMultiplier, float bottomClamp, float topClamp, GameObject cinemachineCameraTarget, Transform playerTransform)
    {
        Quaternion quat1 = Quaternion.AngleAxis(input.look.y * rotationSpeed * deltaTimeMultiplier, Vector3.right);
        Quaternion quat2 = Quaternion.AngleAxis(input.look.x * rotationSpeed * deltaTimeMultiplier, Vector3.up);
        ZoomVirtualCamera.transform.localRotation *= quat1 * quat2;

        if (Quaternion.Angle(_startingRotation, ZoomVirtualCamera.transform.rotation) > 15)
            ZoomVirtualCamera.transform.rotation *= Quaternion.Inverse(quat1) * Quaternion.Inverse(quat2);
    }

    public override void UpdateLogic()
    {
        RaycastCheck();

        if (Input.GetKeyDown(KeyCode.Mouse1))
            _sm.ChangeState(_sm.defaultState);
    }

    public override void OnInteract(Camera mainCamera, DialogueScript dialogueScript)
    {
        RaycastHit hit;
        // Does the ray intersect any objects in the "Dialogue" layer
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask))
        {
            if (hit.transform.gameObject.TryGetComponent<IDirectInteract>(out IDirectInteract dInteract))
                dInteract.Interact(_sm);
            else if (hit.transform.gameObject.TryGetComponent<IInteract>(out IInteract interact))
                interact.Interact();

        }
    }

    private void RaycastCheck()
    {
        RaycastHit hit;
        Ray forwardRay = _sm.MainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        //int layerMask = 1 << 6;

        // if hit something on interactables layer
        if (Physics.Raycast(forwardRay, out hit, _sm.RaycastDistance, LayerMask))
        {
                // hit object has Outline script component
                if (hit.transform.gameObject.TryGetComponent<Outline>(out Outline outline))
                {
                    // no target -> set target to hit object & activate highlight panel
                    if (Target == null || Target != hit.transform.gameObject)
                    {
                        ResetTarget();

                        Target = hit.transform.gameObject;
                        outline.enabled = true;

                        _key = true;
                    }
                }
                else
                    ResetTarget();
        }

        // did not hit anything on interactables layer and key is true
        else if (_key == true)
            ResetTarget();
    }

    private void ResetTarget()
    {
        if (Target != null)
            Target.GetComponent<Outline>().enabled = false;

        Target = null;
        _key = false;
    }
}
