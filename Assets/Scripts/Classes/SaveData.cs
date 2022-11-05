using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Variable which wil contain the data in between levels, and for saving/loading the data.
/// </summary>
[System.Serializable]
public class SaveData {
    public bool week1Unlocked = true;
    public bool week2Unlocked = false;
    public bool week3Unlocked = false;

    public bool[] week1LevelsUnlocked = {true, false, false, false, false};
    public bool[] week2LevelsUnlocked = {false, false, false, false, false};
    public bool[] week3LevelsUnlocked = {false, false, false, false, false};
}