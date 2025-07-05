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
        // 1) SprawdŸ, czy w inventory jest wymagany item
        if (!inventoryManager.HasItem(req.requiredItemName))
        {
            textAnimation.Play($"Potrzebujesz: {req.requiredItemName}");
            return;
        }

        // 2) Masz item, wiêc wykonaj akcjê
        textAnimation.Play($"U¿ywasz: {req.requiredItemName}");
        req.onUse.Invoke();

        // 3) Usuñ go z inventory
        inventoryManager.RemoveItemByName(req.requiredItemName);
    }
}
