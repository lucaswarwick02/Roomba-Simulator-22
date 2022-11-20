using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameProgress gameProgress;
    public Settings settings;

    public GameObject mainMenuPanel;

    public GameObject week1Panel;
    public GameObject week2Panel;
    public GameObject week3Panel;

    [SerializeField] public AudioSource click;

    public GameObject week1Option;
    public GameObject week2Option;
    public GameObject week3Option;

    public GameObject[] week1Levels;
    public GameObject[] week2Levels;
    public GameObject[] week3Levels;

    public TextMeshProUGUI needtext1;
    public TextMeshProUGUI needtext2;
    public Image needMedal1;
    public Image needMedal2;
    public TextMeshProUGUI medalAmountT;

    private void Start() {
        UpdateUI();
        medalAmountT.text = "x " + gameProgress.NumberOfMedals();
    }


    public void SelectWeek1 () {
        click.Play();
        DeactivateAllPanels();
        week1Panel.SetActive(true);
    }
    public void SelectWeek2 () {
        click.Play();
        DeactivateAllPanels();
        week2Panel.SetActive(true);
    }
    public void SelectWeek3 () {
        click.Play();
        DeactivateAllPanels();
        week3Panel.SetActive(true);
    }

    /// <summary>
    /// Go back to the MainMenu panel.
    /// </summary>
    public void Back () {
        click.Play();
        DeactivateAllPanels();
        mainMenuPanel.SetActive(true);
    }

    /// <summary>
    /// Toggle UI components based on the current GameSave data.
    /// </summary>
    public void UpdateUI () {
        AudioListener.volume = settings.mute ? 0 : 1;

        needMedal1.gameObject.SetActive(gameProgress.NumberOfMedals() < 5);
        needtext1.gameObject.SetActive(gameProgress.NumberOfMedals() < 5);

        needMedal2.gameObject.SetActive(gameProgress.NumberOfMedals() < 10);
        needtext2.gameObject.SetActive(gameProgress.NumberOfMedals() < 10);

        for (int house = 1; house <= 3; house++) {
            getHouseOption(house).GetComponent<Button>().enabled = gameProgress.IsHouseUnlocked(house);
            getHouseOption(house).transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsHouseUnlocked(house));

            LevelSaveData[] levelSaveDatas = gameProgress.GetHouseSaveData(house);
            
            for (int i = 0; i < levelSaveDatas.Length; i++) {
                getHouseLevels(house)[i].GetComponent<Button>().enabled = gameProgress.IsRoomUnlocked(house, i + 1);
                getHouseLevels(house)[i].transform.GetChild(0).GetComponent<TextButton>().ToggleEffects(gameProgress.IsRoomUnlocked(house, i + 1));
                getHouseLevels(house)[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ("high score " + levelSaveDatas[i].highScore);
                getHouseLevels(house)[i].transform.GetChild(1).GetComponent<TextButton>().ToggleEffects(gameProgress.IsRoomUnlocked(house, i + 1));
                getHouseLevels(house)[i].transform.GetChild(1).GetComponent<TextButton>().ForceDisable();
                getHouseLevels(house)[i].transform.GetChild(3).gameObject.SetActive(gameProgress.RoomHasMedal(house, i + 1));
            }
        }
    }

    /// <summary>
    /// Get all of the UI objects for the levels.
    /// </summary>
    /// <param name="house">House number</param>
    /// <returns>List of GameObjects</returns>
    public GameObject[] getHouseLevels (int house) {
        switch (house) {
            case 1:
                return week1Levels;
            case 2:
                return week2Levels;
            case 3:
                return week3Levels;
            default:
                return week1Levels;
        }
    }

    /// <summary>
    /// Get the GameObject for the house option.
    /// </summary>
    /// <param name="house">House number</param>
    /// <returns>GameObject for the option</returns>
    public GameObject getHouseOption (int house) {
        switch (house) {
            case 1:
                return week1Option;
            case 2:
                return week2Option;
            case 3:
                return week3Option;
            default:
                return week1Option;
        }
    }

    /// <summary>
    /// Change the current scene to a given level.
    /// </summary>
    /// <param name="levelID">Name of the scene being loaded</param>
    public void StartLevel (string levelID) {
        click.Play();
        // Load level scene
        SceneManager.LoadScene("Level_" + levelID);
    }

    /// <summary>
    /// Decactivate all of the UI panels.
    /// </summary>
    private void DeactivateAllPanels () {
        week1Panel.SetActive(false);
        week2Panel.SetActive(false);
        week3Panel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }
}
