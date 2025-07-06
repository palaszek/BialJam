using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    [Header("Limit przedmiotów w plecaku")]
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
        // tylko jeden klik na klatkê
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        // pozycja kursora w World Space
        Vector2 mousePx = Mouse.current.position.ReadValue();
        Vector3 worldPoint = cam.ScreenToWorldPoint(
            new Vector3(mousePx.x, mousePx.y, -cam.transform.position.z)
        );
        // sprawdŸ, czy trafiliœmy w collider 2D
        Collider2D hit = Physics2D.OverlapPoint(worldPoint);
        if (hit == null || !hit.CompareTag("Item"))
            return;

        // mamy Item?
        InventoryItem item = hit.GetComponent<InventoryItem>();
        if (item != null)
            TryAddItem(item);
    }

    public void TryAddItem(InventoryItem item)
    {
        if (items.Count >= maxItems)
        {
            textAnimation.Play("Inventory pe³ny!");
            return;
        }

        // 1) Stwórz kopiê itemu
        InventoryItem copy = Instantiate(item);

        // 2) Ustaw klona poza ekranem
        copy.transform.position = new Vector3(10000f, 10000f, 0f);

        // 3) Wy³¹cz chowanko w klonie, ¿eby nie znika³ przy dodaniu
        copy.hideOnClick = false;

        // 4) Ukryj lub zniszcz orygina³ na scenie
        if (item.hideOnClick)
            item.gameObject.SetActive(false);

        // 5) Podmieñ rodzica (opcjonalne, ale pomaga w organizacji)
        copy.transform.SetParent(transform);

        // 6) Dodaj klona do listy
        items.Add(copy);

        // 7) Odœwie¿ UI i daj feedback
        inventoryUI.UpdateUI(items);
        textAnimation.Play($"Zebrano: {item.itemName}");
    }


    public bool HasItem(string itemName)
    {
        return items.Any(i => i.itemName == itemName);
    }

    // Usuwa pierwszy napotkany item o danej nazwie
    public bool RemoveItemByName(string itemName)
    {
        var item = items.FirstOrDefault(i => i.itemName == itemName);
        if (item == null) return false;
        items.Remove(item);
        // Je¿eli chcesz odœwie¿yæ UI (np. ikonki)
        inventoryUI.UpdateUI(items);
        Debug.Log($"Usuniêto z inventory: {item.itemName}");
        return true;
    }
}


