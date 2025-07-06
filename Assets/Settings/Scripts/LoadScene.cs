using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Collider2D))]
public class LoadScene : MonoBehaviour
{
    [Tooltip("Nazwa sceny dok³adnie tak, jak w Build Settings")]
    public string NazwaSceny;
    public float CzasPrzejscia = 1f;
    // Update is called once per frame
    public Animator transition;
    Camera cam;
    AudioManager audioManager;
    private void OnAwake()
    {
       
    }
    void Start()
    {
        cam = Camera.main;
        if (string.IsNullOrEmpty(NazwaSceny))
            Debug.LogWarning($"[{name}] nie ustawiono sceneName!");
        audioManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>();
    }

    void Update()
    {
        // tylko jeden klik na klatkê
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        // pozycja kursora w World Space
        Vector2 mousePx = Mouse.current.position.ReadValue();
        Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(mousePx.x, mousePx.y, -cam.transform.position.z));

        // sprawdŸ, czy trafiliœmy w ten collider
        Collider2D hit = Physics2D.OverlapPoint(worldPoint);
        if (hit != null && hit.gameObject == gameObject)
        {
            // za³aduj scenê
            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.Chodzenie);
            }
            LoadNextLevel(NazwaSceny);
        }
    }

    public void LoadNextLevel(string sceneName)
    {
        StartCoroutine(LoadLevel(NazwaSceny));
    }
    IEnumerator LoadLevel(string sceneName)
    {
        if(transition != null)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(CzasPrzejscia); // Czas trwania animacji przejœcia
        }
            
        SceneManager.LoadScene(sceneName);

    }
}
