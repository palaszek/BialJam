using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class RequiredItemInteraction : MonoBehaviour
{
    [Tooltip("Dokładna nazwa wymaganego przedmiotu")]
    public string requiredItemName;

    [Tooltip("Domyślny UnityEvent z Twoimi akcjami (np. otwarcie drzwi)")]
    public UnityEvent onUse;

    [Header("Zamiana obrazu (UI Image)")]
    [Tooltip("Ten Image dostanie nowy Sprite")]
    public SpriteRenderer targetRenderer;

    [Tooltip("Sprite, który będzie ustawiony po użyciu")]
    public Sprite usedSprite;

    [Header("Nagroda — prefab z InventoryItem")]
    public InventoryItem rewardItemPrefab;

    void Awake()
    {
        var col = GetComponent<Collider2D>();
        if (!col.isTrigger) col.isTrigger = true;
    }

    public void RemoveSelf()
    {
        Destroy(gameObject);
    }

    public void ChangeImage()
    {
        if (targetRenderer == null || usedSprite == null)
        {
            Debug.LogWarning($"[{name}] Nie ustawiono targetImage lub usedSprite!");
            return;
        }
        targetRenderer.sprite = usedSprite;
    }

    [Header("Spawnowanie w świecie")]
    [Tooltip("Jeśli true → po onUse instancjonujemy ten prefab w miejscu obiektu")]
    public bool spawnInWorld = false;
    [Tooltip("Prefab, który utworzymy w świecie zamiast usuniętego obiektu")]
    public GameObject spawnPrefab;

    /// <summary>
    /// Dodaje nagrodę do ekwipunku (jeśli rewardItemPrefab != null).
    /// </summary>
    public void GiveRewardToInventory()
    {
        if (rewardItemPrefab == null) return;
        InventoryManager im = FindFirstObjectByType<InventoryManager>();
        if (im != null)
            im.TryAddItem(rewardItemPrefab);
    }

    /// <summary>
    /// Spawnuje w świecie `spawnPrefab` dokładnie w miejscu i obrocie tego obiektu.
    /// </summary>
    public void SpawnInWorld()
    {
        if (!spawnInWorld || spawnPrefab == null) return;
        Instantiate(spawnPrefab, transform.position, transform.rotation);
    }
}
