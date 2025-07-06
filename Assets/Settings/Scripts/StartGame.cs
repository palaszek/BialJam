using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
public class LoadSceneUI : MonoBehaviour, IPointerClickHandler
{
    public string sceneName;

    public float transitionTime = 1f;

    public Animator transition;

    AudioManager audioManager;

    void Awake()
    {
        if (string.IsNullOrEmpty(sceneName))
            Debug.LogWarning($"[{name}] sceneName is not set!");

        audioManager = GameObject.FindGameObjectWithTag("Player")?.GetComponent<AudioManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioManager != null)
            audioManager.PlaySFX(audioManager.Chodzenie);

        StartCoroutine(LoadSceneRoutine());
    }

    IEnumerator LoadSceneRoutine()
    {
        if (transition != null) transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
