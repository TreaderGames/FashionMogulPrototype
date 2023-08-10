
public class PlayerDataController : Singleton<PlayerDataController>
{
    public struct PlayerData
    {
        public int currentOutfitStackCount;
        public WardrobeData.WardrobeType currentWardrobeType;
    }

    PlayerData playerData;

    #region Public
    public void AddToWardrobe(WardrobeData.WardrobeType wardrobeType)
    {
        if(playerData.currentWardrobeType != wardrobeType)
        {
            playerData.currentOutfitStackCount = 1;
            playerData.currentWardrobeType = wardrobeType;
        }
        else
        {
            playerData.currentOutfitStackCount++;
        }

        EventController.TriggerEvent(EventID.EVENT_PLAYER_DATA_UPDATE, playerData);
    }

    public bool RemoveFromWardrobe(WardrobeData.WardrobeType wardrobeType)
    {
        if (playerData.currentWardrobeType == wardrobeType && playerData.currentOutfitStackCount > 0)
        {
            playerData.currentOutfitStackCount--;
            EventController.TriggerEvent(EventID.EVENT_PLAYER_DATA_UPDATE, playerData);

            return true;
        }

        return false;
    }
    #endregion
}
