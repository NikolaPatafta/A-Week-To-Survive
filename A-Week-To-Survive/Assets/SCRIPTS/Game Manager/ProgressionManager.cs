using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public GameObject[] Borders;
    public Canvas NotificationText;
    public AudioSource audioSource;

    public void RemoveLevelBorder(int bordernumber)
    {
        Borders[bordernumber].gameObject.SetActive(false);
        StartCoroutine("NotificationTextTimer");
        audioSource.Play();
    }

    private IEnumerator NotificationTextTimer()
    {
        NotificationText.gameObject.SetActive(true);
        yield return new WaitForSeconds(7f);
        NotificationText.gameObject.SetActive(false);
    }
    

}
