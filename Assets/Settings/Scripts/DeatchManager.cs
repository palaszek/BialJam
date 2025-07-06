using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Humanity))]
public class DeathManager : MonoBehaviour
{
    [Tooltip("Nazwa sceny, do której przechodzimy gdy humanity >= 100")]
    public string endSceneName = "EndScene";

    Humanity humanity;

    void Awake()
    {
        // Pobieramy komponent Humanity na tym samym obiekcie
        humanity = GetComponent<Humanity>();
        if (humanity == null)
        {
            Debug.LogError("[DeathManager] Brak komponentu Humanity!");
            enabled = false;
            return;
        }
    }

    void OnEnable()
    {
        // Subskrybujemy event zmiany wartoœci
        humanity.onChanged.AddListener(CheckHumanity);
    }

    void OnDisable()
    {
        humanity.onChanged.RemoveListener(CheckHumanity);
    }

    void CheckHumanity(int newValue)
    {
        if (newValue >= 100)
        {
            // Mo¿esz tu jeszcze dodaæ jakiœ efekt (animacjê, dŸwiêk) przed zmian¹ sceny
            SceneManager.LoadScene(endSceneName);
        }
    }
}
