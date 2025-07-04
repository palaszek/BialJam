using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NoteManager : MonoBehaviour
{
    Image note;
    Image noteInner;
    Sprite[] noteSprites;
    int index;

    void Awake()
    {
        if (note == null) note = GetComponent<Image>();
        if (noteInner == null && note != null) noteInner = note.GetComponentInChildren<Image>(true);
        note.enabled = false;
        noteInner.enabled = false;
    }

    void Update()
    {
        if (note.enabled == true && Mouse.current.leftButton.wasPressedThisFrame)
        {
            note.enabled = false;
            noteInner.enabled = false;
        }
    }

    public void Show()
    {
        note.enabled = true;
    }
}
