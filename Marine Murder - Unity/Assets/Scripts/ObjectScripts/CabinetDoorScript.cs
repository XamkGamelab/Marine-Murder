using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDoorScript : MonoBehaviour, IInteract
{
    [Header("Can change")]
    [SerializeField] private string examineText;
    [SerializeField] private string lockedInteractText;

    [SerializeField] private float targetRotationY;
    [SerializeField] private float moveTime;
    [SerializeField] private float closedRotationY;
    [SerializeField] private float openRotationY;
    [Space(10)]
    [Header("Don't change")]
    [SerializeField] private PlayerSM playerSM;

    private bool helper;
    [HideInInspector] public bool doorLocked = true;
    [HideInInspector] public bool doorClosed = true;

    public string GetExamineText()
    {
        //if (helper)
        //{
        //    helper = !helper;
        //    return examineText1;
        //}
        //else
        //{
        //    helper = !helper;
        //    return examineText2;
        //}
        return null;
    }

    public string GetInteractText()
    {
        if (doorLocked)
            return lockedInteractText;
        else
            return null;
    }

    public void Interact()
    {
        if (!doorLocked)
        {
            if (doorClosed)
                StartCoroutine(OpenCloseDoor(openRotationY));
            else
                StartCoroutine(OpenCloseDoor(closedRotationY));
        }
    }

    public void Examine()
    {
        if (doorLocked)
            playerSM.InteractExamineText.text = examineText;

        playerSM.InteractExamineTextPanel.SetActive(true);
        playerSM.ChangeState(playerSM.zoomState, playerSM.ZoomVirtualCamera, playerSM.PlayerFollowCamera, this.gameObject, playerSM.ZoomInPercentage);
    }

    public bool HasExamine()
    {
        if (doorLocked)
            return true;
        else
            return false;
    }

    public void OnLockUnlocked()
    {
        StartCoroutine(UnlockDoor());
    }

    IEnumerator UnlockDoor()
    {
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, targetRotationY, 0);
        float timeCount = 0f;

        while (transform.localRotation.eulerAngles.y < targetRotationY)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, timeCount * (1 / moveTime));
            timeCount += Time.deltaTime;
            yield return null;
        }

        doorLocked = false;
    }

    IEnumerator OpenCloseDoor(float targetRotationY)
    {
        Quaternion targetRotation = Quaternion.Euler(0, targetRotationY, 0);
        Quaternion startRotation = transform.localRotation;
        float timeCount = 0f;

        while (Mathf.Abs(targetRotationY - transform.localRotation.eulerAngles.y) > 0.1f)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, timeCount * (1 / moveTime));
            timeCount += Time.deltaTime;
            yield return null;
        }
        doorClosed = !doorClosed;
    }

    public bool HasInteract()
    {
        return true;
    }
}
