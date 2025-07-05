using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EdgeZoneScroller : MonoBehaviour
{
    [Header("Strefy Edge (Trigger)")]
    public BoxCollider2D leftZone;
    public BoxCollider2D rightZone;

    [Header("Nazwa obiektu z tłem")]
    public string targetObjectName = "Sceneria";
    Transform target;

    [Header("Prędkość scrolla (world units/sec)")]
    public float scrollSpeed = 5f;

    [Header("Drobna korekta (zapobiega mikro-prześwitom)")]
    public float margin = 0f;

    Camera cam;
    float combinedMinLocalX, combinedMaxLocalX;

    void Awake()
    {
        cam = Camera.main;
        // Subskrybujemy event ładowania sceny
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Wywołane raz na start i potem za każdym razem po załadowaniu sceny
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1) Znajdź nowy target
        var go = GameObject.Find(targetObjectName);
        if (go == null)
        {
            Debug.LogError($"EdgeZoneScroller: Nie znalazłem obiektu '{targetObjectName}' w scenie {scene.name}");
            enabled = false;
            return;
        }
        target = go.transform;

        // 2) Przelicz bounds (to samo, co w Start wcześniej)
        var rends = target.GetComponentsInChildren<Renderer>();
        if (rends.Length == 0)
        {
            Debug.LogError("EdgeZoneScroller: nie znalazłem żadnych Rendererów w target!");
            enabled = false;
            return;
        }
        Bounds combined = rends[0].bounds;
        for (int i = 1; i < rends.Length; i++)
            combined.Encapsulate(rends[i].bounds);

        combinedMinLocalX = combined.min.x - target.position.x;
        combinedMaxLocalX = combined.max.x - target.position.x;
        enabled = true;  // upewnij się, że Update działa potem
    }

    void Update()
    {
        if (target == null) return;

        float halfCamW = cam.orthographicSize * cam.aspect;
        float camX = cam.transform.position.x;
        float minX = camX + halfCamW - combinedMaxLocalX + margin;
        float maxX = camX - halfCamW - combinedMinLocalX - margin;

        Vector2 mPx = Mouse.current.position.ReadValue();
        Vector3 mWorld = cam.ScreenToWorldPoint(new Vector3(mPx.x, mPx.y, -cam.transform.position.z));

        float move = 0f;
        if (leftZone.OverlapPoint(mWorld)) move = +scrollSpeed * Time.deltaTime;
        else if (rightZone.OverlapPoint(mWorld)) move = -scrollSpeed * Time.deltaTime;

        Vector3 p = target.position + new Vector3(move, 0f, 0f);
        p.x = Mathf.Clamp(p.x, minX, maxX);
        target.position = p;
    }

    void OnDrawGizmosSelected()
    {
        if (cam == null || target == null) return;
        float halfCamW = cam.orthographicSize * cam.aspect;
        float camX = cam.transform.position.x;
        float minX = camX + halfCamW - combinedMaxLocalX + margin;
        float maxX = camX - halfCamW - combinedMinLocalX - margin;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minX, -100, 0), new Vector3(minX, +100, 0));
        Gizmos.DrawLine(new Vector3(maxX, -100, 0), new Vector3(maxX, +100, 0));
    }
}
