using TMPro;
using UnityEngine;

public class UIInGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] TextMeshProUGUI countText;

    #region Unity
    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_PLAYER_DATA_UPDATE, HandlePlayerDataUpdated);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_PLAYER_DATA_UPDATE, HandlePlayerDataUpdated);
    }
    #endregion

    #region Callback

    private void HandlePlayerDataUpdated(object arg)
    {
        PlayerDataController.PlayerData playerData = (PlayerDataController.PlayerData)arg;

        typeText.text = "Wardrobe Type: " + playerData.currentWardrobeType.ToString();
        countText.text = "Count: " + playerData.currentOutfitStackCount;
    }

    #endregion
}
