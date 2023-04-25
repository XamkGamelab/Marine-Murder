using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class DefaultState : BaseState
{
    private PlayerSM _sm;

    private bool _key;
    private float _pitch;

    public DefaultState(PlayerSM stateMachine) : base("DefaultState", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        _sm.InteractExamineTextPanel.SetActive(false);
        //_sm.HighlightPanel.SetActive(true);

        // dialogue layer
        LayerMask = 1 << 7;
    }

    public override void Exit()
    {
        //_sm.HighlightPanel.SetActive(false);
        ResetTarget();
    }

    public override void UpdateLogic()
    {
        RaycastCheck();

        if (Input.GetKeyDown(KeyCode.Mouse0) && _sm.InteractExamineTextPanel.activeSelf)
            _sm.InteractExamineTextPanel.SetActive(false);
    }

    public override void CameraRotation(StarterAssetsInputs input, float rotationSpeed, float deltaTimeMultiplier, float bottomClamp, float topClamp, GameObject cinemachineCameraTarget, Transform playerTransform)
    {
        _pitch += input.look.y * rotationSpeed * deltaTimeMultiplier;
        float velocity = input.look.x * rotationSpeed * deltaTimeMultiplier;

        _pitch = ClampAngle(_pitch, bottomClamp, topClamp);

        // Update Cinemachine camera target pitch
        cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_pitch, 0.0f, 0.0f);

        // rotate the player left and right
        playerTransform.Rotate(Vector3.up * velocity);
    }

    public override void OnInteract(Camera mainCamera, DialogueScript dialogueScript)
    {
        RaycastHit hit;
        // Does the ray intersect any objects in the "Dialogue" layer
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, LayerMask))
        {
            hit.transform.gameObject.GetComponent<IDirectInteract>().Interact(_sm);
        }
    }

    private void RaycastCheck()
    {
        RaycastHit hit;
        Ray forwardRay = _sm.MainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        int layerMask1 = 1 << 5;
        int layerMask2 = 1 << 6;
        int layerMask = layerMask1 | layerMask2;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Set up the new Pointer Event
        _sm.PointerEventData = new PointerEventData(_sm.EventSystem);
        //Set the Pointer Event Position to that of the game object
        _sm.PointerEventData.position = Input.mousePosition;
        //Raycast using the Graphics Raycaster and mouse click position
        _sm.Raycaster.Raycast(_sm.PointerEventData, results);

        // if hit something on interactables layer
        if (Physics.Raycast(forwardRay, out hit, _sm.RaycastDistance, layerMask2))
        {
            // if hit something with IInteract component
            if (hit.transform.gameObject.TryGetComponent<IInteract>(out IInteract interaction))
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

                        _sm.HighlightPanel.SetActive(true);

                        if (interaction.HasInteract())
                        {
                            _sm.InteractButton.gameObject.SetActive(true);
                            _sm.InteractButton.onClick.AddListener(_sm.InteractResponse);
                            _sm.InteractButton.onClick.AddListener(interaction.Interact);
                        }
                        else
                            _sm.InteractButton.gameObject.SetActive(false);
                        if (interaction.HasExamine())
                        {
                            _sm.ExamineButton.gameObject.SetActive(true);
                            //_sm.ExamineButton.onClick.AddListener(this.ExamineResponse);
                            _sm.ExamineButton.onClick.AddListener(interaction.Examine);
                        }
                        else
                            _sm.ExamineButton.gameObject.SetActive(false);
                    }
                }
                else
                    ResetTarget();

                HighlighPanelPositionUpdate();
            }
        }
        // player is hovering over the highlight panel so update it's position
        else if (results.Count > 0)
        {
            if (Target)
                HighlighPanelPositionUpdate();
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
        _sm.HighlightPanel.SetActive(false);
        _key = false;

        _sm.InteractButton.onClick.RemoveAllListeners();
        _sm.ExamineButton.onClick.RemoveAllListeners();
    }

    private void HighlighPanelPositionUpdate()
    {
        // update the position on highlight panel if it's active
        if (_sm.HighlightPanel.activeSelf)
        {
            Vector3 point = _sm.MainCamera.WorldToScreenPoint(Target.transform.position);
            _sm.HighlightPanel.transform.position = point;

        }
    }

    private void ExamineResponse()
    {
        if (Target.TryGetComponent<IInteract>(out IInteract interaction))
        {
            //_sm.HighlightPanel.SetActive(false);
            _sm.InteractExamineText.text = interaction.GetExamineText();
            if(_sm.InteractExamineText.text != null)
                _sm.InteractExamineTextPanel.SetActive(true);
            _sm.ChangeState(_sm.zoomState, _sm.ZoomVirtualCamera, _sm.PlayerFollowCamera, Target, _sm.ZoomInPercentage);
            //playerSM.ChangeState(playerSM.zoomState, zoomVirtualCamera, playerFollowCamera, target, zoomInPercentage);
        }
    }
}
