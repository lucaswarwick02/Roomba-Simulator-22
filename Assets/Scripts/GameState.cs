using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public static GameState INSTANCE;

    public int initialBattery1 = 10;
    public int initialBattery2 = 10;
    public int maxDirt = 3;
    public Level level;

    public Vector3Int startPos1 = new Vector3Int(0, 0, 0);
    public Vector3Int startPos2 = new Vector3Int(-100, 0, 0);

    private UnityEvent onStateChange = new UnityEvent();

    private int _battery1;
    public int Battery1 {
        get { return _battery1; }
        set { _battery1 = value; onStateChange.Invoke(); }
    }

    private int _battery2;
    public int Battery2 {
        get { return _battery2; }
        set { _battery2 = value; onStateChange.Invoke(); }
    }

    private int _dirt;
    public int Dirt {
        get { return _dirt; }
        set { _dirt = value; onStateChange.Invoke(); }
    }

    private int _rings;
    public int Rings {
        get { return _rings; }
        set { _rings = value; onStateChange.Invoke(); }
    }

    private void Awake() {
        INSTANCE = this;
        Battery1 = initialBattery1;
        Battery2 = initialBattery2;
        
        onStateChange.AddListener(checkGameStatus);
    }  


    private void checkGameStatus () {
        // Stop function if the game is still runnings
        bool isGameOver = false;

        if (Battery1 <= 0 && Battery2 <= 0) {
            // Both Roombas have died! (Lose)
            isGameOver = true;
        }
        if (Dirt >= maxDirt) {
            // They have collected all the dirt! (Win)
            isGameOver = true;
        }

        if (!isGameOver) return;

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
            Camera.main.backgroundColor = Color.green;
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
        else {
            Camera.main.backgroundColor = Color.red;
        }
    }
}
