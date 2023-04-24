using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDoorScript : MonoBehaviour, IInteract
{
    [SerializeField] private string examineText1;
    [SerializeField] private string examineText2;

    [SerializeField] private float targetRotationY;
    [SerializeField] private float moveTime;
    [SerializeField] private float closedRotationY;
    [SerializeField] private float openRotationY;

    private bool helper;
    public bool doorLocked = true;
    public bool doorClosed = true;

    public string GetExamineText()
    {
        if (helper)
        {
            helper = !helper;
            return examineText1;
        }
        else
        {
            helper = !helper;
            return examineText2;
        }
    }

    public string GetInteractText()
    {
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
