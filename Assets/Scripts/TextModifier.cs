using UnityEngine;
using UnityEngine.InputSystem;

public class TextModifier : MonoBehaviour
{
    public TextAnimation target;
    public string newText = "";

    void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        var cam = Camera.main;
        var hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!hit || hit.transform != transform || target == null) return;

        target?.Play(newText);
    }
}
