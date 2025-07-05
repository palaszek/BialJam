using UnityEngine;
using UnityEngine.UI;


public class HumanityBarController : MonoBehaviour
{
    public Humanity humanity;
    public Image fillImage;
    
    void Start() => UpdateVisual();

    void OnValidate() => UpdateVisual();

    void Update() => UpdateVisual();

    void UpdateVisual()
    {
       if (fillImage != null) fillImage.fillAmount = humanity.value / 100f;
    }
}
