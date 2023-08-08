using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenuManager mainMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;

    private SaveSlot[] saveSlots;
    private bool isLoadingGame = false;

    private void Awake()
    {
        saveSlots = GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();

        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileID());
        
        if(!isLoadingGame)
        {
            DataPersistenceManager.instance.NewGame();
        }   
        SceneManager.LoadSceneAsync(2);

    }

    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        DeactivateMenu();
    }


    public void ActivateMenu(bool isLoadingGame)
    {
        this.gameObject.SetActive(true);   
        
        this.isLoadingGame = isLoadingGame;

        Dictionary<string, GameData> profileGamedata = DataPersistenceManager.instance.GetallProfilesGameData();    

        foreach (SaveSlot saveslot in saveSlots)
        {
            GameData profileData = null;
            profileGamedata.TryGetValue(saveslot.GetProfileID(), out profileData);
            saveslot.SetData(profileData);
            if(profileData == null && isLoadingGame)
            {
                saveslot.SetIneractable(false);
            }
            else
            {
                saveslot.SetIneractable(true);
            }
        }
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetIneractable(false);
        }
        backButton.interactable = false;
    }
}
