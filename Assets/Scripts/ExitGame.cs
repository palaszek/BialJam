using UnityEngine;
using UnityEngine.InputSystem;

public class ExitGame : MonoBehaviour
{
    Camera cam;
    Collider2D self;

    void Awake()
    {
        cam = Camera.main;
        self = GetComponent<Collider2D>();
    }

    void Update()
    {
        var pos = Mouse.current.position.ReadValue();
        var hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(pos));
        bool over = hit.collider == self;

        if (!Mouse.current.leftButton.wasPressedThisFrame || !over) return;

        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
