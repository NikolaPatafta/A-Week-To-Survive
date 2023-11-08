using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour, IDataPersistence
{
    public bool isPaused = false;
    public bool isCutScenePlaying = false;
    public int scoreCounter;
    public int dayCounter;

    [SerializeField] private GameObject playerCanvas = null;
    [SerializeField] private GameObject pauseCanvas = null;
    [SerializeField] private GameObject endCanvas = null;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject pauseScreenButtons;
    [SerializeField] private HealthScript healthScript;
    [SerializeField] private InventoryManager invManager;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private TextMeshProUGUI scoreText; 

    private void Start()
    {
        SetActiveHud(true);
        scoreText.text = "Score: " + scoreCounter;
    }

    private void Update()
    {
        if (!healthScript.IsDead())
        {
            if (!isCutScenePlaying)
            {
                if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
                {
                    SetActivePause(true);
                }
                else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
                {
                    SetActivePause(false);
                    OptionsBackButton();
                }
            }
            else
            {
                playerCanvas.SetActive(false);
            }
        }
        else if (healthScript.IsDead())
        {
            UnlockCursor();
        }


    }

    public void SetActiveHud(bool state)
    {

        playerCanvas.SetActive(state);
        endCanvas.SetActive(!state);
        if (!healthScript.IsDead())
        {
            pauseCanvas.SetActive(!state);
        }

    }

    public void CutSceneIsPlaying(bool state)
    {
        Debug.Log("UiManager CutSceneIsPlaying status: " + state);
        playerCanvas.SetActive(!state);
        isCutScenePlaying = state;
    }

    public void HardPause(bool state)
    {
        Time.timeScale = state ? 0 : 1; 
    }

    public void SetActivePause(bool state)
    {
        pauseCanvas.SetActive(state);
        playerCanvas.SetActive(!state);

        Time.timeScale = state ? 0 : 1;
        isPaused = state;

        if (!isPaused && !invManager.isInventoryOn)
        {
            LockCursor();
        }
        else if (!isPaused && invManager.isInventoryOn)
        {
            invManager.TurnInventoryOfforOn(false);
            LockCursor();
        }
        else if (isPaused)
        {
            UnlockCursor();
        }


    }

    public void SetActiveOptions()
    {
        if (!pauseCanvas.activeInHierarchy)
        {
            pauseCanvas.SetActive(true);
        }
        optionsScreen.SetActive(true);
        pauseScreenButtons.SetActive(false);
    }

    public void OptionsBackButton()
    {
        optionsScreen.SetActive(false);
        pauseScreenButtons.SetActive(true);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }


    public void Restart()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void IncreaseScore()
    {
        scoreCounter++;
        scoreText.text = "Score: " + scoreCounter;
    }

    public void LoadData(GameData data)
    {
        this.scoreCounter = data.scoreCounter;
        this.dayCounter = data.dayCounter;
    }

    public void SaveData(ref GameData data)
    {
        data.scoreCounter = this.scoreCounter;
        data.dayCounter = this.dayCounter;
    }


  
}
