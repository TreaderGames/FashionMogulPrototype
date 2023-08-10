using System;
using UnityEngine;

public class CustomerCollider : MonoBehaviour
{
    Action triggerCallback;

    #region Unity
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTags.PLAYER_TAG))
        {
            triggerCallback?.Invoke();
        }
    }
    #endregion

    #region Public
    public void AddListener(Action action)
    {
        triggerCallback += action;
    }
    public void RemoveListener(Action action)
    {
        triggerCallback -= action;
    }
    #endregion
}
