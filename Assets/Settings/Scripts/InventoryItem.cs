using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [Tooltip("Nazwa wy�wietlana w inventory")]
    public string itemName;
    [Tooltip("Ikona (opcjonalnie)")]
    public Sprite icon;

    void Awake()
    {
        // upewnij si�, �e collider jest triggerem (cho� przy klikaniu nie jest to konieczne)
        var col = GetComponent<Collider2D>();
        if (!col.isTrigger) col.isTrigger = true;
    }
}
