using Assets.Scripts.Core;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public string objectName = "Объект";
    public string description = "Описание";
    public bool isInteractable = true;

    public abstract void Interact(Player player);

    public virtual bool CanInteract()
    {
        return isInteractable;
    }
}