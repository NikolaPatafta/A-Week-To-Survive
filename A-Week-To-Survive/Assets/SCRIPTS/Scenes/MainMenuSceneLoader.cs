using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneLoader : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}
