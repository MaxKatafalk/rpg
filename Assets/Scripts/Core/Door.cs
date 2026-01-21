using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractableObject
{
    public bool isLocked = true;
    public int requiredKeyId = 1;
    public string targetSceneName;
    public Vector3 playerSpawnPosition = Vector3.zero;

    public override void Interact(Player player)
    {
        if (!CanInteract()) return;

        if (isLocked)
        {
            TryUnlock(player);
        }
        else
        {
            EnterDoor(player);
        }
    }

    private void TryUnlock(Player player)
    {
        var inventory = player.GetInventory();
        foreach (Item item in inventory)
        {
            if (item is KeyItem key)
            {
                if (key.GetDoorId() == requiredKeyId)
                {
                    isLocked = false;
                    Debug.Log("Дверь открыта ключом с ID: " + requiredKeyId);
                    return;
                }
            }
        }

        Debug.Log("Нужен ключ с ID: " + requiredKeyId);
    }

    private void EnterDoor(Player player)
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            // Сохраняем данные игрока
            PlayerPrefs.SetInt("PlayerHealth", player.GetHealth());
            PlayerPrefs.SetFloat("PlayerPosX", playerSpawnPosition.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerSpawnPosition.y);
            PlayerPrefs.Save();

            Debug.Log("Переход в сцену: " + targetSceneName);
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.Log("Дверь никуда не ведет");
        }
    }
}