using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "New Game Progress", menuName = "Game Progress")]
public class GameProgress : ScriptableObject
{
    private LevelSaveData[] house1Levels = {
        new LevelSaveData(true),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false)
    };

    private LevelSaveData[] house2Levels = {
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false)
    };

    private LevelSaveData[] house3Levels = {
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false)
    };

    /// <summary>
    /// Check if the house number is unlocked.
    /// </summary>
    /// <param name="house">House number</param>
    /// <returns>True if unlocked, false if not</returns>
    public bool IsHouseUnlocked (int house) {
        if (house == 1) return true;

        return GetHouseSaveData(house - 1).All(levelSaveData => levelSaveData.hasMedal);
    }

    /// <summary>
    /// Check if the specified room is unlocked.
    /// </summary>
    /// <param name="house">House number</param>
    /// <param name="room">Room number</param>
    /// <returns>True if unlocked, false if not</returns>
    public bool IsRoomUnlocked (int house, int room) {
        return GetHouseSaveData(house)[room - 1].isUnlocked;
    }

    /// <summary>
    /// Check if the specified room has a medal.
    /// </summary>
    /// <param name="house">House number</param>
    /// <param name="room">Room number</param>
    /// <returns>True if the room has a medal, false if not</returns>
    public bool RoomHasMedal (int house, int room) {
        return GetHouseSaveData(house)[room - 1].hasMedal;
    }

    /// <summary>
    /// Calculate the total number of medals across all rooms.
    /// </summary>
    /// <returns>Sum of medals for all levels</returns>
    public int NumberOfMedals () {
        int count = 0;

        for (int i = 1; i <= 3; i++) {
            foreach(LevelSaveData levelSaveData in GetHouseSaveData(i)) {
                count += levelSaveData.hasMedal ? 1 : 0;
            }
        }

        return count;
    }

    /// <summary>
    /// Get the house data for a given week.
    /// </summary>
    /// <param name="week">Week number</param>
    /// <returns>Array of save data for the week</returns>
    public LevelSaveData[] GetHouseSaveData (int week) {
        switch (week) {
            case 1:
                return house1Levels;
            case 2:
                return house2Levels;
            case 3:
                return house3Levels;
            default:
                return house1Levels;
        }
    }
}



[Serializable]
public struct LevelSaveData {
    public bool isUnlocked;
    public bool hasMedal;
    public int highScore;

    public LevelSaveData (bool isUnlocked) {
        this.isUnlocked = isUnlocked;
        this.hasMedal = false;
        this.highScore = 0;
    }
}