using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField]
    private GameObject hudCanvas = null;

    [SerializeField]
    private GameObject pauseCanvas = null;

    [SerializeField]
    private GameObject endCanvas = null;

    [SerializeField]
    private AudioSource audioPause;

    private HealthScript checkifDead;

    private CameraController camController = null;

    private void Awake()
    {
        checkifDead= GetComponent<HealthScript>();
        audioPause.GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetActiveHud(true);
        camController= GetComponentInChildren<CameraController>();
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
            }
        }
        else if (checkifDead.IsDead())
        {
            MuteOrPlayAudio(true);
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
        MuteOrPlayAudio(state);

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
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void MuteOrPlayAudio(bool audiostatus)
    {

        if(audiostatus)
        {
            audioPause.Pause();
        }
        else
        {
            audioPause.Play();
        }
    }

  
}
