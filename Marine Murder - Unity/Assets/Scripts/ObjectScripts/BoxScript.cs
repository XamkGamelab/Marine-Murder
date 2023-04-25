using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour, IInteract
{
    [SerializeField] private PlayerSM playerSM;
    [SerializeField] private GameObject tableSpot;
    [SerializeField] private ItemSO boxSO;
    [SerializeField] private InventoryScript inventory;
    [SerializeField] private GameEventSO interactEvent;

    [SerializeField] private string examineText;
    [SerializeField] private string interactText;

    public string GetExamineText()
    {
        return examineText;
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public bool HasInteract()
    {
        return true;
    }

    public void Interact()
    {
        inventory.AddItem(boxSO);
        interactEvent.Raise();
        Destroy(this.gameObject);
        tableSpot.SetActive(true);
    }

    public void Examine()
    {
        playerSM.InteractExamineText.text = examineText;
        playerSM.InteractExamineTextPanel.SetActive(true);
        playerSM.ChangeState(playerSM.zoomState, playerSM.ZoomVirtualCamera, playerSM.PlayerFollowCamera, this.gameObject, playerSM.ZoomInPercentage);   
    }

    public bool HasExamine()
    {
        return true;
    }
}
