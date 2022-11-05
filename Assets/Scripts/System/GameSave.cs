using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSave
{
    /// <summary>
    /// Edit this variable's data during game, and can save and load
    /// it using the static variables Save() and Load() respectively.
    /// </summary>
    public static SaveData INSTANCE;

    /// <summary>
    /// Cverride and delete any old saves, and create a fresh new one.
    /// </summary>
    public static void NewSave () {
        Debug.Log("New Save Data Created");
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
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.sav");
        bf.Serialize(file, GameSave.INSTANCE);
        file.Close();

        Debug.Log("Progress Saved");
    }

    /// <summary>
    /// Load the saved object from persistent storage, into the variable INSTANCE.
    /// </summary>
    public static void Load () {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.sav", FileMode.Open);
        GameSave.INSTANCE = (SaveData) bf.Deserialize(file);
        file.Close();

        Debug.Log("Progress Loaded");
    }
}
