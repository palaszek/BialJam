using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class RequiredItemInteraction : MonoBehaviour
{
    public string requiredItemName;
    public UnityEvent onUse;

    void Awake()
    {
        var col = GetComponent<Collider2D>();
        if (!col.isTrigger) col.isTrigger = true;
    }
}
