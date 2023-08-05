using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        ActivateMainMenu(true);
        Cursor.visible = true;

        if(!DataPersistenceManager.instance.hasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void ActivateMainMenu(bool state)
    {
        mainMenu.SetActive(state);
        optionsMenu.SetActive(!state);
    }

    public void Play()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync(2);
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync(2);
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
    


}
