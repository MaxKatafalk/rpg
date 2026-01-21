using Assets.Scripts.Core;
using UnityEngine;

public class KeyItem : Item
{
    public int doorId = 1;

    public int GetDoorId()
    {
        return doorId;
    }

    public void SetDoorId(int id)
    {
        doorId = id;
    }

    public override void Use(Player player)
    {
        Debug.Log("Ключ использован для двери ID: " + doorId);
    }
}