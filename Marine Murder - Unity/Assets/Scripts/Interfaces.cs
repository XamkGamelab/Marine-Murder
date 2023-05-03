using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteract
{
    public bool HasInteract();
    public void Interact();
    public string GetInteractText();
    public bool HasExamine();
    public void Examine();
    public string GetExamineText();
}

public interface IDirectInteract
{
    public void Interact(PlayerSM playerSM);
}
