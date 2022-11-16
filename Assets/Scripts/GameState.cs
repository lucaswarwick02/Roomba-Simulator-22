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
    private int passScore;

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

    public int dirtCollected;



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
        String titleString = level + " Results";
        completionPanel.resultsTitle.text = "Room " + titleString.Substring(6);
        completionPanel.level = level;
        Debug.Log(level);
        Debug.Log(level.NextLevel());
        // completionPanel.medal.gameObject.SetActive(false);
        completionPanel.tick1.gameObject.SetActive(false);
        completionPanel.tick2.gameObject.SetActive(false);
        completionPanel.gameObject.SetActive(false);
        dirtCollected = 0;
        float passScore1 = (float) maxDirt/2;
        passScore = (int) Math.Ceiling((decimal) passScore1);
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
        if (dirtCollected >= maxDirt) {
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

        float score = (dirtCollected - Rings);

        // Assign score to level save data

        completionPanel.dirtCollected.text = dirtCollected.ToString();
        completionPanel.ringsCollected.text = Rings.ToString();

        if(passScore == 1){
                completionPanel.passScore.text =  "get 1 point to unlock next level";
            }
            else{
                completionPanel.passScore.text =  "get " + passScore.ToString() + " points to unlock next level";
            }
            completionPanel.medalScore.text =  "get " + maxDirt.ToString() + " points to earn clean sweep medal";

            completionPanel.score.text = score.ToString();

        if (score >= (int) passScore)
        {
            completionPanel.tick1.gameObject.SetActive(true);
            if(score >= maxDirt){
                
                completionPanel.medal.gameObject.SetActive(true);
                completionPanel.tick2.gameObject.SetActive(true);
            }
            else{
                completionPanel.medal.gameObject.SetActive(false);
                completionPanel.tick2.gameObject.SetActive(false);
            }

            if(passScore == 1){
                completionPanel.passScore.text =  "get 1 point to unlock next level";
            }
            else{
                completionPanel.passScore.text =  "get " + passScore.ToString() + " points to unlock next level";
            }
            // * Completion Panel = Win
            completionPanel.gameObject.SetActive(true);
            // completionPanel.titleText.text = "You Win!";
            // completionPanel.percentageText.text = (score).ToString("#");
            // completionPanel.percentageText.color = Color.green;

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
            completionPanel.deactivateNextLevel();
            // CompletionPanel.nextLevelButton.enabled = false;
        // CompletionPanel.nextLevelText.ToggleEffects(false);
        // CompletionPanel.nextLevelText.ForceDisable();
    

            completionPanel.medal.gameObject.SetActive(false);
            completionPanel.tick1.gameObject.SetActive(false);
            completionPanel.tick2.gameObject.SetActive(false);
            
        completionPanel.medalScore.text =  "get " + maxDirt.ToString() + " points to earn clean sweep medal";
            // * Completion Panel = Lose
            completionPanel.gameObject.SetActive(true);
            // completionPanel.titleText.text = "You Lose";
            if(score <= 0){
                // completionPanel.score.text = (0).ToString("#");
            }
            else{
                // completionPanel.score.text = (score).ToString("#");
            }
            // completionPanel.percentageText.color = Color.red;
        }

        // if(maxDirt <= dirtCollected){
        //     score = 1f;
        // }

        switch (level.week) {
            case 1:
                if (gameProgress.week1Levels[level.day - 1].percentage < score) {
                    if(score >= maxDirt){
                    gameProgress.week1Levels[level.day - 1].percentage = score;
                    }
                
                }
                break;
            case 2:
                if (gameProgress.week2Levels[level.day - 1].percentage < score) {
                     gameProgress.week2Levels[level.day - 1].percentage = score;
                 if(score >= maxDirt){
                    gameProgress.week1Levels[level.day - 1].percentage = score;
                    }
                
                }
                break;
            case 3:
                if (gameProgress.week3Levels[level.day - 1].percentage < score) {
                     gameProgress.week3Levels[level.day - 1].percentage = score;
                 if(score >= maxDirt){
                    gameProgress.week1Levels[level.day - 1].percentage = score;
                    }
                }
                break;
            default:
                break;
        }
    }
}
