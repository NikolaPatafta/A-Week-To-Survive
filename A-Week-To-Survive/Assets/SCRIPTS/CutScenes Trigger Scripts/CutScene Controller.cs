using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public GameObject video;
    public GameObject blackscreen;

    public void TurnOnVideo()
    {
        video.gameObject.SetActive(true);
    }
    public void TurnOffVideo()
    {
        video.gameObject.SetActive(false);
    }
    public void TurnOnBlackscreen()
    {
        blackscreen.SetActive(true);
    }
    public void TurnOffBlackScreen()
    {
        blackscreen.SetActive(false);
    }
}
