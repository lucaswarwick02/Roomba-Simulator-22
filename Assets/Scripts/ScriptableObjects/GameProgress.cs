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

    public bool IsHouseUnlocked (int house) {
        if (house == 1) return true;

        return GetHouseSaveData(house - 1).All(levelSaveData => levelSaveData.hasMedal);
    }

    public bool IsDayUnlocked (int house, int room) {
        return GetHouseSaveData(house)[room - 1].isUnlocked;
    }

    public bool DayHasMedal (int house, int room) {
        return GetHouseSaveData(house)[room - 1].hasMedal;
    }

    public int NumberOfMedals () {
        int count = 0;

        for (int i = 1; i <= 3; i++) {
            foreach(LevelSaveData levelSaveData in GetHouseSaveData(i)) {
                count += levelSaveData.hasMedal ? 1 : 0;
            }
        }

        return count;
    }

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