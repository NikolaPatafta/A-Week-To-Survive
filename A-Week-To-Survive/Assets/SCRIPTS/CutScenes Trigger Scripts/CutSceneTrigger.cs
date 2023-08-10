using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject cutScene;
    [SerializeField] private CutScenesManager cutSceneManager;

    public bool cutSceneWasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!cutSceneWasPlayed && other.tag.Equals("Player"))
        {
            cutScene.SetActive(true);
            cutSceneManager.PlayingCutScene(true);
            gameObject.SetActive(false);
            Debug.Log("Triggered CutScene!");
        } 
    }

    public void TurnOffCutScene()
    {
        cutSceneManager.PlayingCutScene(false);
        cutScene.SetActive(false);
        cutSceneWasPlayed = true;

    }
}
