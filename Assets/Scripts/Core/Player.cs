using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public class Player : MonoBehaviour
    {
        private int health = 100;
        private int attack = 10;
        private List<Item> inventory = new List<Item>();

        public int GetHealth()
        {
            return health;
        }

        public int GetAttack()
        {
            return attack;
        }

        public bool IsAlive()
        {
            return health > 0;
        }

        void Start()
        {
            Debug.Log("Игрок создан. Здоровье: " + health);
        }

        public void TakeDamage(int damage)
        {
            if (!IsAlive()) return;

            health -= damage;
            if (health < 0) health = 0;

            Debug.Log("Игрок получил урон: " + damage + ". Здоровье: " + health);

            if (!IsAlive())
            {
                Debug.Log("Игрок умер!");
            }
        }

        public void Heal(int healAmount)
        {
            if (!IsAlive()) return;

            health += healAmount;
            if (health > 100) health = 100;

            Debug.Log("Игрок вылечен на: " + healAmount + ". Здоровье: " + health);
        }
        public List<Item> GetInventory()
        {
            return inventory;
        }
        public void AddItem(Item item)
        {
            if (item == null) return;

            inventory.Add(item);
            Debug.Log("Предмет добавлен в инвентарь: " + item.GetName());
        }

        
    }
}