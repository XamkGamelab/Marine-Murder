using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour, IInteract
{
    [SerializeField] private ItemSO crystalSO;
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

    public void Interact()
    {
        inventory.AddItem(crystalSO);
        interactEvent.Raise();
        Destroy(this.gameObject);
    }

    public bool HasInteract()
    {
        return true;
    }
}
