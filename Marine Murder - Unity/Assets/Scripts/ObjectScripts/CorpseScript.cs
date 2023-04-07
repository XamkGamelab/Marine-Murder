using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseScript : MonoBehaviour, IInteract
{
    [SerializeField] private InventoryScript inventory;
    [SerializeField] private string examineText;
    [SerializeField] private ItemSO IDCardItemSO;

    private bool hasID = true;

    public string GetExamineText()
    {
        return examineText;
    }

    public string GetInteractText()
    {
        return "";
    }

    public void Interact()
    {
        if (hasID)
        {
            inventory.AddItem(IDCardItemSO);
            hasID = false;
        }
    }
}
