using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement INSTANCE;

    private Vector3Int nd = new Vector3Int(0, 0, 0);
    public Vector3Int nd2 = new Vector3Int(-100, 0, 0);

    [HideInInspector] public GameObject roomba1;
    [HideInInspector] public GameObject roomba2;

    private Vector3 offset2 = new Vector3(-0.5f, -0.5f, 0f);

    private float movespeed1;
    private float movespeed2;
    [HideInInspector] public Transform movePoint1;
    [HideInInspector] public Transform movePoint2; //This is what gets moved instantly and the roomba sprite follows non instantly causing animation

    private bool wfe; //Wait For Effect, specifically if the square is due for cat push (cannot move until you let cat push roomba)

    private Vector3Int movementDir; //vector follows what key is last pressed and saved in a variable (for slip squares)

    private void Awake() {
        INSTANCE = this;

        // Grab references to the roobas
        roomba1 = GameObject.FindWithTag("roomba1");
        roomba2 = GameObject.FindWithTag("roomba2");

        // Grab references to move points...
        movePoint1 = roomba1.transform.GetChild(0);
        movePoint2 = roomba2.transform.GetChild(0);
        // ...Then set parents to null
        movePoint1.parent = null;
        movePoint2.parent = null;

        movespeed1 = 3f;
        movespeed2 = 3f;
    }

    private void Start()
    {
        // TilemapManager.INSTANCE.newPos(nd, nd);

        roomba1.transform.position = nd - offset2;
        roomba2.transform.position = nd - offset2 + nd2;
        movePoint1.position = roomba1.transform.position;
        movePoint2.position = roomba2.transform.position;
        // Debug.Log(roomba1.transform.position);
        // Debug.Log(roomba2.transform.position);
        UpdatePlayerPositions(nd);
    }

    public bool isMoving()
    {
        if ((Vector3.Distance(roomba1.transform.position, movePoint1.position) != 0f)
        || (Vector3.Distance(roomba2.transform.position, movePoint2.position) != 0f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void updateSpeed1(float speed)
    {
        movespeed1 = speed;
    }
    public void updateSpeed2(float speed)
    {
        movespeed2 = speed;
    }

    // Update is called once per frame
    void Update()
    {
        roomba1.transform.position = Vector3.MoveTowards(roomba1.transform.position,
                movePoint1.position,
                movespeed1 * Time.deltaTime);
        roomba2.transform.position = Vector3.MoveTowards(roomba2.transform.position,
                movePoint2.position,
                movespeed2 * Time.deltaTime);

        if (!isMoving())
        { // cant have tile effects act until roomba stops moving
            if (wfe)
            {
                wfe = false;
                TilemapManager.INSTANCE.ProcessInput1(movementDir);
                TilemapManager.INSTANCE.ProcessInput2(movementDir);
            }
            else
            {
                updateSpeed1(3f);
                updateSpeed2(3f);

                if ((GameState.INSTANCE.Battery1 > 0) && (GameState.INSTANCE.Battery2 > 0))
                {

                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(0, 1, 0);
                        TilemapManager.INSTANCE.newPos(movementDir, movementDir);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;

                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(0, -1, 0);
                        TilemapManager.INSTANCE.newPos(movementDir, movementDir);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(-1, 0, 0);
                        TilemapManager.INSTANCE.newPos(movementDir, movementDir);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(1, 0, 0);
                        TilemapManager.INSTANCE.newPos(movementDir, movementDir);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;

                    }

                }
                else if ((GameState.INSTANCE.Battery1 > 0))
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(0, 1, 0);
                        TilemapManager.INSTANCE.newPos(movementDir, nd);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;

                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(0, -1, 0);
                        TilemapManager.INSTANCE.newPos(movementDir, nd);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;

                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(-1, 0, 0);
                        TilemapManager.INSTANCE.newPos(movementDir, nd);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;

                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(1, 0, 0);
                        TilemapManager.INSTANCE.newPos(movementDir, nd);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;

                    }


                }
                else if ((GameState.INSTANCE.Battery2 > 0))
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(0, 1, 0);
                        TilemapManager.INSTANCE.newPos(nd, movementDir);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;

                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(0, -1, 0);
                        TilemapManager.INSTANCE.newPos(nd, movementDir);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;

                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(-1, 0, 0);
                        TilemapManager.INSTANCE.newPos(nd, movementDir);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;

                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        wfe = true;
                        movementDir = new Vector3Int(1, 0, 0);
                        TilemapManager.INSTANCE.newPos(nd, movementDir);
                        GameState.INSTANCE.Battery1--;
                        GameState.INSTANCE.Battery2--;

                    }


                }
            }

        }

    }

    private void UpdatePlayerPositions(Vector3Int velocity)
    {
        // roomba1.transform.position = nd - offset2;
        // roomba2.transform.position = nd - offset2;
        // roomba1.transform.position = TilemapManager.INSTANCE.currentPos1 - offset2 ;
        // roomba2.transform.position = TilemapManager.INSTANCE.currentPos2 - offset2;
    }

}




