using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    public GameObject cutScene;
    public UIManager uiManager;
    public EquipmentManager equipmentManager;
    public PlayerMovement playerMovement;
    public PlayerSprintAndCrouch playerSprint;
    public MouseLook mouseLook;
    public PlayerFootSteps playerSound;
    public DoorRayCast doorRayCast;

    public bool cutSceneWasPlayed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!cutSceneWasPlayed && other.tag.Equals("Player"))
        {
            equipmentManager.UnequipWeapon();
            cutScene.SetActive(true);
            gameObject.SetActive(false);
            Debug.Log("Triggered CutScene!");
            uiManager.isPaused = true;
            playerMovement.enabled = false;
            playerSprint.enabled = false;
            mouseLook.enabled = false;
            playerSound.enabled = false;
            doorRayCast.enabled = false;
        }
        
    }

    public void TurnOffCutScene()
    {
        cutScene.SetActive(false);
        cutSceneWasPlayed = true;
        uiManager.isPaused = false;
        playerMovement.enabled = true;
        playerSprint.enabled = true;
        mouseLook.enabled = true;
        playerSound.enabled = true;
        doorRayCast.enabled = true;

    }
}
