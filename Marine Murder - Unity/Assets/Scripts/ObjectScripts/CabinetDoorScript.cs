using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDoorScript : MonoBehaviour
{
    [SerializeField] private float targetRotationY;
    [SerializeField] private float moveTime;

    public void OnLockUnlocked()
    {
        StartCoroutine(UnlockDoor());
    }

    IEnumerator UnlockDoor()
    {
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, targetRotationY, 0);
        float timeCount = 0f;

        while(transform.localRotation.eulerAngles.y < targetRotationY)
        {
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, timeCount*(1/moveTime));
            timeCount += Time.deltaTime;
            yield return null;
        }
    }
}
