using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateUI : MonoBehaviour
{
    public TextMeshProUGUI batteryCount;

    public TextMeshProUGUI roomba2Text;
    public TextMeshProUGUI batteryCount2;

    public TextMeshProUGUI pointsCount;

    private Vector3Int check = new Vector3Int(-100,0,0);

    // Update is called once per frame
    void Update()
    {
        if(GameState.INSTANCE.battery1 <= 0){
            batteryCount.text = "Battery: " + 0;
        }
        else{
            batteryCount.text = "Battery: " + GameState.INSTANCE.battery1;
        }
        if(GameState.INSTANCE.battery2 <= 0){
            batteryCount2.text = "Battery: " + 0;
        }
        else{
            batteryCount2.text = "Battery: " + GameState.INSTANCE.battery2;
        }
        pointsCount.text = "Points: " + GameState.INSTANCE.points;

        if(PlayerMovement.INSTANCE.nd2 == check){
            batteryCount2.text = "";
            roomba2Text.text = "";
        }
    }
}
