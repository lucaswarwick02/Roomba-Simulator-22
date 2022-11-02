using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileEffect
{
    Invalid,
    SingleDirt,
    DoubleDirt,
    Slippery,
    Battery,
    CatPush,
    Ring
}

public class TilemapManager : MonoBehaviour
{
    public Tilemap floorTilemap;
    public Tilemap effectsTilemap;
    public Tilemap invalidTilemap;

    public Tile singleDirtTile;

    public Vector3Int startPos;

    [HideInInspector] public static bool sliding = false;
    [HideInInspector] public static bool catPush = false;

    [HideInInspector] public Vector3Int currentPos;
    [HideInInspector] public Vector3 offset = new Vector3(-0.5f, -0.5f, 0f);

    public static TilemapManager INSTANCE;

  
    

    private void Start() {
        INSTANCE = this;
        catPush = false;
        sliding = false;
    }

    public void newPos(Vector3Int velocity){
        Vector3Int nextPos = currentPos + velocity;
        if (invalidTilemap.GetTile(nextPos)){
            sliding = false; //stop sliding from slipSquares if you were sliding
            catPush = false; //stop sliding from catPush if you were sliding
        } // This tilemap only has invalid tiles, so just check it's not null
        else{currentPos = nextPos;}
        PlayerMovement.INSTANCE.movePoint.position = currentPos - offset;
    }

    public void ProcessInput (Vector3Int velocity)
    {
        if (effectsTilemap.GetTile(currentPos)) // If we have landed on an "effect" tile (i.e., battery, slippery tile, etc)
        {
            switch (effectsTilemap.GetTile(currentPos).name)
            {
                case "Effects_2":
                    PerformEffect(TileEffect.Slippery, velocity);
                    break;
                case "Effects_6":
                    velocity = new Vector3Int(0, 1, 0);
                    PerformEffect(TileEffect.CatPush, velocity);
                    break;
                case "Effects_7":
                    velocity = new Vector3Int(0, -1, 0);
                    PerformEffect(TileEffect.CatPush, velocity);
                    break;
                case "Effects_8":
                    velocity = new Vector3Int(-1, 0, 0);
                    PerformEffect(TileEffect.CatPush, velocity);
                    break;
                case "Effects_9":
                    velocity = new Vector3Int(1, 0, 0);
                    PerformEffect(TileEffect.CatPush, velocity);
                    break;
                default:
                    Debug.LogError("Tilename not defined");
                    break;
            }
        }

        if (catPush) {
            newPos(velocity);
            ProcessInput(velocity);
            // PerformEffect(TileEffect.CatPush, currentPos, velocity);
        }
        
        if (sliding) {
            sliding = false;
            PlayerMovement.INSTANCE.updateSpeed(6f);
            newPos(velocity);
            ProcessInput(velocity);
        }
    }

    public void PerformEffect (TileEffect tileEffect, Vector3Int velocity)
    {
        // Perform effect depending on "effect" tile type
        switch (tileEffect)
        {
            case TileEffect.Slippery:
            PlayerMovement.INSTANCE.updateSpeed(6f);
                // Player is now sliding
                sliding = true;
                // Move player by velocity
                newPos(velocity);
                ProcessInput(velocity);
                break;
            case TileEffect.CatPush:
            PlayerMovement.INSTANCE.updateSpeed(8f);
                catPush = true;
                newPos(velocity);
                ProcessInput(velocity);
                break;
            default:
                break;
        }
    }

    public void PerformCollection (TileEffect tileEffect) {
        switch (tileEffect)
        {
            case TileEffect.SingleDirt:
                // +1 to dirt counter
                GameState.INSTANCE.IncreasePoints(1);
                // Remove tile
                effectsTilemap.SetTile(currentPos, null);
                // if(sliding || catPush){
                //     sliding = false;
                //     ProcessInput(velocity);
                // }
                break;
            case TileEffect.DoubleDirt:
                // +1 to dirt counter
                GameState.INSTANCE.IncreasePoints(1);
                // Replace with TileEffect.SingleDirt
                effectsTilemap.SetTile(currentPos, singleDirtTile);
                // if(sliding  || catPush){
                //     sliding = false;
                //     ProcessInput(velocity);
                // }
                break;
            case TileEffect.Battery:
                // + 3 to battery counter
                GameState.INSTANCE.IncreaseBattery(3);
                // Remove tile
                effectsTilemap.SetTile(currentPos, null);
                // if(sliding || catPush){
                //     newPos(velocity);
                //     ProcessInput(velocity); 
                // }
                break;
            case TileEffect.Ring:
                Debug.Log("Ring effect");
                GameState.INSTANCE.DecreasePoints(1);
                effectsTilemap.SetTile(currentPos, null);
                break;
            default:
                break;
        }
    }

    public static TileEffect TileNameToEnum (string tileName) {
        switch (tileName)
            {
                case "Effects_0":
                    return TileEffect.SingleDirt;
                case "Effects_1":
                    return TileEffect.DoubleDirt;
                case "Effects_2":
                    return TileEffect.Slippery;
                case "Effects_4":
                    return TileEffect.Battery;
                case "Effects_6":
                    return TileEffect.CatPush;
                case "Effects_5":
                    return TileEffect.Ring;
                case "Effects_7":
                    return TileEffect.CatPush;
                case "Effects_8":
                    return TileEffect.CatPush;
                case "Effects_9":
                    return TileEffect.CatPush;
                default:
                    Debug.LogError("Tilename" + tileName + " not defined");
                    return TileEffect.Invalid;
            }
    }
}
