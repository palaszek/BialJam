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

    public bool AudioOnClick = true;
    Camera cam;
    Collider2D self;
    SpriteRenderer[] childSprites;
    SpriteRenderer rootSprite;
    AudioManager audioManager;
    bool hoverShown;
    bool clickShown;

    void Start()
    {
        cam = Camera.main;
        self = GetComponent<Collider2D>();

        textTarget = FindFirstObjectByType<TextAnimation>();
        humanity = FindFirstObjectByType<Humanity>();
        noteTarget = FindFirstObjectByType<NoteManager>();

        rootSprite = GetComponent<SpriteRenderer>();
        childSprites = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var sr in childSprites) if (sr.transform != transform) sr.enabled = false;
        audioManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>();
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
                if (audioManager != null && AudioOnClick)
                {
                    audioManager.PlaySFX(audioManager.UżycieItem);
                }
                if (useText && textTarget != null) textTarget.Play(newText);
                if (humanity != null)
                {
                    if (useHumanity) { humanity.Add(humanityAmount); } else { humanity.Add(10); }
                }
                if (noteTarget == null) noteTarget = FindFirstObjectByType<NoteManager>();
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
