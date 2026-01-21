using Assets.Scripts.Core;
using UnityEngine;

public class WeaponItem : Item
{
    private int damageBonus = 5;

    public int GetDamageBonus()
    {
        return damageBonus;
    }

    public void SetDamageBonus(int bonus)
    {
        damageBonus = bonus;
    }

    public override void Use(Player player)
    {
        Debug.Log("Оружие использовано. Бонус урона: " + damageBonus);
    }
}