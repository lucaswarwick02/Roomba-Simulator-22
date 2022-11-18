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

    private Color dead = new Color(0.5377358f, 0.5377358f, 0.5377358f);
    private Color red = new Color(0.8f, 0f, 0.1f);
    private Color orange = new Color(0.8f, 0.6f, 0.2f);
    private Color green = new Color(0.4f, 0.8f, 0.2f);


    public TextMeshProUGUI dirtCount;
    public TextMeshProUGUI ringsCount;

    private Vector3Int check = new Vector3Int(-100,0,0);

    public TextMeshProUGUI levelID;

    private void Start() {
        levelID.text = GameState.INSTANCE.level.house + "-" + GameState.INSTANCE.level.room;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.INSTANCE.Battery1 <= 0){
            batteryCount.text = "Battery Life: " + 0;
            batteryCount.color = dead;
        }
        else{
            batteryCount.text = "Battery Life: " + GameState.INSTANCE.Battery1;
        }
        if(GameState.INSTANCE.Battery1 == 1){
            batteryCount.color = red;
        }
        if(GameState.INSTANCE.Battery1 <= 3 && GameState.INSTANCE.Battery1 > 1){
            batteryCount.color = orange;
        }
        if(GameState.INSTANCE.Battery1 > 3){
            batteryCount.color = green;

        }
        if(GameState.INSTANCE.Battery2 <= 0){
            batteryCount2.text = "Battery Life: " + 0;
            batteryCount2.color = dead;
        }
        else{
            batteryCount2.text = "Battery Life: " + GameState.INSTANCE.Battery2;
        }
        if(GameState.INSTANCE.Battery2 == 1){
            batteryCount2.color = red;
        }
        if(GameState.INSTANCE.Battery2 <= 3 && GameState.INSTANCE.Battery2 > 1){
            batteryCount2.color = orange;
        }
        if(GameState.INSTANCE.Battery2 > 3){
            batteryCount2.color = green;

        }
       
        dirtCount.text = "Dirt Left: " + GameState.INSTANCE.Dirt;
        ringsCount.text = "Rings Vacuumed: " + GameState.INSTANCE.Rings;

        if(GameState.INSTANCE.startPos2 == check){
            batteryCount2.text = "";
            // roomba1Text.text = "";
            roomba2Text.text = "";
        }
    }

    public void BackToMainMenu () {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel () {
        SceneManager.LoadScene("Level_" + GameState.INSTANCE.level.house + "-" + GameState.INSTANCE.level.room);
    }
}
