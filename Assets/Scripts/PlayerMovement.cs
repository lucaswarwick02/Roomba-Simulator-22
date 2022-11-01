using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement INSTANCE;

    private Vector3Int nd = new Vector3Int(0,0,0);

    private float movespeed;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

    private bool b;

    private Vector3Int mv;

    private Vector3Int vel;

    private void Start() {
        b = true;
        INSTANCE = this;
        movePoint.parent = null;
        movePoint.position = transform.position - TilemapManager.INSTANCE.offset;
        // transform.position = TilemapManager.INSTANCE.currentPos - TilemapManager.INSTANCE.offset;
        UpdatePlayerPosition(nd);
    }

    public bool isMoving(){
        if(Vector3.Distance(transform.position, movePoint.position) != 0f){
            return true;
        }
        else{
            return false;
        }
    }

    public void updateSpeed(float speed){
        movespeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
                movePoint.position,
                movespeed * Time.deltaTime);

        if(!isMoving()){
            if(b){
                b = false;
                TilemapManager.INSTANCE.ProcessInput(mv);
            }
            else{
        updateSpeed(3f);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            b = true;
            mv = new Vector3Int(0, 1, 0);
            TilemapManager.INSTANCE.newPos(new Vector3Int(0, 1, 0));
            GameState.INSTANCE.DecreaseBattery(1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            b = true;
            mv = new Vector3Int(0, -1, 0);
            TilemapManager.INSTANCE.newPos(new Vector3Int(0, -1, 0));
            GameState.INSTANCE.DecreaseBattery(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            b = true;
            mv = new Vector3Int(-1, 0, 0);
            TilemapManager.INSTANCE.newPos(new Vector3Int(-1,0, 0));
            GameState.INSTANCE.DecreaseBattery(1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            b = true;
            mv = new Vector3Int(1, 0, 0);
            TilemapManager.INSTANCE.newPos(new Vector3Int(1, 0, 0));
            GameState.INSTANCE.DecreaseBattery(1);
        }
        }
        }

    }

    public void UpdatePlayerPosition (Vector3Int velocity)
    {
        transform.position = TilemapManager.INSTANCE.currentPos - TilemapManager.INSTANCE.offset;
        // transform.position = Vector3.MoveTowards(TilemapManager.INSTANCE.currentPos, 
        // TilemapManager.INSTANCE.currentPos,
        // movespeed * Time.deltaTime);
    }
}
    

    

