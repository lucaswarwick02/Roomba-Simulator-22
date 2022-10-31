using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int initialBattery = 10;

    [HideInInspector] public int battery = 0;
    [HideInInspector] public int points = 0;

    public static GameState INSTANCE;

    // Start is called before the first frame update
    void Start()
    {
        INSTANCE = this;
        battery = initialBattery;
    }

    public void DecreaseBattery (int amount) {
        battery -= amount;
    }
    public void IncreaseBattery (int amount) {
        battery += amount;
    }

    public void DecreasePoints (int amount) {
        points -= amount;
    }
    public void IncreasePoints (int amount) {
        points += amount;
    }
}
