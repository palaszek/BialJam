using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    [Header("Sloty UI")]
    public Image[] slotImages;      // <-- tu przeci¹gnij 3 Image z ekranu
    public Sprite emptySlotSprite;

    void Start()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            slotImages[i].sprite = emptySlotSprite;
            slotImages[i].color = new Color(1, 1, 1, 0.3f);
        }
    }

    public void UpdateUI(List<InventoryItem> items)
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < items.Count)
            {
                slotImages[i].sprite = items[i].icon;
                slotImages[i].color = Color.white;
            }
            else
            {
                slotImages[i].sprite = emptySlotSprite;
                slotImages[i].color = new Color(1, 1, 1, 0.3f);
            }
        }
    }
}
