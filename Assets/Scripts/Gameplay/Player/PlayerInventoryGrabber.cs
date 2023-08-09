using UnityEngine;

public class PlayerInventoryGrabber : MonoBehaviour
{
    [SerializeField] PlayerCollision playerCollision;
    [SerializeField] int delayToAddSeconds = 4;

    bool isTouchingWardrobe;
    float currentDelta = 0;
    WardrobeData.WardrobeType currentWardrobeType;

    #region Unity
    private void OnEnable()
    {
        playerCollision.AddListener(HandleWardrobeCollision);
    }

    private void OnDisable()
    {
        playerCollision?.RemoveListener(HandleWardrobeCollision);
    }

    private void Update()
    {
        if(isTouchingWardrobe)
        {
            currentDelta += Time.deltaTime;
            if(currentDelta >= delayToAddSeconds)
            {
                currentDelta = 0;
                PlayerDataController.Instance.AddToWardrobe(currentWardrobeType);
            }
        }
    }
    #endregion

    #region Callback
    private void HandleWardrobeCollision(bool collisionEnter, WardrobeData.WardrobeType wardrobeType)
    {
        isTouchingWardrobe = collisionEnter;
        currentWardrobeType = wardrobeType;
    }
    #endregion
}
