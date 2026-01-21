using Assets.Scripts.Core;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string itemName = "Предмет";
    private string description = "Описание";

    public string GetName()
    {
        return itemName;
    }

    public string GetDescription()
    {
        return description;
    }

    public virtual void Use(Player player)
    {
        Debug.Log("Предмет использован: " + itemName);
    }
}