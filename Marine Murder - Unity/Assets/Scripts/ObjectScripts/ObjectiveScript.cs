using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveScript : MonoBehaviour, IDirectInteract
{
    [Header("Don't change")]
    public ItemSO ObjectiveSO;

    [SerializeField] private InventoryScript inventory;
    [SerializeField] private GameObject parentGO;
    [SerializeField] private EventCheckSO objectivePicked;

    private bool microscopeNotFixed = true;

    public void Interact(PlayerSM playerSM)
    {
        if (microscopeNotFixed)
        {
            foreach (Transform child in parentGO.transform)
            {
                child.gameObject.SetActive(true);
                inventory.RemoveItem(child.gameObject.GetComponent<ObjectiveScript>().ObjectiveSO);
            }

            inventory.AddItem(ObjectiveSO);
            objectivePicked.eventHasHappened = true;
            this.gameObject.SetActive(false);
        }
    }

    public void MicroscopeFixedResponse()
    {
        microscopeNotFixed = false;
    }
}
