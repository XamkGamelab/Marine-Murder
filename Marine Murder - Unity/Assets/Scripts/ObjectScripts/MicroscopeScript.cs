using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroscopeScript : MonoBehaviour, IInteract
{
    [SerializeField] private GameEventSO interactEventSO;
    [SerializeField] private EventCheckSO microscopeOnTable;

    [SerializeField] private string floorInteractText;
    [SerializeField] private string tableInteractText;

    [SerializeField] private string floorExamineText;
    [SerializeField] private string tableBrokenExamineText;
    [SerializeField] private string tableFixedExamineText;

    [SerializeField] private Transform microscopePlace;

    private bool onFloor = true;
    private bool broken = true;

    public string GetExamineText()
    {
        if (onFloor)
            return floorExamineText;
        else if (broken)
            return tableBrokenExamineText;
        else
            return tableFixedExamineText;
    }

    public string GetInteractText()
    {
        if (onFloor)
            return floorInteractText;
        else
            return tableInteractText;
    }

    public void Interact()
    {
        if (onFloor)
        {
            Debug.Log("Interacted with microscope");
            gameObject.transform.position = microscopePlace.position;
            gameObject.transform.rotation = microscopePlace.rotation;

            interactEventSO.Raise();
            microscopeOnTable.eventHasHappened = true;
            onFloor = false;
        }
        else
        {
            // begin the microscope puzzle here
            throw new System.NotImplementedException();
        }
    }
}
