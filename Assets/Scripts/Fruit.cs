using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField]
    fruitType type = fruitType.Cherry;
    public enum fruitType
    {
        Cherry,
        Strawberry,
        Grape,
        Dekopon,
        Persimmon,
        Apple,
        Pear,
        Peach,
        Pineapple,
        Melon,
        Watermelon
    }

    public int scoreToSend;
}
