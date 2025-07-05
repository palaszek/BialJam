using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveItem : MonoBehaviour
{
    public bool useText = true;
    public TextAnimation textTarget;
    public string newText;

    public bool useHumanity = true;
    public Humanity humanity;
    public int humanityAmount;

    public bool useNote = true;
    public NoteManager noteTarget;
    public int noteIndex;

    Camera cam;
    Collider2D self;

    void Awake()
    {
        cam = Camera.main;
        self = GetComponent<Collider2D>();
        if (textTarget == null) textTarget = FindObjectOfType<TextAnimation>(true);
        if (humanity == null) humanity = FindObjectOfType<Humanity>(true);
        if (noteTarget == null) noteTarget = FindObjectOfType<NoteManager>(true);
    }

    void Update()
    {
        if (NoteManager.IsOpen) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        var hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (hit.collider != self) return;

        if (useText && textTarget != null) textTarget.Play(newText);
        if (useHumanity && humanity != null) humanity.Add(humanityAmount);
        if (useNote && noteTarget != null) noteTarget.Show(noteIndex);
    }
}
