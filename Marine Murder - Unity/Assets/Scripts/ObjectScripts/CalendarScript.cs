using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarScript : MonoBehaviour, IInteract
{
    [SerializeField] private PlayerSM playerSM;
    public string GetExamineText()
    {
        return null;
    }

    public string GetInteractText()
    {
        throw new System.NotImplementedException();
    }

    public bool HasInteract()
    {
        return false;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void Examine()
    {
        playerSM.ChangeState(playerSM.zoomState, playerSM.ZoomVirtualCamera, playerSM.PlayerFollowCamera, this.gameObject, playerSM.ZoomInPercentage);
    }

    public bool HasExamine()
    {
        return true;
    }
}
