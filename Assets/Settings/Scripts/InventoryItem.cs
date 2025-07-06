using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InventoryItem : MonoBehaviour
{
    [Tooltip("Nazwa wyœwietlana w inventory")]
    public string itemName;
    [Tooltip("Ikona (opcjonalnie)")]
    public Sprite icon;
    [Tooltip("Czy przedmiot ma znikn¹æ po klikniêciu")]
    public bool hideOnClick = true;

    void Awake()
    {
        var col = GetComponent<Collider2D>();
        if (!col.isTrigger) col.isTrigger = true;
    }
}
