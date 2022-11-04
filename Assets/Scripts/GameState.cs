using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public Vector3Int startPos;
    public int initialBattery = 10;
    public int maxDirt = 3;

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
    }
}
