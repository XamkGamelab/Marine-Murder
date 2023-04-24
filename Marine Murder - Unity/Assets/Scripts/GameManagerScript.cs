using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject demoCompletePanel;
    [SerializeField] private float panelDisappearTimer;

    public void OnMicroscopeFixed()
    {
        demoCompletePanel.SetActive(true);
        StartCoroutine(DisablePanel());
    }

    private IEnumerator DisablePanel()
    {
        yield return new WaitForSeconds(panelDisappearTimer);
        demoCompletePanel.SetActive(false);
    }
    // viewing code lock stuff
    //[SerializeField] private GameObject characterCamera;
    //[SerializeField] private FirstPersonController firstPersonController;
    //[SerializeField] private PlayerSM playerSM;


    //private bool isFocused = false;
    //private GameObject _targetCamera;
    //private BoxCollider _interactionCollider;
    //private float _oldTargetPitch;

    //public void ExitFocus()
    //{
    //    if (isFocused)
    //        ToggleObjectFocus(_targetCamera, _interactionCollider, _oldTargetPitch, false);
    //}

    //public void ToggleObjectFocus(GameObject targetCamera, BoxCollider interactionCollider, float targetPitch, bool startFocus)
    //{
    //    _targetCamera = targetCamera;
    //    _interactionCollider = interactionCollider;
    //    _oldTargetPitch = firstPersonController.cinemachineTargetPitch;

    //    //characterCamera.SetActive(!characterCamera.activeSelf);
    //    //targetCamera.SetActive(!targetCamera.activeSelf);
    //    interactionCollider.enabled = !interactionCollider.enabled;
    //    firstPersonController.cinemachineTargetPitch = targetPitch;

    //    if (startFocus)
    //        //firstPersonController.playerState = PlayerState.codeLock;
    //        playerSM.ChangeState(playerSM.codelockZoomState, targetCamera, interactionCollider.gameObject);
    //    else
    //        //firstPersonController.playerState = PlayerState.normal;
    //        playerSM.ChangeState(playerSM.defaultState);

    //    isFocused = !isFocused;
    //}
}
