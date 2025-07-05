using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class ObjectManager : MonoBehaviour
{
    public bool useText = false;
    TextAnimation textTarget;
    public string newText;

    public bool useHumanity = false;
    Humanity humanity;
    public int humanityAmount;

    public bool useNote = false;
    NoteManager noteTarget;
    public int noteIndex;

    Camera cam;
    Collider2D self;
    SpriteRenderer[] childSprites;

    void Awake()
    {
        cam = Camera.main;
        self = GetComponent<Collider2D>();

        if (textTarget == null) textTarget = FindObjectOfType<TextAnimation>(true);
        if (humanity == null) humanity = FindObjectOfType<Humanity>(true);
        if (noteTarget == null) noteTarget = FindObjectOfType<NoteManager>(true);

        childSprites = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var sr in childSprites)
            if (sr.transform != transform) sr.enabled = false;
    }

    void Update()
    {
        var pos = Mouse.current.position.ReadValue();
        var hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(pos));
        bool over = hit.collider == self;

        foreach (var sr in childSprites)
            if (sr.transform != transform) sr.enabled = over;

        if (NoteManager.IsOpen || !Mouse.current.leftButton.wasPressedThisFrame || !over) return;

        if (useText && textTarget != null) textTarget.Play(newText);
        if (useHumanity && humanity != null) humanity.Add(humanityAmount);
        if (useNote && noteTarget != null) noteTarget.Show(noteIndex);
    }
}
