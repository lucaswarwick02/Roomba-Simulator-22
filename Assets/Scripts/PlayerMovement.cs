using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement INSTANCE;

    [HideInInspector] public GameObject roomba1;
    [HideInInspector] public GameObject roomba2;

    private Vector3 offset2 = new Vector3(-0.5f, -0.5f, 0f);



    private float movespeed1;
    private float movespeed2;
    [HideInInspector] public Transform movePoint1;
    [HideInInspector] public Transform movePoint2; //This is what gets moved instantly and the roomba sprite follows non instantly causing animation

    private bool wfe; //Wait For Effect, specifically if the square is due for cat push (cannot move until you let cat push roomba)

    private Vector3Int movementDir; //vector follows what key is last pressed and saved in a variable (for slip squares)

    private bool allowInput = true;

    public void OnMove (InputValue input) {

        if (!allowInput) return;

        Vector2 inputVec = input.Get<Vector2>();
        wfe = true;
        movementDir = new Vector3Int(Mathf.RoundToInt(inputVec.x), Mathf.RoundToInt(inputVec.y), 0);
        
        if ((GameState.INSTANCE.Battery1 > 0) && (GameState.INSTANCE.Battery2 > 0))
        {
            TilemapManager.INSTANCE.newPos(movementDir, movementDir);
        }
        else if ((GameState.INSTANCE.Battery1 > 0))
        {
            TilemapManager.INSTANCE.newPos(movementDir, Vector3Int.zero);
        }
        else if ((GameState.INSTANCE.Battery2 > 0))
        {
            TilemapManager.INSTANCE.newPos(Vector3Int.zero, movementDir);
        }

        GameState.INSTANCE.Battery1--;
        GameState.INSTANCE.Battery2--;
    }

    private void Awake()
    {
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
        roomba1.transform.position = GameState.INSTANCE.startPos1 - offset2;
        roomba2.transform.position = GameState.INSTANCE.startPos2 - offset2;
        movePoint1.position = roomba1.transform.position;
        movePoint2.position = roomba2.transform.position;
    }

    public bool isMoving()
    {
        return (Vector3.Distance(roomba1.transform.position, movePoint1.position) != 0f) || (Vector3.Distance(roomba2.transform.position, movePoint2.position) != 0f);
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
                    allowInput = true;
                }
                else if ((GameState.INSTANCE.Battery1 > 0))
                {
                    allowInput = true;
                }
                else if ((GameState.INSTANCE.Battery2 > 0))
                {
                    allowInput = true;
                }
                else
                {
                    allowInput = false;
                }
            }

        }
        else {
            allowInput = false;
        }
    }
}




