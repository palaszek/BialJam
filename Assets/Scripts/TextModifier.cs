using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TextModify : MonoBehaviour
{
    public TMP_Text targetText;
    public string newText = "";

    void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        var cam = Camera.main;
        var hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!hit) return;

        if (hit.transform == transform && targetText != null)
            targetText.text = newText;
    }
}
