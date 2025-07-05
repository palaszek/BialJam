using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class InteractiveItem : MonoBehaviour
{
    public bool useText = false;
    public string newText;
    TextAnimation textTarget;

    public bool useHumanity = false;
    public int humanityAmount;
    Humanity humanity;

    public bool useNote = false;
    public int noteIndex;
    NoteManager noteTarget;

    public bool useHoverText = false;
    public string hoverText;

    public bool hideOnClick = false;

    Camera cam;
    Collider2D self;
    SpriteRenderer[] childSprites;
    SpriteRenderer rootSprite;

    bool hoverShown;
    bool clickShown;

    void Awake()
    {
        cam = Camera.main;
        self = GetComponent<Collider2D>();

        textTarget = FindFirstObjectByType<TextAnimation>();
        humanity = FindFirstObjectByType<Humanity>();
        noteTarget = FindFirstObjectByType<NoteManager>();

        rootSprite = GetComponent<SpriteRenderer>();
        childSprites = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var sr in childSprites) if (sr.transform != transform) sr.enabled = false;
    }

    void Update()
    {
        var pos = Mouse.current.position.ReadValue();
        var hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(pos));
        bool over = hit.collider == self;

        foreach (var sr in childSprites) if (sr.transform != transform) sr.enabled = over;

        if (!over)
        {
            hoverShown = false;
            clickShown = false;
            return;
        }

        if (NoteManager.IsOpen) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!clickShown)
            {
                if (useText && textTarget != null) textTarget.Play(newText);
                if (useHumanity && humanity != null) humanity.Add(humanityAmount);
                if (useNote && noteTarget != null) noteTarget.Show(noteIndex);
                if (hideOnClick)
                {
                    if (rootSprite != null) rootSprite.enabled = false;
                    foreach (var sr in childSprites) sr.enabled = false;
                    self.enabled = false;
                }
                clickShown = true;
            }
            return;
        }

        if (useHoverText && !hoverShown && textTarget != null)
        {
            textTarget.Play(hoverText);
            hoverShown = true;
        }
    }
}
