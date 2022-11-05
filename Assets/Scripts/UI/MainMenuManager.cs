using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;

    public GameObject week1Panel;
    public GameObject week2Panel;
    public GameObject week3Panel;

    public GameObject week1Option;
    public GameObject week2Option;
    public GameObject week3Option;

    public GameObject[] week1Levels;
    public GameObject[] week2Levels;
    public GameObject[] week3Levels;

    public void SelectWeek1 () {
        DeactivateAllPanels();
        week1Panel.SetActive(true);
    }
    public void SelectWeek2 () {
        DeactivateAllPanels();
        week2Panel.SetActive(true);
    }
    public void SelectWeek3 () {
        DeactivateAllPanels();
        week3Panel.SetActive(true);
    }

    /// <summary>
    /// Go back to the MainMenu panel.
    /// </summary>
    public void Back () {
        DeactivateAllPanels();
        mainMenuPanel.SetActive(true);
    }

    /// <summary>
    /// Create a new save on persistent storage.
    /// </summary>
    public void NewSave () {
        GameSave.NewSave();
        UpdateUI();
    }
    /// <summary>
    /// Saves data to persistent storage.
    /// </summary>
    public void Save () {
        GameSave.Save();
    }
    /// <summary>
    /// Loads data from persistent storage.
    /// </summary>
    public void Load() {
        GameSave.Load();
        UpdateUI();
    }

    /// <summary>
    /// (De)activates the UI based on the current GameSave data.
    /// </summary>
    public void UpdateUI () {
        week1Option.GetComponent<Button>().enabled = GameSave.INSTANCE.week1Unlocked;
        week1Option.transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(GameSave.INSTANCE.week1Unlocked);

        week2Option.GetComponent<Button>().enabled = GameSave.INSTANCE.week2Unlocked;
        week2Option.transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(GameSave.INSTANCE.week2Unlocked);

        week3Option.GetComponent<Button>().enabled = GameSave.INSTANCE.week3Unlocked;
        week3Option.transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(GameSave.INSTANCE.week3Unlocked);

        for (int i = 0; i < GameSave.INSTANCE.week1LevelsUnlocked.Length; i++) {
            week1Levels[i].GetComponent<Button>().enabled = GameSave.INSTANCE.week1LevelsUnlocked[i];
            week1Levels[i].transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(GameSave.INSTANCE.week1LevelsUnlocked[i]);
        }

        for (int i = 0; i < GameSave.INSTANCE.week2LevelsUnlocked.Length; i++) {
            week2Levels[i].GetComponent<Button>().enabled = GameSave.INSTANCE.week2LevelsUnlocked[i];
            week2Levels[i].transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(GameSave.INSTANCE.week2LevelsUnlocked[i]);
        }

        for (int i = 0; i < GameSave.INSTANCE.week3LevelsUnlocked.Length; i++) {
            week3Levels[i].GetComponent<Button>().enabled = GameSave.INSTANCE.week3LevelsUnlocked[i];
            week3Levels[i].transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(GameSave.INSTANCE.week3LevelsUnlocked[i]);
        }
    }

    public void StartLevel (string levelID) {
        // Load level scene
        SceneManager.LoadScene("Level_" + levelID);
    }

    private void DeactivateAllPanels () {
        week1Panel.SetActive(false);
        week2Panel.SetActive(false);
        week3Panel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }
}
