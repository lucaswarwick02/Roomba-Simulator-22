using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int initialBattery1 = 10;
    public int initialBattery2 = 10;
    
    public int pointsNeeded = 3;

    [HideInInspector] public int battery1 = 0;
    [HideInInspector] public int battery2 = 0;
    [HideInInspector] public int points = 0;

    public static GameState INSTANCE;

    // Start is called before the first frame update
    void Start()
    {
        INSTANCE = this;
        battery1 = initialBattery1;
        battery2 = initialBattery2;
    }

    public void DecreaseBattery (int amount) {
        battery1 -= amount;
        battery2 -= amount;
        checkGameState();
    }
    public void IncreaseBattery1 (int amount) {
        battery1 += amount;
        checkGameState();
    }
    public void IncreaseBattery2 (int amount) {
        battery2 += amount;
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
        if (battery1 <= 0 & battery2 <= 0) GameLose();

        if (points >= pointsNeeded) GameWin();
    }

    public void GameWin () {
        Debug.Log("You win!");
    }

    public void GameLose () {
        Debug.Log("You lose!");
    }
}
