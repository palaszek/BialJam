using UnityEngine;

public class PotworManager : MonoBehaviour
{
    public Humanity humanity;
    public Sprite[] sprites = new Sprite[4];
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int index = 0;

        if (humanity.value < 33) index = 0;
        else if (humanity.value < 66) index = 1;
        else if (humanity.value < 100) index = 2;
        else if (humanity.value == 100) index = 3;

        sr.sprite = sprites[index];
    }
}
