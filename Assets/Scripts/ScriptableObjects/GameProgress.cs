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
                return week1Levels.All(levelSaveData => levelSaveData.percentage == 1f);
            case 3:
                return week2Levels.All(levelSaveData => levelSaveData.percentage == 1f);
            default:
                return false;
        }
    }

    public bool IsDayUnlocked (int week, int day) {
        switch (week) {
            case 1:
                return week1Levels[day - 1].unlocked;
            case 2:
                return week2Levels[day - 1].unlocked;
            case 3:
                return week3Levels[day - 1].unlocked;;
            default:
                return false;
        }
    }
}

[Serializable]
public struct LevelSaveData {
    public bool unlocked;
    public float percentage;

    public LevelSaveData (bool unlocked) {
        this.unlocked = unlocked;
        this.percentage = 0f;
    }
}