using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarScript : MonoBehaviour, IInteract
{
    public string GetExamineText()
    {
        return null;
    }

    public string GetInteractText()
    {
        throw new System.NotImplementedException();
    }

    public bool HasInteract()
    {
        return false;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
