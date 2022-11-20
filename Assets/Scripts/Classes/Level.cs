using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int house;
    public int room;

    /// <summary>
    /// Used for calculating levels.
    /// </summary>
    /// <param name="house"> House number</param>
    /// <param name="room">Room number</param>
    public Level (int house, int room) {
        this.house = house;
        this.room = room;
    }

    /// <summary>
    /// Calculate the level information for the next level.
    /// </summary>
    /// <returns>Next Level</returns>
    public Level NextLevel () {
        Level nextLevel = new Level(this.house, this.room + 1);

        if (nextLevel.room == 6) {
            nextLevel.house++;
            nextLevel.room = 1;
        }

        return nextLevel;
    }

    /// <summary>
    /// Calculate the level information for the previous level.
    /// </summary>
    /// <returns>Previous Level</returns>
    public Level PreviousLevel () {
        Level previousLevel = new Level(this.house, this.room - 1);

        if (previousLevel.room == 0) {
            previousLevel.house--;
            previousLevel.room = 1;
        }

        return previousLevel;
    }

    /// <summary>
    /// Get the scene name of the level.
    /// </summary>
    /// <returns>Scene name for the level</returns>
    public override string ToString () {
        return "Level_" + this.house + "-" + this.room;
    }
}
