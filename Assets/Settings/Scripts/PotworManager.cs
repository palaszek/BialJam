using UnityEngine;
using UnityEngine.UI;

public class PotworManager : MonoBehaviour
{
    public Humanity humanity;
    public Sprite[] sprites = new Sprite[5];
    Image sr;
    void Awake()
    {
        sr = GetComponent<Image>();
    }

    void Update()
    {
        int index = 0;

        if (humanity.value < 25) index = 0;
        else if (humanity.value < 50) index = 1;
        else if (humanity.value < 75) index = 2;
        else if (humanity.value < 100) index = 3;
        else if (humanity.value == 100) index = 4;

        sr.sprite = sprites[index];
    }
}
