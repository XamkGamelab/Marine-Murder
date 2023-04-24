/* Original code by Mina Pêcheux
 * https://medium.com/c-sharp-progarmming/make-a-basic-fsm-in-unity-c-f7d9db965134
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class BaseState
{
    public string name;
    protected StateMachine stateMachine;

    public GameObject Target { get; protected set; }
    protected int LayerMask;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Enter(GameObject zoomVirtualCamera) { }
    public virtual void Enter(GameObject zoomVirtualCamera, GameObject targetGO) { }
    public virtual void Enter(GameObject zoomVirtualCamera, GameObject playerFollowCamera, GameObject targetGO, float zoomInPercentage) { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }

    public virtual void CameraRotation(StarterAssetsInputs input, float rotationSpeed, float deltaTimeMultiplier, float bottomClamp, float topClamp, GameObject cinemachineCameraTarget, Transform playerTransform) { }
    public virtual void OnInteract(Camera mainCamera, DialogueScript dialogueScript)
    {
        RaycastHit hit;
        // Does the ray intersect any objects in the "Interactables" layer
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask))
        {
            hit.transform.gameObject.GetComponent<IInteract>().Interact();
        }
    }

    public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}