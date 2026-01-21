using Assets.Scripts.Core;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Text healthText;
    private Text inventoryText;
    private Player player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CreateUI();
        FindPlayer();
        UpdateUI();
    }

    void Update()
    {
        if (player == null)
        {
            FindPlayer();
        }
        else
        {
            UpdateUI();
        }
    }

    void CreateUI()
    {
        GameObject canvasObject = GameObject.Find("Canvas");
        Canvas canvas;

        if (canvasObject == null)
        {
            canvasObject = new GameObject("Canvas");
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            Debug.Log("Canvas создан");
        }
        else
        {
            canvas = canvasObject.GetComponent<Canvas>();
        }

        GameObject healthObject = new GameObject("HealthUI");
        healthObject.transform.SetParent(canvas.transform);
        healthText = healthObject.AddComponent<Text>();

        Font font = GetDefaultFont();
        if (font != null)
        {
            healthText.font = font;
        }

        healthText.fontSize = 30;
        healthText.color = Color.white;
        healthText.alignment = TextAnchor.UpperLeft;

        RectTransform healthRT = healthObject.GetComponent<RectTransform>();
        healthRT.anchorMin = new Vector2(0, 1);
        healthRT.anchorMax = new Vector2(0, 1);
        healthRT.pivot = new Vector2(0, 1);
        healthRT.anchoredPosition = new Vector2(10, -10);
        healthRT.sizeDelta = new Vector2(400, 50);
        healthText.text = "Здоровье: --";

        GameObject inventoryObject = new GameObject("InventoryUI");
        inventoryObject.transform.SetParent(canvas.transform);
        inventoryText = inventoryObject.AddComponent<Text>();

        if (font != null)
        {
            inventoryText.font = font;
        }

        inventoryText.fontSize = 24;
        inventoryText.color = Color.white;
        inventoryText.alignment = TextAnchor.LowerLeft;

        RectTransform inventoryRT = inventoryObject.GetComponent<RectTransform>();
        inventoryRT.anchorMin = new Vector2(0, 0);
        inventoryRT.anchorMax = new Vector2(0, 0);
        inventoryRT.pivot = new Vector2(0, 0);
        inventoryRT.anchoredPosition = new Vector2(10, 10);
        inventoryRT.sizeDelta = new Vector2(500, 50);
        inventoryText.text = "Инвентарь: --";

        Debug.Log("UI создан");
    }

    Font GetDefaultFont()
    {
        Font font = null;

        font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        if (font != null)
        {
            Debug.Log("Используем LegacyRuntime.ttf");
            return font;
        }

        Font[] allFonts = Resources.FindObjectsOfTypeAll<Font>();
        if (allFonts.Length > 0)
        {
            Debug.Log("Используем первый найденный шрифт: " + allFonts[0].name);
            return allFonts[0];
        }

        Debug.LogWarning("Не удалось найти шрифт. Будет использован системный.");
        return null;
    }

    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
            Debug.Log("Игрок найден. Здоровье: " + player.GetHealth());
        }
        else
        {
            Debug.LogWarning("Игрок не найден! Убедитесь что у объекта Player тег 'Player'");
        }
    }

    void UpdateUI()
    {
        if (player == null) return;

        if (healthText != null)
        {
            healthText.text = "Здоровье: " + player.GetHealth();
        }

        if (inventoryText != null)
        {
            var inventory = player.GetInventory();
            if (inventory.Count == 0)
            {
                inventoryText.text = "Инвентарь: пусто";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Инвентарь: ");

                for (int i = 0; i < inventory.Count; i++)
                {
                    sb.Append(inventory[i].GetName());
                    if (i < inventory.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }

                inventoryText.text = sb.ToString();
            }
        }
    }
}