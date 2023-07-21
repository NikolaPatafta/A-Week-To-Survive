using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    private PlayerMovement playerMovement; 

    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;

    private Transform look_Root;
    private float stand_Height = 1.8f;
    private float crouch_Height = 1.6f;

    private bool isCrouching;

    private PlayerFootSteps player_FootSteps;

    //Zvuk za korake
    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_min = 0.2f;
    private float walk_Volume_max = 0.6f;
    //***

    //Razmak izmedju koraka (u sekundama)
    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;
    private float crouch_Step_Distance = 0.5f;
    //*** 

    private PlayerStats player_Stats;

    private float sprint_Value = 100f;
    private float sprint_Treshold = 10f;

    void Awake()
    {
        playerMovement= GetComponent<PlayerMovement>(); 
        look_Root = transform.GetChild(0);        
        player_FootSteps= GetComponentInChildren<PlayerFootSteps>();
        player_Stats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        player_FootSteps.volume_Min = walk_Volume_min;
        player_FootSteps.volume_Max= walk_Volume_max;
        player_FootSteps.step_Distance= walk_Step_Distance;
    }

    void Update()
    {
        Sprint(); 
        Crouch();
    }

    void Sprint()
    {

        if (sprint_Value > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching && Input.GetKey(KeyCode.W))
            {
                playerMovement.speed = sprint_Speed;

                //footsteps audio settings 
                player_FootSteps.step_Distance = sprint_Step_Distance;
                player_FootSteps.volume_Min = sprint_Volume;
                player_FootSteps.volume_Max = sprint_Volume;
                //***
            }

        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = move_Speed;

            //footstep audio settings
            player_FootSteps.step_Distance = walk_Step_Distance;
            player_FootSteps.volume_Min = walk_Volume_min;
            player_FootSteps.volume_Max = walk_Volume_max;
            //*** 

        }

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && Input.GetKey(KeyCode.W))
        {
            sprint_Value -= sprint_Treshold * Time.deltaTime;

            if (sprint_Value <= 0f)
            {
                sprint_Value = 0f;

                playerMovement.speed = move_Speed;
                player_FootSteps.step_Distance = walk_Step_Distance;
                player_FootSteps.volume_Min = walk_Volume_min;
                player_FootSteps.volume_Max = walk_Volume_max;
                WaitBeforeSprint();
            }
        }
        else
        {
            if (sprint_Value != 100f)
            {
                if (isCrouching)
                {
                    sprint_Value += (sprint_Treshold / 2f) * Time.deltaTime * 1.5f;
                }
                else
                {
                    sprint_Value += (sprint_Treshold / 2f) * Time.deltaTime;
                }

                if (sprint_Value > 100f)
                {
                    sprint_Value = 100f;
                }
            }
        }   
        player_Stats.Display_StaminaStats(sprint_Value);

    }//sprint

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            //if crouching stand up
            if (isCrouching) 
            {
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                playerMovement.speed = move_Speed;

                isCrouching = false;
                characterController.height = stand_Height;
            }
            //if not crouching - crouch
            else 
            {
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.speed = crouch_Speed;

                //crouching footsteps audio
                player_FootSteps.step_Distance = crouch_Step_Distance;
                player_FootSteps.volume_Min = crouch_Volume;
                player_FootSteps.volume_Max = crouch_Volume;
                //***

                isCrouching = true;
                characterController.height = crouch_Height;  
            }

        }
    }

    IEnumerator WaitBeforeSprint()
    {
        yield return new WaitForSeconds(2f);
    }
}
