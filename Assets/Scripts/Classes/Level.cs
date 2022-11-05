using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int week;
    public int day;

    public Level (int week, int day) {
        this.week = week;
        this.day = day;
    }

    public Level NextLevel () {
        Level nextLevel = new Level(this.week, this.day + 1);

        if (nextLevel.day == 6) {
            nextLevel.week++;
            nextLevel.day = 1;
        }

        return nextLevel;
    }

    public override string ToString () {
        return "Level_" + this.week + "-" + this.day;
    }
}
