using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseScript : MonoBehaviour, IInteract
{
    [SerializeField] private PlayerSM playerSM;
    [SerializeField] private ItemSO IDCardItemSO;
    [SerializeField] private InventoryScript inventory;

    [SerializeField] private string examineText;

    private bool hasID = true;

    public string GetExamineText()
    {
        return examineText;
    }

    public string GetInteractText()
    {
        return null;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();

        //if (hasID)
        //{
        //    inventory.AddItem(IDCardItemSO);
        //    hasID = false;
        //}
    }

    public bool HasInteract()
    {
        return false;
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

    public bool HasInteract()
    {
        return true;
    }
}
