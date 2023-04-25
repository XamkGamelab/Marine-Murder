using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour, IInteract
{
    [Header("Don't change")]
    [SerializeField] private PlayerSM playerSM;
    [SerializeField] private GameObject virtualCamera;

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
        playerSM.ChangeState(playerSM.cameraZoomState, virtualCamera);
    }

    public bool HasExamine()
    {
        return true;
    }
}
