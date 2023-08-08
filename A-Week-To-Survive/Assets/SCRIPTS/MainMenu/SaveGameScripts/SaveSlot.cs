using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private Text scoreBoardText;
    [SerializeField] private Text dayCountText;

    private Button saveSlotButton;

    private void Awake()
    {
        saveSlotButton = GetComponent<Button>();
    }

    public void SetData(GameData data)
    {
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            scoreBoardText.text = "SCORE: " + data.scoreCounter;
            dayCountText.text = "DAY: " + data.dayCounter;
        }
    }

    public string GetProfileID()
    {
        return profileId;
    }

    public void SetIneractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
    }

}
