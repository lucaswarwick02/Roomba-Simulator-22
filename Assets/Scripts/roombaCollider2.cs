using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roombaCollider2 : MonoBehaviour
{

    public static roombaCollider2 INSTANCE;
    

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Equals("Effects")) {
            // Roomba has collided with an collection tile
            Vector3Int tilePos =  Vector3Int.FloorToInt(PlayerMovement.INSTANCE.roomba2.transform.position);
            string tileName = TilemapManager.INSTANCE.effectsTilemap.GetTile(tilePos).name;
            TilemapManager.INSTANCE.PerformCollection2(TilemapManager.TileNameToEnum(tileName), tilePos);
        }
    }
  }

