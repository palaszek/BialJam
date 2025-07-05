using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    Camera cam;
    public TextAnimation textAnimation;

    void Awake() => cam = Camera.main;

    void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector2 mpx = Mouse.current.position.ReadValue();
        Vector3 wp = cam.ScreenToWorldPoint(new Vector3(mpx.x, mpx.y, -cam.transform.position.z));

        var hit = Physics2D.OverlapPoint(wp);
        if (hit == null) return;

        var req = hit.GetComponent<RequiredItemInteraction>();
        if (req != null)
            HandleRequiredItem(req);
    }

    void HandleRequiredItem(RequiredItemInteraction req)
    {
        var sel = inventoryManager.GetSelectedItem();
        if (sel == null)
        {
            textAnimation.Play($"Potrzebujesz: {req.requiredItemName}");
            return;
        }

        if (sel.itemName != req.requiredItemName)
        {
            textAnimation.Play($"Ten obiekt wymaga: {req.requiredItemName}");
            return;
        }

        textAnimation.Play($"U¿ywasz {sel.itemName} na {req.gameObject.name}");
        req.onUse.Invoke();
        inventoryManager.RemoveSelectedItem();
    }
}
