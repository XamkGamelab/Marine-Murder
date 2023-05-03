using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDoorScript : MonoBehaviour, IInteract
{
    [Header("Can change")]
    [SerializeField] private string examineText;
    [SerializeField] private string lockedInteractText;

    [SerializeField] private float targetRotationZ;
    [SerializeField] private float moveTime;
    [SerializeField] private float closedRotationZ;
    [SerializeField] private float openRotationZ;
    [Space(10)]
    [Header("Don't change")]
    [SerializeField] private PlayerSM playerSM;

    private bool helper;
    [HideInInspector] public bool doorLocked = true;
    [HideInInspector] public bool doorClosed = true;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
                StartCoroutine(OpenCloseDoor(openRotationZ));
            else
                StartCoroutine(OpenCloseDoor(closedRotationZ));
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
        Quaternion targetRotation = Quaternion.Euler(-90, 0, targetRotationZ);
        float timeCount = 0f;

        while (timeCount < moveTime)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, timeCount * (1 / moveTime));
            timeCount += Time.deltaTime;
            yield return null;
        }
        doorLocked = false;
    }

    IEnumerator OpenCloseDoor(float targetRotationY)
    {
        audioSource.Play();

        Quaternion targetRotation = Quaternion.Euler(-90, 0, targetRotationY);
        Quaternion startRotation = transform.localRotation;
        float timeCount = 0f;

        //while (Mathf.Abs(targetRotationY - transform.localRotation.eulerAngles.y) > 0.1f)
        while(timeCount < moveTime)
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
