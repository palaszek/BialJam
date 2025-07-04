using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneLoaderScript : MonoBehaviour
{
    public string NazwaSceny;
    public float CzasPrzejscia = 1f;
    // Update is called once per frame
    public Animator transition;
    public void LoadNextLevel(string sceneName)
    {
        StartCoroutine(LoadLevel(NazwaSceny));
    }
    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(CzasPrzejscia); // Czas trwania animacji przejœcia
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

    }
}
