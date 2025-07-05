using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    public int slotIndex;
    public InventoryUI inventoryUI; // referencja na komponent InventoryUI

    Image bg;

    void Awake()
    {
        bg = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryUI.SelectSlot(slotIndex);
    }

    // pozwala InventoryUI podœwietliæ bie¿¹cy slot
    public void SetSelected(bool selected)
    {
        bg.color = selected ? Color.yellow : Color.white;
    }
}
