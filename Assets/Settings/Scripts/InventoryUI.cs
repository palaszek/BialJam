using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    [Header("Sloty UI")]
    public SlotUI[] slots;            // SlotUI[3]
    public Sprite emptySlotSprite;

    int selectedSlot = -1;

    void Start()
    {
        // zainicjuj wszystkie sloty jako nieaktywne
        foreach (var s in slots)
            s.SetSelected(false);
    }

    // wywo�ujesz po ka�dej zmianie inventory
    public void UpdateUI(List<InventoryItem> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var img = slots[i].GetComponent<Image>();
            if (i < items.Count)
            {
                img.sprite = items[i].icon;
                img.color = Color.white;
            }
            else
            {
                img.sprite = emptySlotSprite;
                img.color = new Color(1, 1, 1, 0.3f);
            }
        }
        // upewnij si�, �e wybrany slot nadal pod�wietlony lub zresetuj
        if (selectedSlot >= items.Count)
            SelectSlot(-1);
    }

    // wywo�uje SlotUI po klikni�ciu
    public void SelectSlot(int index)
    {
        // odznacz poprzedni
        if (selectedSlot >= 0 && selectedSlot < slots.Length)
            slots[selectedSlot].SetSelected(false);

        selectedSlot = index;

        // zaznacz nowy (je�li prawid�owy i niepusty)
        if (selectedSlot >= 0)
            slots[selectedSlot].SetSelected(true);
    }

    // API dla InteractionManagera
    public InventoryItem GetSelectedItem(List<InventoryItem> items)
    {
        if (selectedSlot >= 0 && selectedSlot < items.Count)
            return items[selectedSlot];
        return null;
    }

    // usuni�cie po u�yciu
    public void Deselect()
    {
        SelectSlot(-1);
    }
}
