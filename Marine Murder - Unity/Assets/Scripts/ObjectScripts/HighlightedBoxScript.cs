using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightedBoxScript : MonoBehaviour, IDirectInteract
{
    [SerializeField] private GameObject tableBox;
    [SerializeField] private ItemSO boxSO;
    [SerializeField] private InventoryScript inventory;

    public void Interact(PlayerSM playerSM)
    {
        inventory.RemoveItem(boxSO);
        Destroy(this.gameObject);
        tableBox.SetActive(true);
    }
}
