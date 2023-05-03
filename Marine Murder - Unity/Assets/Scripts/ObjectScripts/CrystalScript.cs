using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour, IInteract
{
    [Header("Can change")]
    [SerializeField] private string examineText;
    [SerializeField] private string interactText;
    [Space(10)]
    [Header("Don't change")]
    [SerializeField] private PlayerSM playerSM;
    [SerializeField] private ItemSO crystalSO;
    [SerializeField] private InventoryScript inventory;
    [SerializeField] private GameEventSO interactEvent;

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

    public void Examine()
    {
        playerSM.InteractExamineText.text = examineText;
        playerSM.InteractExamineTextPanel.SetActive(true);
        playerSM.ChangeState(playerSM.zoomState, playerSM.ZoomVirtualCamera, playerSM.PlayerFollowCamera, this.gameObject, playerSM.ZoomInPercentage);
    }

    public bool HasInteract()
    {
        return true;
    }

    public bool HasExamine()
    {
        return true;
    }
}
