using UnityEngine;
using UnityEngine.UI;

public class Scene12Manager : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Image, kt�rego sprite zmieniamy")]
    public Image screenImage;

    [Header("Grafiki dla poszczeg�lnych stan�w")]
    public Sprite maxHumanitySprite;  // gdy humanity >=100
    public Sprite hasRoseSprite;      // gdy w ekwipunku jest item 'r�a'
    public Sprite defaultSprite;      // w pozosta�ych wypadkach

    void Start()
    {
        // 1) Znajd� Playera po tagu
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("[Scene12Manager] Nie znalaz�em Playera!");
            return;
        }

        // 2) Pobierz komponenty Humanity i InventoryManager
        var hum = player.GetComponent<Humanity>();
        var inv = player.GetComponent<InventoryManager>();
        if (hum == null || inv == null)
        {
            Debug.LogError("[Scene12Manager] Brakuje Humanity lub InventoryManager na Playerze!");
            return;
        }

        // 3) Ustal kt�ry sprite wstawi�
        if (hum.value >= 100)
        {
            screenImage.sprite = maxHumanitySprite;
        }
        else if (inv.HasItem("R�a"))
        {
            screenImage.sprite = hasRoseSprite;
        }
        else
        {
            screenImage.sprite = defaultSprite;
        }
    }
}
