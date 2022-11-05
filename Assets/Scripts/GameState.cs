using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public Vector3Int startPos;
    public int initialBattery = 10;
    public int maxDirt = 3;
    public Level level;

    private int _battery;
    public int Battery {
        get { return _battery; }
        set { _battery = value; onStateChange.Invoke(); }
    }

    private int _rings;
    public int Rings {
        get { return _rings; }
        set { _rings = value; onStateChange.Invoke(); }
    }

    private int _dirt;
    public int Dirt {
        get { return _dirt; }
        set { _dirt = value; onStateChange.Invoke(); }
    }

    private UnityEvent onStateChange = new UnityEvent();

    public static GameState INSTANCE;

    private void Awake() {
        INSTANCE = this;
        Battery = initialBattery;
        
        onStateChange.AddListener(checkGameStatus);
    }

    private void checkGameStatus () {
        // Stop function if the game is still runnings
        if (Battery < 0) return;
        if (Dirt < maxDirt) return;

        float score = ((float) Dirt - (float) Rings) / (float) maxDirt;
        Debug.Log("Score = " + score);

        // Assign score to level save data
        switch (level.week) {
            case 1:
                if (GameSave.INSTANCE.week1Levels[level.day - 1].percentage < score) GameSave.INSTANCE.week1Levels[level.day - 1].percentage = score;
                break;
            case 2:
                if (GameSave.INSTANCE.week2Levels[level.day - 1].percentage < score) GameSave.INSTANCE.week2Levels[level.day - 1].percentage = score;
                break;
            case 3:
                if (GameSave.INSTANCE.week3Levels[level.day - 1].percentage < score) GameSave.INSTANCE.week3Levels[level.day - 1].percentage = score;
                break;
            default:
                break;
        }

        if (score >= 0.5f) {
            // Unlock next level
            Level nextLevel = level.NextLevel();
            switch (nextLevel.week) {
                case 1:
                    GameSave.INSTANCE.week1Levels[nextLevel.day - 1].unlocked = true;
                    break;
                case 2:
                    GameSave.INSTANCE.week2Levels[nextLevel.day - 1].unlocked = true;
                    break;
                case 3:
                    GameSave.INSTANCE.week3Levels[nextLevel.day - 1].unlocked = true;
                    break;
                default:
                    break;
            }
        }
    }
}
