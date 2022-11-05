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

        if (score >= 0.5f) {
            GameWin();
        }
        else {
            GameLose();
        }
    }

    private void GameLose () {
        Debug.Log("Game Lost!");
    }

    private void GameWin () {
        Debug.Log("Game Win!");

        // Unlock next level
        Level nextLevel = level.NextLevel();
        switch (nextLevel.week) {
            case 1:
                GameSave.INSTANCE.week1LevelsUnlocked[nextLevel.day - 1] = true;
                break;
            case 2:
                GameSave.INSTANCE.week2LevelsUnlocked[nextLevel.day - 1] = true;
                break;
            case 3:
                GameSave.INSTANCE.week3LevelsUnlocked[nextLevel.day - 1] = true;
                break;
            default:
                break;
        }

        GameSave.Save();
    }
}
