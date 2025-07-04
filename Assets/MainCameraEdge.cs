using UnityEngine;
using UnityEngine.InputSystem; // nowe Input System

public class EdgeZoneScroller : MonoBehaviour
{
    [Header("Strefy")]
    public BoxCollider2D leftZone;
    public BoxCollider2D rightZone;

    [Header("Obiekt do przesuwania")]
    public Transform target;      // np. tło (sprite) lub inny kontener sceny

    [Header("Prędkość scrolla (world units/sec)")]
    public float scrollSpeed = 5f;

    [Header("Ograniczenia X (opcjonalnie)")]
    public float minX = float.NegativeInfinity;
    public float maxX = float.PositiveInfinity;

    void Update()
    {
        // 1) weź pozycję kursora w pikselach
        Vector2 mousePx = Mouse.current.position.ReadValue();
        // 2) rzutuj na świat (z uwzględnieniem z od kamery do z=0)
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(mousePx.x, mousePx.y, -Camera.main.transform.position.z)
        );

        // 3) sprawdź czy kursor jest w jednej ze stref
        float move = 0f;
        if (leftZone.OverlapPoint(mouseWorld))
        {
            Debug.Log("Kursor w lewej strefie");
            // kursor w lewej strefie → przesuwaj target w prawo
            move = scrollSpeed * Time.deltaTime;
        }
        else if (rightZone.OverlapPoint(mouseWorld))
        {
            Debug.Log("Kursor w prawej strefie");
            // kursor w prawej strefie → przesuwaj target w lewo
            move = -scrollSpeed * Time.deltaTime;
        }

        // 4) zastosuj ruch i ogranicz w osi X
        Vector3 pos = target.position + new Vector3(move, 0f, 0f);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        target.position = pos;
    }
}
