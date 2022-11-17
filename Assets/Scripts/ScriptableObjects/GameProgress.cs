using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "New Game Progress", menuName = "Game Progress")]
public class GameProgress : ScriptableObject
{
    public LevelSaveData[] week1Levels = {
        new LevelSaveData(true),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false)
    };

    public LevelSaveData[] week2Levels = {
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false)
    };

    public LevelSaveData[] week3Levels = {
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false),
        new LevelSaveData(false)
    };

    public bool IsWeekUnlocked (int week) {
        
        switch (week) {
            case 1:
                return true; // Week 1 is always unlocked
            case 2:
                return week1Levels.All(levelSaveData => levelSaveData.hasMedal);
            case 3:
                return week2Levels.All(levelSaveData => levelSaveData.hasMedal);
            default:
                return false;
        }
        
    }

    public bool IsDayUnlocked (int week, int day) {
        switch (week) {
            case 1:
                return week1Levels[day - 1].isUnlocked;
            case 2:
                return week2Levels[day - 1].isUnlocked;
            case 3:
                return week3Levels[day - 1].isUnlocked;
            default:
                return false;
        }
    }

    public bool DayHasMedal (int week, int day) {
        switch (week) {
            case 1:
                return week1Levels[day - 1].hasMedal;
            case 2:
                return week2Levels[day - 1].hasMedal;
            case 3:
                return week3Levels[day - 1].hasMedal;
            default:
                return false;
        }
    }

    public int NumberOfMedals () {
        int count = 0;
        foreach (LevelSaveData levelSaveData in week1Levels) {
            count += levelSaveData.hasMedal ? 1 : 0;
        }
        foreach (LevelSaveData levelSaveData in week2Levels) {
            count += levelSaveData.hasMedal ? 1 : 0;
        }
        foreach (LevelSaveData levelSaveData in week3Levels) {
            count += levelSaveData.hasMedal ? 1 : 0;
        }
        return count;
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