using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SpecialItemsManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject _3rdCutScene;

    private int collectableCounter = 0;

    public void SpecialItemCollected()
    {
        //play sound when special item is collected
        collectableCounter++;
        AllSpecialItemsCollected();
    }

    public void AllSpecialItemsCollected()
    {
        if (collectableCounter == 4)
        {
            //unlocked 3rd cutscene when collected all items
            audioSource.Play();
            _3rdCutScene.gameObject.SetActive(true);
        }
    }

}
