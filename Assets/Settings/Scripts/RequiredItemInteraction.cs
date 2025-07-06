using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class RequiredItemInteraction : MonoBehaviour
{
    [Tooltip("Dok³adna nazwa wymaganego przedmiotu")]
    public string requiredItemName;

    [Tooltip("Domyœlny UnityEvent z Twoimi akcjami (np. otwarcie drzwi)")]
    public UnityEvent onUse;

    [Header("Zamiana obrazu (UI Image)")]
    [Tooltip("Ten Image dostanie nowy Sprite")]
    public SpriteRenderer targetRenderer;

    [Tooltip("Sprite, który bêdzie ustawiony po u¿yciu")]
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
}
