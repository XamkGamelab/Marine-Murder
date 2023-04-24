using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public List<ItemSO> items;

    [SerializeField] private InventoryViewScript inventoryView;
    [SerializeField] AudioSource audioSource;

    public void AddItem(ItemSO item)
    {
        items.Add(item);
        inventoryView.UpdateInvetoryView();
        audioSource.Play();
    }

    public void RemoveItem(ItemSO item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            inventoryView.UpdateInvetoryView();
        }
    }
}
