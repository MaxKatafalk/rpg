using Assets.Scripts.Core;
using UnityEngine;

public class Trap : InteractableObject
{
    public int damageAmount = 20;
    public bool isActive = true;

    public override void Interact(Player player)
    {
        if (!CanInteract() || !isActive) return;

        player.TakeDamage(damageAmount);
        Debug.Log("Ловушка активирована! Урон: " + damageAmount);
        isActive = false;
        isInteractable = false;
    }
}