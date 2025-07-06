using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NoteManager : MonoBehaviour
{
    Image noteBg;
    Image noteInner;
    public Sprite[] noteSprites;
    int openedFrame = -1;
    public static bool IsOpen { get; private set; }

    void Start()
    {
        noteBg = GetComponent<Image>();
        foreach (var img in GetComponentsInChildren<Image>(true))
            if (img != noteBg) { noteInner = img; break; }
        Hide();
    }

    void Update()
    {
        if (!IsOpen) return;
        if (Time.frameCount == openedFrame) return;
        if (Mouse.current.leftButton.wasPressedThisFrame) Hide();
    }

    public void Show(int index = 0)
    {
        if (noteSprites != null && noteSprites.Length > 0)
            noteInner.sprite = noteSprites[Mathf.Clamp(index, 0, noteSprites.Length - 1)];
        noteBg.enabled = true;
        noteInner.enabled = true;
        openedFrame = Time.frameCount;
        IsOpen = true;
    }

    public void Hide()
    {
        noteBg.enabled = false;
        noteInner.enabled = false;
        openedFrame = -1;
        IsOpen = false;
    }
}
