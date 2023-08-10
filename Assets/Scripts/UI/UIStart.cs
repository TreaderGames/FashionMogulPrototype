using UnityEngine;

public class UIStart : MonoBehaviour
{
    #region Public
    public void OnClickStart()
    {
        EventController.TriggerEvent(EventID.EVENT_GAME_START);
        Destroy(gameObject);
    }
    #endregion
}
