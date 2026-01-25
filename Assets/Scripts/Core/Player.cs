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
            if (GameManager.Instance != null)
            {
                health = GameManager.Instance.playerHealth;

                inventory = GameManager.Instance.LoadInventory();
            }
            else
            {
                Debug.Log("Игрок создан. Здоровье: " + health);
            }
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
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SaveInventory(inventory);
            }
        }

        public void UseItem(Item item)
        {
            if (item == null) return;

            item.Use(this);
            Debug.Log("Использован предмет: " + item.GetName());
        }

        public void RemoveItem(Item item)
        {
            inventory.Remove(item);
            Debug.Log("Предмет удален: " + item.GetName());
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SaveInventory(inventory);
            }
        }
    }
}
