using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateUI : MonoBehaviour
{
    public TilemapMovement tilemapMovement;

    public TextMeshProUGUI batteryCount;
    public TextMeshProUGUI pointsCount;

    // Update is called once per frame
    void Update()
    {
        batteryCount.text = "Battery: " + tilemapMovement.battery;
        pointsCount.text = "Points: " + tilemapMovement.points;
    }
}
