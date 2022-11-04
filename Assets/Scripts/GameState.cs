using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int initialBattery = 10;
    public int pointsNeeded = 3;

    [HideInInspector] public int battery = 0;
    [HideInInspector] public int points = 0;

    public Vector3Int startPos;

    public static GameState INSTANCE;

    private void Awake() {
        INSTANCE = this;
        battery = initialBattery;
    }

    public void DecreaseBattery (int amount) {
        battery -= amount;
        checkGameState();
    }
    public void IncreaseBattery (int amount) {
        battery += amount;
        checkGameState();
    }

    public void DecreasePoints (int amount) {
        points -= amount;
        checkGameState();
    }
    public void IncreasePoints (int amount) {
        points += amount;
        checkGameState();
    }

    private void checkGameState () {
        if (battery <= 0) GameLose();

        if (points >= pointsNeeded) GameWin();
    }

    public void GameWin () {
        Debug.Log("You win!");
    }

    public void GameLose () {
        Debug.Log("You lose!");
    }
}
