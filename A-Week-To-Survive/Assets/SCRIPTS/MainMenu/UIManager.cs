using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool isPaused = false;

    [SerializeField]
    private GameObject hudCanvas = null;

    [SerializeField]
    private GameObject pauseCanvas = null;

    [SerializeField]
    private GameObject endCanvas = null;

    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private GameObject optionsScreen;

    [SerializeField]
    private GameObject pauseScreenButtons;

    [SerializeField]
    private HealthScript checkifDead;

    [SerializeField]
    private InventoryManager invManager;


    private void Start()
    {
        SetActiveHud(true);
    }

    private void Update()
    {
        if (!checkifDead.IsDead())
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
        else if (checkifDead.IsDead())
        {
            UnlockCursor();
        }
       

    }

    public void SetActiveHud(bool state)
    {

        hudCanvas.SetActive(state);
        endCanvas.SetActive(!state);
        if (!checkifDead.IsDead())
        {
            pauseCanvas.SetActive(!state);
        }

    }

    public void SetActivePause(bool state)
    {
        pauseCanvas.SetActive(state);
        hudCanvas.SetActive(!state);

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
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

  
}
