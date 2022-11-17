using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int house;
    public int room;

    public Level (int house, int room) {
        this.house = house;
        this.room = room;
    }

    public Level NextLevel () {
        Level nextLevel = new Level(this.house, this.room + 1);

        if (nextLevel.room == 6) {
            nextLevel.house++;
            nextLevel.room = 1;
        }

        return nextLevel;
    }

    public Level PreviousLevel () {
        Level previousLevel = new Level(this.house, this.room - 1);

        if (previousLevel.room == 0) {
            previousLevel.house--;
            previousLevel.room = 1;
        }

        return previousLevel;
    }

    public override string ToString () {
        return "Level_" + this.house + "-" + this.room;
    }
}
