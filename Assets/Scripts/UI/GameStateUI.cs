using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateUI : MonoBehaviour
{
    public TextMeshProUGUI batteryCount;
    public TextMeshProUGUI dirtCount;
    public TextMeshProUGUI ringsCount;

    // Update is called once per frame
    void Update()
    {
        batteryCount.text = "Battery: " + GameState.INSTANCE.Battery;
        dirtCount.text = "Dirt: " + GameState.INSTANCE.Dirt;
        ringsCount.text = "Rings: " + GameState.INSTANCE.Rings;
    }
}
