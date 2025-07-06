using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasPersistence : MonoBehaviour
{
    static CanvasPersistence instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // Subskrybuj event �adowania scen
            SceneManager.sceneLoaded += OnSceneLoaded;
            // Obs�u� aktualn� scen� ju� na start
            HandleScene(SceneManager.GetActiveScene());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Wywo�ywane przy ka�dej zmianie sceny
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleScene(scene);
    }

    // Ukrywa lub pokazuje Canvas w zale�no�ci od nazwy sceny
    void HandleScene(Scene scene)
    {
        if (scene.name == "Scene12")
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
