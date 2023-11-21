using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenesManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerSprintAndCrouch playerSprint;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private PlayerFootSteps playerSound;
    [SerializeField] private DoorRayCast doorRayCast;
    [SerializeField] private EnemyManager enemyManager;

    public void PlayingCutScene(bool playing)
    {
        equipmentManager.UnequipWeapon();
        gameObject.SetActive(!playing);
        uiManager.isPaused = playing;
        playerMovement.enabled = !playing;
        playerSprint.enabled = !playing;
        mouseLook.enabled = !playing;
        playerSound.enabled = !playing;
        doorRayCast.enabled = !playing;
        enemyManager.enabled = !playing;
        Debug.Log("EnemyManager: " + enemyManager.enabled);
    }


}
