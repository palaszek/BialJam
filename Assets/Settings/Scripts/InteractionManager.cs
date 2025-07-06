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
        // 1) Sprawd�, czy w inventory jest wymagany item
        if (!inventoryManager.HasItem(req.requiredItemName))
        {
            textAnimation.Play($"Potrzebujesz: {req.requiredItemName}");
            return;
        }

        // 2) Masz item, wi�c wykonaj akcj�
        //textAnimation.Play($"U�ywasz: {req.requiredItemName}");
        req.onUse.Invoke();

        // 3) Usu� go z inventory
        inventoryManager.RemoveItemByName(req.requiredItemName);


        if (req.rewardItemPrefab != null)
        {
            // 4a) Stw�rz instancj� prefab-a
            var rewardInstance = Instantiate(req.rewardItemPrefab);
            // 4b) Dodaj do ekwipunku (TryAddItem wy��czy GameObject, wi�c nie zostanie w �wiecie)
            inventoryManager.TryAddItem(rewardInstance);
        }
    }
}
