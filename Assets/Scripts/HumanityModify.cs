using UnityEngine;
using UnityEngine.InputSystem;

public class HumanityModify : MonoBehaviour
{
    public int amount;
    public Humanity humanity;

    void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        var cam = Camera.main;
        var hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!hit) return;

        if (hit.transform == transform)
            humanity?.Add(amount);
    }
}
