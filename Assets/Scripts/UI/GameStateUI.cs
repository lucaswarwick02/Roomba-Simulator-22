using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStateUI : MonoBehaviour
{
    public TextMeshProUGUI batteryCount;
    public TextMeshProUGUI roomba1Text;
    public TextMeshProUGUI roomba2Text;
    public TextMeshProUGUI batteryCount2;

    public TextMeshProUGUI dirtCount;
    public TextMeshProUGUI ringsCount;

    private Vector3Int check = new Vector3Int(-100,0,0);

    // Update is called once per frame
    void Update()
    {
        if(GameState.INSTANCE.Battery1 <= 0){
            batteryCount.text = "Battery Life: " + 0;
        }
        else{
            batteryCount.text = "Battery Life: " + GameState.INSTANCE.Battery1;
        }
        if(GameState.INSTANCE.Battery2 <= 0){
            batteryCount2.text = "Battery Life: " + 0;
        }
        else{
            batteryCount2.text = "Battery Life: " + GameState.INSTANCE.Battery2;
        }

        dirtCount.text = "Dirt Left: " + GameState.INSTANCE.Dirt;
        ringsCount.text = "Rings Vacuumed: " + GameState.INSTANCE.Rings;

        if(GameState.INSTANCE.startPos2 == check){
            batteryCount2.text = "";
            roomba1Text.text = "";
            roomba2Text.text = "";
        }
    }
    public void BackToMainMenu () {
        SceneManager.LoadScene("MainMenu");
    }
}
