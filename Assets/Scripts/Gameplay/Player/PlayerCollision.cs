using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Action<bool, WardrobeData.WardrobeType> triggerCallback;

    #region Unity
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameTags.WARDROBE_TAG))
        {
            triggerCallback?.Invoke(true, other.GetComponent<Wardrobe>().pWardrobeType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameTags.WARDROBE_TAG))
        {
            triggerCallback?.Invoke(false, other.GetComponent<Wardrobe>().pWardrobeType);
        }
    }
    #endregion

    #region Public
    public void AddListener(Action<bool, WardrobeData.WardrobeType> action)
    {
        triggerCallback += action;
    }
    public void RemoveListener(Action<bool, WardrobeData.WardrobeType> action)
    {
        triggerCallback -= action;
    }
    #endregion
}
