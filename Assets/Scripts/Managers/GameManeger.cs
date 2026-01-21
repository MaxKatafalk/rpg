using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerPrefab;
    public Vector3 defaultSpawnPosition = Vector3.zero;
    public GameObject uiPrefab;
    private GameObject currentPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Загружена сцена: " + scene.name);

        if (GameObject.FindObjectOfType<Canvas>() == null && uiPrefab != null)
        {
            Instantiate(uiPrefab);
        }

        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }

        SpawnPlayer();
    }
    void SpawnPlayer()
    {
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
        if (existingPlayer != null)
        {
            Debug.Log("Игрок уже есть на сцене, удаляю...");
            Destroy(existingPlayer);
        }

        if (playerPrefab != null)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            currentPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Игрок создан на позиции: " + spawnPosition);
        }
        else
        {
            Debug.LogError("Player Prefab не назначен в GameManager!");
        }
    }

    Vector3 GetSpawnPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerPosX", defaultSpawnPosition.x);
        float y = PlayerPrefs.GetFloat("PlayerPosY", defaultSpawnPosition.y);
        return new Vector3(x, y, 0);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}