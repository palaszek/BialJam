using UnityEngine;
using UnityEngine.UI;

public class Scene12Manager : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Image, którego sprite zmieniamy")]
    public Image screenImage;

    [Header("Grafiki dla poszczególnych stanów")]
    public Sprite maxHumanitySprite;  // gdy humanity >=100
    public Sprite hasRoseSprite;      // gdy w ekwipunku jest item 'ró¿a'
    public Sprite defaultSprite;      // w pozosta³ych wypadkach

    void Start()
    {
        // 1) ZnajdŸ Playera po tagu
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("[Scene12Manager] Nie znalaz³em Playera!");
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

        // 3) Ustal który sprite wstawiæ
        if (hum.value >= 100)
        {
            screenImage.sprite = maxHumanitySprite;
        }
        else if (inv.HasItem("Ró¿a"))
        {
            screenImage.sprite = hasRoseSprite;
        }
        else
        {
            screenImage.sprite = defaultSprite;
        }
    }
}
