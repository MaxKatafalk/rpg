using Assets.Scripts.Core;
using UnityEngine;

public class Chest : InteractableObject
{
    public bool isOpen = false;
    public Item containedItem;

    public override void Interact(Player player)
    {
        if (!CanInteract() || isOpen) return;

        OpenChest(player);
    }

    private void OpenChest(Player player)
    {
        isOpen = true;
        isInteractable = false;

        if (containedItem != null)
        {
            player.AddItem(containedItem);
            Debug.Log("Вы открыли сундук и нашли: " + containedItem.GetName());
        }
        else
        {
            Debug.Log("Сундук пуст");
        }
    }
}