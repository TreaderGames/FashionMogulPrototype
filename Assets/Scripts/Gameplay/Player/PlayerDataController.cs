using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataController : Singleton<PlayerDataController>
{
    private int currentOutfitStackCount = 0;
    private WardrobeData.WardrobeType currentWardrobeType;

    #region Public
    public void AddToWardrobe(WardrobeData.WardrobeType wardrobeType)
    {
        if(currentWardrobeType != wardrobeType)
        {
            currentOutfitStackCount = 1;
            currentWardrobeType = wardrobeType;
        }
        else
        {
            currentOutfitStackCount++;
        }
    }

    public bool RemoveFromWardrobe(WardrobeData.WardrobeType wardrobeType)
    {
        if (currentWardrobeType == wardrobeType)
        {
            currentOutfitStackCount--;
            return true;
        }

        return false;
    }
    #endregion
}
