using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public void TryAddItem(InventoryItem item)
    {
        if (items.Count >= maxItems)
        {
            textAnimation.Play("Inventory pe�ny!");
            return;
        }

        // 1) Stw�rz kopi� itemu
        InventoryItem copy = Instantiate(item);

        // 2) Ustaw klona poza ekranem
        copy.transform.position = new Vector3(10000f, 10000f, 0f);

        // 3) Wy��cz chowanko w klonie, �eby nie znika� przy dodaniu
        copy.hideOnClick = false;

        // 4) Ukryj lub zniszcz orygina� na scenie
        if (item.hideOnClick)
            item.gameObject.SetActive(false);

        // 5) Podmie� rodzica (opcjonalne, ale pomaga w organizacji)
        copy.transform.SetParent(transform);

        // 6) Dodaj klona do listy
        items.Add(copy);

        // 7) Od�wie� UI i daj feedback
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
        // Je�eli chcesz od�wie�y� UI (np. ikonki)
        inventoryUI.UpdateUI(items);
        Debug.Log($"Usuni�to z inventory: {item.itemName}");
        return true;
    }
}


