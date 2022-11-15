using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameState : MonoBehaviour
{
    public static GameState INSTANCE;

    public GameProgress gameProgress;
    public Settings settings;

    public int initialBattery1 = 10;
    public int initialBattery2 = 10;
    public int maxDirt = 3;
    public Level level;
    public double passScore;

    public CompletionPanel completionPanel;

    [SerializeField] public AudioSource roombaMoveSound;
    [SerializeField] public AudioSource roombaDiesSound;

    [SerializeField] public AudioSource batterySound;
    [SerializeField] public AudioSource dirtSound;
    [SerializeField] public AudioSource ringSound;
    [SerializeField] public AudioSource slipSound;
    [SerializeField] public AudioSource catPushSound;
    [SerializeField] public AudioSource bumpWallSound;
    [SerializeField] public AudioSource bumpRoombaSound;

    public Vector3Int startPos1 = new Vector3Int(0, 0, 0);
    public Vector3Int startPos2 = new Vector3Int(-100, 0, 0);

    private bool gameOver = false;
    private bool batteryCheck1 = false;
    private bool batteryCheck2 = false;

    private int _battery1;
    private Vector3Int check = new Vector3Int(-100,0,0);
    public int Battery1 {
        get { return _battery1; }
        set { _battery1 = value; }
    }

    private int _battery2;
    public int Battery2 {
        get { return _battery2; }
        set { _battery2 = value; }
    }

    private int _dirt;
    public int Dirt {
        get { return _dirt; }
        set { _dirt = value; }
    }

    private int _rings;
    public int Rings {
        get { return _rings; }
        set { _rings = value; }
    }

    private void Awake() {
        INSTANCE = this;
        Battery1 = initialBattery1;
        Battery2 = initialBattery2;
    }  

    private void Start() {
        passScore = Math.Ceiling((double) (maxDirt/2));
        Dirt = maxDirt;
        // Change scale of camera to match settings
        Camera.main.orthographicSize  = settings.largeScale ? 5f : 10f;
    }

    private void Update() {
        if (!gameOver) checkGameStatus();

        if (!PlayerMovement.INSTANCE.isMoving()) {
        if(!batteryCheck1){
            if (_battery1 <= 0){
                batteryCheck1 = true;
                roombaDiesSound.Play();
            }
        }
        }
        if(startPos2 != check){
        if (!PlayerMovement.INSTANCE.isMoving()) {
        if(!batteryCheck2){
            if (_battery2 <= 0){
                batteryCheck2 = true;
                roombaDiesSound.Play();
            }
        }
      }  
    }
    }


    private void checkGameStatus () {
        // Stop function if the game is still runnings
        bool isGameOver = false;

        if (Battery1 <= 0 && Battery2 <= 0) {
            // Both Roombas have died! (Lose)
            isGameOver = true;
        }
        if (Dirt <= 0) {
            // They have collected all the dirt! (Win)
            isGameOver = true;
        }

        if (PlayerMovement.INSTANCE.isMoving()) {
            isGameOver = false;
        }

        if (!isGameOver) {
            return;
        }
        else {
            
            gameOver = true;
        }

        float score = (((float) maxDirt - (float) Dirt) - (float) Rings);
        Debug.Log(score);

        // Assign score to level save data
        switch (level.week) {
            case 1:
                if (gameProgress.week1Levels[level.day - 1].percentage < score) gameProgress.week1Levels[level.day - 1].percentage = score;
                break;
            case 2:
                if (gameProgress.week2Levels[level.day - 1].percentage < score) gameProgress.week2Levels[level.day - 1].percentage = score;
                break;
            case 3:
                if (gameProgress.week3Levels[level.day - 1].percentage < score) gameProgress.week3Levels[level.day - 1].percentage = score;
                break;
            default:
                break;
        }

        if (score > passScore)
        {


            // * Completion Panel = Win
            completionPanel.gameObject.SetActive(true);
            completionPanel.titleText.text = "You Win!";
            completionPanel.percentageText.text = (score).ToString("#");
            completionPanel.percentageText.color = Color.green;

            // Unlock next level
            Level nextLevel = level.NextLevel();
            switch (nextLevel.week)
            {
                case 1:
                    gameProgress.week1Levels[nextLevel.day - 1].unlocked = true;
                    break;
                case 2:
                    gameProgress.week2Levels[nextLevel.day - 1].unlocked = true;
                    break;
                case 3:
                    gameProgress.week3Levels[nextLevel.day - 1].unlocked = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
            // * Completion Panel = Lose
            completionPanel.gameObject.SetActive(true);
            completionPanel.titleText.text = "You Lose";
            if(score <= 0){
                completionPanel.percentageText.text = (0).ToString("#");
            }
            else{
                completionPanel.percentageText.text = (score).ToString("#");
            }
            completionPanel.percentageText.color = Color.red;
        }
    }
}
