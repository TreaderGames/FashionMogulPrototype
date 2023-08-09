using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    [SerializeField] WardrobeData.WardrobeType wardrobeType;

    public WardrobeData.WardrobeType pWardrobeType { get => wardrobeType; }
}
