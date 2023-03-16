using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // viewing code lock stuff
    [SerializeField] private GameObject characterCamera;
    [SerializeField] private FirstPersonController firstPersonController;


    private bool isFocused = false;
    private GameObject _targetCamera;
    private BoxCollider _interactionCollider;
    private float _oldTargetPitch;

    public void ExitFocus()
    {
        if (isFocused)
            ToggleObjectFocus(_targetCamera, _interactionCollider, _oldTargetPitch, false);
    }

    public void ToggleObjectFocus(GameObject targetCamera, BoxCollider interactionCollider, float targetPitch, bool startFocus)
    {
        _targetCamera = targetCamera;
        _interactionCollider = interactionCollider;
        _oldTargetPitch = firstPersonController.cinemachineTargetPitch;

        characterCamera.SetActive(!characterCamera.activeSelf);
        targetCamera.SetActive(!targetCamera.activeSelf);
        interactionCollider.enabled = !interactionCollider.enabled;
        firstPersonController.cinemachineTargetPitch = targetPitch;

        if (startFocus)
            firstPersonController.playerState = PlayerState.codeLock;
        else
            firstPersonController.playerState = PlayerState.normal;

        isFocused = !isFocused;
    }
}
