using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour, IInteract
{
    [SerializeField] private int buttonNumber;

    private float buttonMoveAmount, buttonMoveTime;

    void Start()
    {
        buttonMoveAmount = GetComponentInParent<CodeLockScript>().ButtonMoveAmount;
        buttonMoveTime = GetComponentInParent<CodeLockScript>().ButtonMoveTime;
    }

    public void Interact()
    {
        Debug.Log("Interacted with button " + buttonNumber);
        this.transform.GetComponentInParent<CodeLockScript>().EnterNumber(buttonNumber);
        StartCoroutine(ButtonAnimation());
    }

    private IEnumerator ButtonAnimation()
    {
        float originalZ = transform.localPosition.z;
        float targetZ = originalZ + buttonMoveAmount;

        // moves the button back
        while (transform.localPosition.z < targetZ)
        {
            transform.Translate(Vector3.forward * buttonMoveAmount * Time.deltaTime / buttonMoveTime);
            yield return null;
        }
        // moves the button forward
        while (transform.localPosition.z > originalZ)
        {
            transform.Translate(Vector3.back * buttonMoveAmount * Time.deltaTime / buttonMoveTime );
            yield return null;
        }
        // make sure the button ends in the same position it started at
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, originalZ);
    }
}
