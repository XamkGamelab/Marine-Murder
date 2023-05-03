using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectiveBoxZoomState : BaseState
{
    private PlayerSM _sm;
    private GameObject _zoomVirtualCamera;
    private bool _key;

    public ObjectiveBoxZoomState(PlayerSM stateMachine) : base("ObjectiveBoxZoomState", stateMachine)
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

        //_sm.inventoryPanel.SetActive(false);
        //_sm.puzzleButtonsParent.SetActive(true);
        _sm.PlayerFollowCamera.SetActive(false);
        zoomVirtualCamera.SetActive(true);
        zoomVirtualCamera.GetComponentInParent<Collider>().enabled = false;

        _zoomVirtualCamera = zoomVirtualCamera;
        //_sm.puzzleScript.StartPuzzle();
        LayerMask = 1 << 6;
    }

    public override void Exit()
    {
        _sm.firstPersonInputModule.enabled = true;
        _sm.inputSystemUIInputModule.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        _sm.crosshair.SetActive(true);

        //_sm.inventoryPanel.SetActive(true);
        //_sm.puzzleButtonsParent.SetActive(false);
        _sm.PlayerFollowCamera.SetActive(true);
        _zoomVirtualCamera.SetActive(false);
        _zoomVirtualCamera.GetComponentInParent<Collider>().enabled = true;
    }

    public override void UpdateLogic()
    {
        RaycastCheck();


        if (Input.GetKeyDown(KeyCode.Escape))
            ExitState();

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

    //public override void OnInteract(Camera mainCamera, DialogueScript dialogueScript) { }

    private void ExitState()
    {
        _sm.leftGradient.color = new Color(1, 1, 1, 0);
        _sm.rightGradient.color = new Color(1, 1, 1, 0);
        _sm.topGradient.color = new Color(1, 1, 1, 0);
        _sm.bottomGradient.color = new Color(1, 1, 1, 0);

        _sm.ChangeState(_sm.defaultState);
    }

    private void RaycastCheck()
    {
        RaycastHit hit;
        Ray forwardRay = _sm.MainCamera.ScreenPointToRay(Input.mousePosition);
        //int layerMask = 1 << 6;

        // if hit something on interactables layer
        if (Physics.Raycast(forwardRay, out hit, _sm.RaycastDistance, LayerMask))
        {
            if (hit.transform.gameObject.TryGetComponent<IDirectInteract>(out IDirectInteract interaction))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    interaction.Interact(_sm);

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
