using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SpecialItemsManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private int collectableCounter;

    public void SpecialItemCollected()
    {
        //play sound when special item is collected
        collectableCounter++;
        AllSpecialItemsCollected();
    }

    public void AllSpecialItemsCollected()
    {
        if (collectableCounter == 5)
        {
            //unlocked 3rd cutscene when collected all items

            audioSource.Play();
        }
    }

}
