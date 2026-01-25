using Assets.Scripts.Core;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float interactionRange = 2f;
    public LayerMask interactableLayer;

    private Player player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupItem();
            TryInteractWithObject();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseFirstPotion();
        }
    }

    void UseFirstPotion()
    {
        var inventory = player.GetInventory();
        if (inventory.Count == 0)
        {
            Debug.Log("Инвентарь пуст");
            return;
        }

        foreach (Item item in inventory)
        {
            if (item is PotionItem potion)
            {
                player.UseItem(potion);
                return;
            }
        }

        Debug.Log("Нет зелий в инвентаре");
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(x, y).normalized;
        rb.linearVelocity = movement * speed;
    }

    void TryPickupItem()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);

        Item closestItem = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            Item item = collider.GetComponent<Item>();
            if (item != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }

        if (closestItem != null)
        {
            Item itemCopy = Instantiate(closestItem);
            itemCopy.gameObject.SetActive(false);
            DontDestroyOnLoad(itemCopy.gameObject);

            player.AddItem(itemCopy);
            closestItem.gameObject.SetActive(false);

            Debug.Log("Подобран предмет: " + closestItem.GetName());
        }
    }

    void TryInteractWithObject()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);

        InteractableObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            InteractableObject interactable = collider.GetComponent<InteractableObject>();
            if (interactable != null && interactable.CanInteract())
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = interactable;
                }
            }
        }

        if (closestObject != null)
        {
            closestObject.Interact(player);
        }
    }

    void UseItem()
    {
        var inventory = player.GetInventory();
        if (inventory.Count == 0)
        {
            Debug.Log("Инвентарь пуст");
            return;
        }

        player.UseItem(inventory[0]);
    }
}