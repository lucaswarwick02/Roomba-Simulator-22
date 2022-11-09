using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

public class GameSave
{
    /// <summary>
    /// Edit this variable's data during game, and can save and load
    /// it using the static variables Save() and Load() respectively.
    /// </summary>
    public static SaveData INSTANCE;

    private static string getSaveDataPath () {
        return Application.persistentDataPath + "/savedata.sav";
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod () {
        if (File.Exists(getSaveDataPath())) {
            // Load data
            Load();
        }
        else {
            // Create new data
            NewSave();
        }
    }

    /// <summary>
    /// Cverride and delete any old saves, and create a fresh new one.
    /// </summary>
    public static void NewSave () {
        SaveData saveData = new SaveData();
        // Here is where to initialise the variables
        GameSave.INSTANCE = saveData;
        GameSave.Save();
    }

    /// <summary>
    /// Save the current INSTANCE object to persistent storage.
    /// </summary>
    public static void Save () {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(getSaveDataPath());
        bf.Serialize(file, GameSave.INSTANCE);
        file.Close();

        Debug.Log("Progress Saved");
    }

    /// <summary>
    /// Load the saved object from persistent storage, into the variable INSTANCE.
    /// </summary>
    public static void Load () {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(getSaveDataPath(), FileMode.Open);
        GameSave.INSTANCE = (SaveData) bf.Deserialize(file);
        file.Close();
    }

    public static bool IsWeekUnlocked (int week) {
        switch (week) {
            case 1:
                return true; // Week 1 is always unlocked
            case 2:
                return GameSave.INSTANCE.week1Levels.All(levelSaveData => levelSaveData.percentage == 1f);
            case 3:
                return GameSave.INSTANCE.week2Levels.All(levelSaveData => levelSaveData.percentage == 1f);
            default:
                return false;
        }
    }

    public static bool IsDayUnlocked (int week, int day) {
        switch (week) {
            case 1:
                return GameSave.INSTANCE.week1Levels[day - 1].unlocked;
            case 2:
                return GameSave.INSTANCE.week2Levels[day - 1].unlocked;
            case 3:
                return GameSave.INSTANCE.week3Levels[day - 1].unlocked;;
            default:
                return false;
        }
    }
}
