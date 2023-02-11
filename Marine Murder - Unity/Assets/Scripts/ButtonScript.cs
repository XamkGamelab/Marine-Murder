using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour, IInteract
{
    [SerializeField]
    private int buttonNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Interacted with button " + buttonNumber);
        this.transform.GetComponentInParent<CodeLockScript>().EnterNumber(buttonNumber);
    }
}
