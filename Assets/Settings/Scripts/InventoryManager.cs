using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    [Header("Limit przedmiot�w w plecaku")]
    public int maxItems = 3;
    public TextAnimation textAnimation;
    // aktualne przedmioty
    public List<InventoryItem> items = new List<InventoryItem>();

    [Header("UI Ekwipunku")]
    public InventoryUI inventoryUI;

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
        textAnimation ??= GetComponent<TextAnimation>();
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
        InventoryItem item = hit.GetComponent<InventoryItem>();
        if (item != null)
            TryAddItem(item);
    }

    void TryAddItem(InventoryItem item)
    {
        if (items.Count >= maxItems)
        {
            textAnimation.Play("Inventory pe�ny!");
            return;
        }
        items.Add(item);
        item.gameObject.SetActive(false);
        inventoryUI.UpdateUI(items);
        textAnimation.Play($"Zebrano: {item.itemName}");
    }

    public bool RemoveSelectedItem()
    {
        var sel = GetSelectedItem();
        if (sel == null) return false;
        items.Remove(sel);
        inventoryUI.UpdateUI(items);
        inventoryUI.Deselect();
        textAnimation.Play($"U�yto: {sel.itemName}");
        return true;
    }
    public InventoryItem GetSelectedItem()
      => inventoryUI.GetSelectedItem(items);
}


