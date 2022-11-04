using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateUI : MonoBehaviour
{
    public TextMeshProUGUI batteryCount;
    public TextMeshProUGUI pointsCount;

    // Update is called once per frame
    void Update()
    {
        batteryCount.text = "Battery: " + GameState.INSTANCE.battery1;
        pointsCount.text = "Points: " + GameState.INSTANCE.points;
    }
}
