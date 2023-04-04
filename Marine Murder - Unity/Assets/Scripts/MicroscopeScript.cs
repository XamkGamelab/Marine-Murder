using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroscopeScript : MonoBehaviour, IInteract
{
    [SerializeField] private string floorInteractionText;
    [SerializeField] private string tableInteractionText;
    [SerializeField] private Transform microscopePlace;

    private bool onFloor = true;

    public string GetText()
    {
        if (onFloor)
            return floorInteractionText;
        else
            return tableInteractionText;
    }

    public void Interact()
    {
        if (onFloor)
        {
            Debug.Log("Interacted with microscope");
            gameObject.transform.position = microscopePlace.position;
            gameObject.transform.rotation = microscopePlace.rotation;
            gameObject.transform.Rotate(new Vector3(0, 0, 180), Space.Self);

            onFloor = false;
        }
        else
        {

        }
    }
}
