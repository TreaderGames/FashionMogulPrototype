using UnityEngine;
using System;

public enum CustomerState
{
    None,
    MovingToMirror,
    MovingOut,
    Waiting,
}

[Serializable]
public class CustomerData
{
    public WardrobeData.WardrobeType wardrobeRequest;
    public CustomerState customerState;
    public Transform startPos;
    public Transform endPos;
}
