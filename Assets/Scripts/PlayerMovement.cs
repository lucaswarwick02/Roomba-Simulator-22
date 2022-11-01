using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement INSTANCE;

    private void Start() {
        INSTANCE = this;
        UpdatePlayerPosition();

        GameSave.NewSave();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TilemapManager.INSTANCE.ProcessInput(new Vector3Int(0, 1, 0));
            GameState.INSTANCE.DecreaseBattery(1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TilemapManager.INSTANCE.ProcessInput(new Vector3Int(0, -1, 0));
            GameState.INSTANCE.DecreaseBattery(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TilemapManager.INSTANCE.ProcessInput(new Vector3Int(-1, 0, 0));
            GameState.INSTANCE.DecreaseBattery(1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TilemapManager.INSTANCE.ProcessInput(new Vector3Int(1, 0, 0));
            GameState.INSTANCE.DecreaseBattery(1);
        }
    }

    public void UpdatePlayerPosition ()
    {
        transform.position = TilemapManager.INSTANCE.currentPos - TilemapManager.INSTANCE.offset;
    }
}
    

    

