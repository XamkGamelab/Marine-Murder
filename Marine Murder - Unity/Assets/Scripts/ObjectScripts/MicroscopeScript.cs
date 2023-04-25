using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroscopeScript : MonoBehaviour, IInteract
{
    [SerializeField] private GameEventSO interactEventSO;
    [SerializeField] private GameEventSO microscopeFixedEvent;
    [SerializeField] private EventCheckSO microscopeOnTable;
    [SerializeField] private EventCheckSO objectivePicked;
    [SerializeField] private InventoryScript inventory;
    [SerializeField] private ItemSO correctItemSO;

    public PlayerSM playerSM;
    public GameObject puzzleCamera;

    // Microscope on floor
    [SerializeField] private string floorInteractText;
    [SerializeField] private string floorExamineText;

    // Microscope on table
    [SerializeField] private string fixedInteractText;
    [SerializeField] private string tableBrokenExamineText;
    [SerializeField] private string tableFixedExamineText;

    [SerializeField] private string noObjectiveText;
    [SerializeField] private string correctObjectiveText;
    [SerializeField] private string wrongObjectiveText;

    [SerializeField] private Transform microscopePlace;

    private bool onFloor = true;
    private bool broken = true;
    private bool puzzleSolved = false;

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
        else if (broken)
        {
            // player has picked some objective
            if (objectivePicked.eventHasHappened)
            {
                if (inventory.items.Contains(correctItemSO))
                    return correctObjectiveText;
                else
                    return wrongObjectiveText;
            }
            // player has not picked any objective
            else
            {
                return noObjectiveText;
            }
        }
        // microscope is fixed
        else
            return fixedInteractText;
    }

    public bool HasInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (onFloor)
        {
            gameObject.transform.position = microscopePlace.position;
            gameObject.transform.rotation = microscopePlace.rotation;

            interactEventSO.Raise();
            microscopeOnTable.eventHasHappened = true;
            onFloor = false;
        }
        else if (broken)
        {
            // player has correct item
            if (inventory.items.Contains(correctItemSO))
            {
                interactEventSO.Raise();
                microscopeFixedEvent.Raise();

                broken = false;
                inventory.RemoveItem(correctItemSO);
            }
            else
                interactEventSO.Raise();
        }
        else
        {
            if (!puzzleSolved)
                playerSM.ChangeState(playerSM.microscopePuzzleState, puzzleCamera);
        }
    }

    public void Examine()
    {
        if (onFloor)
            playerSM.InteractExamineText.text = floorExamineText;
        else if (broken)
            playerSM.InteractExamineText.text = tableBrokenExamineText;
        else
            playerSM.InteractExamineText.text = tableFixedExamineText;

        playerSM.InteractExamineTextPanel.SetActive(true);
        playerSM.ChangeState(playerSM.zoomState, playerSM.ZoomVirtualCamera, playerSM.PlayerFollowCamera, this.gameObject, playerSM.ZoomInPercentage);
    }

    public bool HasExamine()
    {
        return true;
    }

    public void OnPuzzleSolved()
    {
        puzzleSolved = true;
    }
}
