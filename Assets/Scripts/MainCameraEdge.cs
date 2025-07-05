using UnityEngine;
using UnityEngine.InputSystem;

public class EdgeZoneScroller : MonoBehaviour
{
    [Header("Strefy Edge (Trigger)")]
    public BoxCollider2D leftZone;
    public BoxCollider2D rightZone;

    [Header("Kontener sceny (wszystkie obiekty tła)")]
    public Transform target;

    [Header("Prędkość scrolla (world units/sec)")]
    public float scrollSpeed = 5f;

    [Header("Drobna korekta (zapobiega mikro-prześwitom)")]
    public float margin = 0f;

    Camera cam;
    // lokalne granice tła względem pivotu targeta
    float combinedMinLocalX;
    float combinedMaxLocalX;

    void Start()
    {
        cam = Camera.main;

        // 1) Pobranie wszystkich Rendererów wewnątrz targeta
        var rends = target.GetComponentsInChildren<Renderer>();
        if (rends.Length == 0)
        {
            Debug.LogError("EdgeZoneScroller: nie znalazłem żadnych Renderer’ów w target!");
            enabled = false;
            return;
        }

        // 2) Scal ich bounds w jeden
        Bounds combined = rends[0].bounds;
        for (int i = 1; i < rends.Length; i++)
            combined.Encapsulate(rends[i].bounds);

        // 3) Przelicz na współrzędne lokalne względem target.position
        combinedMinLocalX = combined.min.x - target.position.x;
        combinedMaxLocalX = combined.max.x - target.position.x;
    }

    void Update()
    {
        // 4) Dynamiczne obliczenie połowy szerokości kamery (world units)
        float halfCamW = cam.orthographicSize * cam.aspect;
        float camX = cam.transform.position.x;

        // 5) Na podstawie combinedMin/Max i halfCamW ustalamy granice ruchu targeta:
        //    - target.x >= lowerBound (gdy tło nie odsłoni się z lewej)
        //    - target.x <= upperBound (gdy tło nie odsłoni się z prawej)
        float minX = camX + halfCamW - combinedMaxLocalX + margin;
        float maxX = camX - halfCamW - combinedMinLocalX - margin;

        // 6) Pobranie pozycji kursora w world space
        Vector2 mPx = Mouse.current.position.ReadValue();
        Vector3 mWorld = cam.ScreenToWorldPoint(
            new Vector3(mPx.x, mPx.y, -cam.transform.position.z)
        );

        // 7) Sprawdzenie stref i obliczenie ruchu
        float move = 0f;
        if (leftZone.OverlapPoint(mWorld)) move = +scrollSpeed * Time.deltaTime;
        else if (rightZone.OverlapPoint(mWorld)) move = -scrollSpeed * Time.deltaTime;

        // 8) Przesunięcie i clamp
        Vector3 p = target.position + new Vector3(move, 0f, 0f);
        p.x = Mathf.Clamp(p.x, minX, maxX);
        target.position = p;
    }

    // (opcjonalnie) wizualizacja granic w edytorze
    void OnDrawGizmosSelected()
    {
        if (cam == null) cam = Camera.main;
        float halfCamW = cam.orthographicSize * cam.aspect;
        float camX = cam.transform.position.x;
        float minX = camX + halfCamW - combinedMaxLocalX + margin;
        float maxX = camX - halfCamW - combinedMinLocalX - margin;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minX, -100, 0), new Vector3(minX, +100, 0));
        Gizmos.DrawLine(new Vector3(maxX, -100, 0), new Vector3(maxX, +100, 0));
    }
}
