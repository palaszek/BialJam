using UnityEngine;
using UnityEngine.Events;

public class Humanity : MonoBehaviour
{
    public int value;

    public UnityEvent<int> onChanged;

    public void Add(int amount)
    {
        int updatedHumanity = Mathf.Clamp(value + amount, 0, 100);
        if (updatedHumanity == value) return;
        value = updatedHumanity;

        onChanged?.Invoke(value);
    }
}
