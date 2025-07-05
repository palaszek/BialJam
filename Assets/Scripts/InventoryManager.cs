using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [Header("Limit przedmiot�w w plecaku")]
    public int maxItems = 3;

    // aktualne przedmioty
    public List<Item> items = new List<Item>();

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // tylko jeden klik na klatk�
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        // pozycja kursora w World Space
        Vector2 mousePx = Mouse.current.position.ReadValue();
        Vector3 worldPoint = cam.ScreenToWorldPoint(
            new Vector3(mousePx.x, mousePx.y, -cam.transform.position.z)
        );

        // sprawd�, czy trafili�my w collider 2D
        Collider2D hit = Physics2D.OverlapPoint(worldPoint);
        if (hit == null || !hit.CompareTag("Item"))
            return;

        // mamy Item?
        Item item = hit.GetComponent<Item>();
        if (item != null)
            TryAddItem(item);
    }

    void TryAddItem(Item item)
    {
        if (items.Count >= maxItems)
        {
            Debug.Log("Inventory pe�ny!");
            return;
        }

        items.Add(item);
        Debug.Log($"Zebrano: {item.itemName} ({items.Count}/{maxItems})");

        // wy��cz z �wiata
        item.gameObject.SetActive(false);

        // TODO: od�wie� UI, je�li masz
        // UpdateInventoryUI();
    }

    // (opcjonalnie) API do wyci�gania listy
    public IReadOnlyList<Item> GetItems() => items;
}
