using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Variable which wil contain the data in between levels, and for saving/loading the data.
/// </summary>
[System.Serializable]
public class SaveData {
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

}

[System.Serializable]
public struct LevelSaveData {
    public bool unlocked;
    public float percentage;

    public LevelSaveData (bool unlocked) {
        this.unlocked = unlocked;
        this.percentage = 0f;
    }
}