using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roombaCollider1 : MonoBehaviour
{

    public static roombaCollider1 INSTANCE;
    

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Equals("Effects")) {
            // Roomba has collided with an collection tile
            // Debug.Log(PlayerMovement.INSTANCE.roomba1.transform.position);
            Vector3Int tilePos =  Vector3Int.FloorToInt(PlayerMovement.INSTANCE.roomba1.transform.position);
            
            if (TilemapManager.INSTANCE.effectsTilemap.GetTile(tilePos) == null) return;

            string tileName = TilemapManager.INSTANCE.effectsTilemap.GetTile(tilePos).name;
            // Debug.Log("1" + TilemapManager.TileNameToEnum(tileName));
            TilemapManager.INSTANCE.PerformCollection1(TilemapManager.TileNameToEnum(tileName), tilePos);
        }
    }
  }

