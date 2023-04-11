using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public List<ItemSO> items;

    [SerializeField] private InventoryViewScript inventoryView;

    public void AddItem(ItemSO item)
    {
        items.Add(item);
        inventoryView.UpdateInvetoryView();
    }

    public void RemoveItem(ItemSO item)
    {
        items.Remove(item);
        inventoryView.UpdateInvetoryView();
    }
}
