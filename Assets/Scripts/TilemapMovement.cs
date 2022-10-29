using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileEffect
{
    SingleDirt,
    DoubleDirt,
    Slippery,
    Battery
}

public class TilemapMovement : MonoBehaviour
{
    public Tilemap floorTilemap;
    public Tilemap effectsTilemap;
    public Tilemap invalidTilemap;
    public Vector3Int startPos;
    public Tile singleDirtTile;

    public int batteryCount = 0;
    public int dirtCollected = 0;

    public static bool sliding = false;

    private Vector3Int currentPos;
    private Vector3 offset = new Vector3(-0.5f, -0.5f, 0f);

    private void Start() {
        transform.position = startPos - offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ProcessInput(new Vector3Int(0, 1, 0));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ProcessInput(new Vector3Int(0, -1, 0));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ProcessInput(new Vector3Int(-1, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ProcessInput(new Vector3Int(1, 0, 0));
        }
    }

    private void updatePlayerPosition ()
    {
        transform.position = currentPos - offset;
    }

    public void ProcessInput (Vector3Int velocity)
    {
        Vector3Int nextPos = currentPos + velocity;

        if (invalidTilemap.GetTile(nextPos)) return; // This tilemap only has invalid tiles, so just check it's not null

        // Update the Roomba's position
        currentPos = nextPos;
        updatePlayerPosition();

        if (effectsTilemap.GetTile(currentPos)) // If we have landed on an "effect" tile (i.e., battery, slippery tile, etc)
        {
            switch (effectsTilemap.GetTile(currentPos).name)
            {
                case "Effects_0":
                    PerformEffect(TileEffect.SingleDirt, currentPos, velocity);
                    break;
                case "Effects_1":
                    PerformEffect(TileEffect.DoubleDirt, currentPos, velocity);
                    break;
                case "Effects_2":
                    PerformEffect(TileEffect.Slippery, currentPos, velocity);
                    break;
                case "Effects_3":
                    PerformEffect(TileEffect.Battery, currentPos, velocity);
                    break;
                default:
                    break;
        
            }
            if(sliding){
                    sliding = false;
                    ProcessInput(velocity);
                }
        }
    }

        public void PerformEffect (TileEffect tileEffect, Vector3Int tilePosition, Vector3Int velocity)
    {
        // Perform effect depending on "effect" tile type
        switch (tileEffect)
        {
            case TileEffect.SingleDirt:
                // +1 to dirt counter
                dirtCollected++;
                // Remove tile
                effectsTilemap.SetTile(tilePosition, null);
                if(sliding){
                    sliding = false;
                    ProcessInput(velocity);
                }
                break;
            case TileEffect.DoubleDirt:
                // +1 to dirt counter
                dirtCollected++;
                // Replace with TileEffect.SingleDirt
                effectsTilemap.SetTile(tilePosition, singleDirtTile);
                if(sliding){
                    sliding = false;
                    ProcessInput(velocity);
                }
                break;
            case TileEffect.Slippery:
                // Player is now sliding
                sliding = true;
                // Move player by velocity
                ProcessInput(velocity);
                break;
            case TileEffect.Battery:
                // +1 to battery counter
                batteryCount++;
                // Remove tile
                effectsTilemap.SetTile(tilePosition, null);
                if(sliding){
                    sliding = false;
                    ProcessInput(velocity);
                }
                break;
        }
    }
}
    

    

