using Assets.Scripts.Core;
using UnityEngine;

public class PotionItem : Item
{
    public int healAmount = 30;

    public int GetHealAmount()
    {
        return healAmount;
    }

    public void SetHealAmount(int amount)
    {
        healAmount = amount;
    }

    public override void Use(Player player)
    {
        if (player == null) return;

        player.Heal(healAmount);
        Debug.Log("Зелье использовано. Лечение: " + healAmount);

        player.RemoveItem(this);
    }
}