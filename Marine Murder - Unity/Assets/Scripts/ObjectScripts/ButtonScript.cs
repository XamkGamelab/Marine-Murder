using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour, IInteract
{
    [SerializeField] private int buttonNumber;

    private float buttonMoveAmount, buttonMoveTime;
    private bool animating = false;

    void Start()
    {
        buttonMoveAmount = GetComponentInParent<CodeLockScript>().ButtonMoveAmount;
        buttonMoveTime = GetComponentInParent<CodeLockScript>().ButtonMoveTime;
    }

    public void Interact()
    {
        this.transform.GetComponentInParent<CodeLockScript>().EnterNumber(buttonNumber);
        if (!animating)
            StartCoroutine(ButtonAnimation());
    }

    private IEnumerator ButtonAnimation()
    {
        float originalZ = transform.localPosition.z;
        float targetZ = originalZ - buttonMoveAmount;
        animating = true;

        // moves the button back
        while (transform.localPosition.z > targetZ)
        {
            //transform.Translate(Vector3.forward * buttonMoveAmount * Time.deltaTime / buttonMoveTime);
            //yield return null;
            transform.Translate(Vector3.back * buttonMoveAmount * Time.deltaTime / buttonMoveTime);
            yield return null;
        }
        // moves the button forward
        while (transform.localPosition.z < originalZ)
        {
            //transform.Translate(Vector3.back * buttonMoveAmount * Time.deltaTime / buttonMoveTime );
            //yield return null;
            transform.Translate(Vector3.forward * buttonMoveAmount * Time.deltaTime / buttonMoveTime);
            yield return null;
        }
        // make sure the button ends in the same position it started at
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, originalZ);
        animating = false;
    }

    public string GetExamineText()
    {
        throw new System.NotImplementedException();
    }

    public string GetInteractText()
    {
        throw new System.NotImplementedException();
    }

    public bool HasInteract()
    {
        return true;
    }

    public void Examine() { }

    public bool HasExamine()
    {
        return true;
    }
}
