using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [Tooltip("Nazwa wyœwietlana w inventory")]
    public string itemName;
    [Tooltip("Ikona (opcjonalnie)")]
    public Sprite icon;

    void Awake()
    {
        // upewnij siê, ¿e collider jest triggerem (choæ przy klikaniu nie jest to konieczne)
        var col = GetComponent<Collider2D>();
        if (!col.isTrigger) col.isTrigger = true;
    }
}
