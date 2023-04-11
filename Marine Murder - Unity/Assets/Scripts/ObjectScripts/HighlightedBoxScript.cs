using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightedBoxScript : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject tableBox;
    [SerializeField] private ItemSO boxSO;
    [SerializeField] private InventoryScript inventory;

    public string GetExamineText()
    {
        throw new System.NotImplementedException();
    }

    public string GetInteractText()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        inventory.RemoveItem(boxSO);
        Destroy(this.gameObject);
        tableBox.SetActive(true);
    }
}
