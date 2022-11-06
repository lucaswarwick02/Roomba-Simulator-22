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

    [HideInInspector] public static bool sliding1 = false;
    [HideInInspector] public static bool sliding2 = false;
    [HideInInspector] public static bool catPush1 = false;
    [HideInInspector] public static bool catPush2 = false;

    [HideInInspector] public Vector3Int currentPos1;
    [HideInInspector] public Vector3Int currentPos2;
    [HideInInspector] public Vector3 offset = new Vector3(-0.5f, -0.5f, 0f);

    public static TilemapManager INSTANCE;

    private void Awake() {
        currentPos1 = GameState.INSTANCE.startPos1;
        currentPos2 = GameState.INSTANCE.startPos2;

        INSTANCE = this;
        sliding1 = false;
        sliding2 = false;
        catPush1 = false;
        catPush2 = false;
    }

    public void newPos(Vector3Int velocity1,Vector3Int velocity2){
        bool invalidCheck1 = false;
        bool invalidCheck2 = false;
        Vector3Int nextPosRCheck1 = new Vector3Int(0,0,0);
        Vector3Int nextPosRCheck2 = new Vector3Int(0,0,0);
       
        Vector3Int nextPos1 = currentPos1 + velocity1;
        if (invalidTilemap.GetTile(nextPos1)){
            invalidCheck1 = true;
            sliding1 = false; //stop sliding from slipSquares if you were sliding
            catPush1 = false; //stop sliding from catPush if you were sliding
            nextPosRCheck1 =  currentPos1;
        } // This tilemap only has invalid tiles, so just check it's not null
        else{
            nextPosRCheck1 = nextPos1;}
        Vector3Int nextPos2 = currentPos2 + velocity2;
        if (invalidTilemap.GetTile(nextPos2)){
            invalidCheck2 = true;
            sliding2 = false; //stop sliding from slipSquares if you were sliding
            catPush2 = false; //stop sliding from catPush if you were sliding
            nextPosRCheck2 =  currentPos2;
        } // This tilemap only has invalid tiles, so just check it's not null
        else{
            nextPosRCheck2 = nextPos2;}

        if(nextPosRCheck1 != nextPosRCheck2){
            if(!invalidCheck1){
                currentPos1 = nextPos1;
            }
            if(!invalidCheck2){
                currentPos2 = nextPos2;
            }
        }
        else{
            sliding1 = false; 
            catPush1 = false;
            sliding2 = false; 
            catPush2 = false;
        }

        PlayerMovement.INSTANCE.movePoint1.position = currentPos1 - offset;
        PlayerMovement.INSTANCE.movePoint2.position = currentPos2 - offset;
    }

    public void ProcessInput1 (Vector3Int velocity)
    {
        if (effectsTilemap.GetTile(currentPos1)) // If we have landed on an "effect" tile (i.e., battery, slippery tile, etc)
        {
            switch (effectsTilemap.GetTile(currentPos1).name)
            {
                case "Effects_2":
                    PerformEffect1(TileEffect.Slippery, velocity);
                    break;
                case "Effects_6":
                    velocity = new Vector3Int(0, 1, 0);
                    PerformEffect1(TileEffect.CatPush, velocity);
                    break;
                case "Effects_7":
                    velocity = new Vector3Int(0, -1, 0);
                    PerformEffect1(TileEffect.CatPush, velocity);
                    break;
                case "Effects_8":
                    velocity = new Vector3Int(-1, 0, 0);
                    PerformEffect1(TileEffect.CatPush, velocity);
                    break;
                case "Effects_9":
                    velocity = new Vector3Int(1, 0, 0);
                    PerformEffect1(TileEffect.CatPush, velocity);
                    break;
                default:
                    break;
            }
        }

        if (catPush1) {
            sliding1 = false;
            newPos(velocity, new Vector3Int(0, 0, 0));
            ProcessInput1(velocity);
            // PerformEffect(TileEffect.CatPush, currentPos, velocity);
        }
        
        if (sliding1) {
            sliding1 = false;
            PlayerMovement.INSTANCE.updateSpeed1(6f);
            newPos(velocity, new Vector3Int(0, 0, 0));
            ProcessInput1(velocity);
        }
    }

    public void ProcessInput2 (Vector3Int velocity)
    {
        if (effectsTilemap.GetTile(currentPos2)) // If we have landed on an "effect" tile (i.e., battery, slippery tile, etc)
        {
            switch (effectsTilemap.GetTile(currentPos2).name)
            {
                case "Effects_2":
                    PerformEffect2(TileEffect.Slippery, velocity);
                    break;
                case "Effects_6":
                    velocity = new Vector3Int(0, 1, 0);
                    PerformEffect2(TileEffect.CatPush, velocity);
                    break;
                case "Effects_7":
                    velocity = new Vector3Int(0, -1, 0);
                    PerformEffect2(TileEffect.CatPush, velocity);
                    break;
                case "Effects_8":
                    velocity = new Vector3Int(-1, 0, 0);
                    PerformEffect2(TileEffect.CatPush, velocity);
                    break;
                case "Effects_9":
                    velocity = new Vector3Int(1, 0, 0);
                    PerformEffect2(TileEffect.CatPush, velocity);
                    break;
                default:
                    break;
            }
        }

        if (catPush2) {
            sliding2 = false;
            newPos(new Vector3Int(0, 0, 0),velocity);
            ProcessInput2(velocity);
            // PerformEffect(TileEffect.CatPush, currentPos, velocity);
        }
        
        if (sliding2) {
            sliding2 = false;
            PlayerMovement.INSTANCE.updateSpeed2(6f);
            newPos(new Vector3Int(0, 0, 0),velocity);
            ProcessInput2(velocity);
        }
    }


    public void PerformEffect1 (TileEffect tileEffect2, Vector3Int velocity)
    {
        // Perform effect depending on "effect" tile type
        switch (tileEffect2)
        {
            case TileEffect.Slippery:
            PlayerMovement.INSTANCE.updateSpeed1(6f);
                // Player is now sliding
                sliding1 = true;
                // Move player by velocity
                newPos(velocity, new Vector3Int(0, 0, 0));
                ProcessInput1(velocity);
                break;
            case TileEffect.CatPush:
            PlayerMovement.INSTANCE.updateSpeed1(8f);
                catPush1 = true;
                newPos(velocity, new Vector3Int(0, 0, 0));
                ProcessInput1(velocity);
                break;
            default:
                break;
        }
    }
    public void PerformEffect2 (TileEffect tileEffect2, Vector3Int velocity)
    {
        // Perform effect depending on "effect" tile type
        switch (tileEffect2)
        {
            case TileEffect.Slippery:
            PlayerMovement.INSTANCE.updateSpeed2(6f);
                // Player is now sliding
                sliding2 = true;
                // Move player by velocity
                newPos(new Vector3Int(0, 0, 0),velocity);
                ProcessInput2(velocity);
                break;
            case TileEffect.CatPush:
            PlayerMovement.INSTANCE.updateSpeed2(8f);
                catPush2 = true;
                newPos(new Vector3Int(0, 0, 0),velocity);
                ProcessInput2(velocity);
                break;
            default:
                break;
        }
    }

    public void PerformCollection1 (TileEffect tileEffect2, Vector3Int tilePos) {
        switch (tileEffect2)
        {
            case TileEffect.SingleDirt:
                GameState.INSTANCE.Dirt += 1;
                effectsTilemap.SetTile(tilePos, null);
                break;
            case TileEffect.DoubleDirt:
                GameState.INSTANCE.Dirt += 1;
                effectsTilemap.SetTile(tilePos, singleDirtTile);
                break;
            case TileEffect.Battery:
                GameState.INSTANCE.Battery1 += 3;
                effectsTilemap.SetTile(tilePos, null);
                break;
            case TileEffect.Ring:
                GameState.INSTANCE.Rings += 1;
                effectsTilemap.SetTile(tilePos, null);
                break;
            default:
                break;
        }
    }
    public void PerformCollection2 (TileEffect tileEffect2, Vector3Int tilePos) {
        switch (tileEffect2)
        {
            case TileEffect.SingleDirt:
                GameState.INSTANCE.Dirt += 1;
                effectsTilemap.SetTile(tilePos, null);
                break;
            case TileEffect.DoubleDirt:
                GameState.INSTANCE.Dirt += 1;
                effectsTilemap.SetTile(tilePos, singleDirtTile);
                break;
            case TileEffect.Battery:
                GameState.INSTANCE.Battery2 += 3;
                effectsTilemap.SetTile(tilePos, null);
                break;
            case TileEffect.Ring:
                GameState.INSTANCE.Rings += 1;
                effectsTilemap.SetTile(tilePos, null);
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
                case "Effects_5":
                    return TileEffect.Ring;
                case "Effects_6":
                    return TileEffect.CatPush;
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
