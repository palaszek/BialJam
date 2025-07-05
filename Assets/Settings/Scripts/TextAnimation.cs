using UnityEngine;
using TMPro;
using System.Collections;

public class TextAnimation : MonoBehaviour
{
    public float interval = 0.03f;
    public string fullText;

    TMP_Text txt;
    Coroutine typing;

    void Awake() => txt = GetComponent<TMP_Text>();

    public void Play(string content)
    {
        fullText = content;
        if (typing != null) StopCoroutine(typing);
        typing = StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        txt.text = fullText;
        txt.maxVisibleCharacters = 0;
        txt.ForceMeshUpdate();
        int total = txt.textInfo.characterCount;

        for (int i = 1; i <= total; i++)
        {
            txt.maxVisibleCharacters = i;
            yield return new WaitForSeconds(interval);
        }
        typing = null;
    }
}