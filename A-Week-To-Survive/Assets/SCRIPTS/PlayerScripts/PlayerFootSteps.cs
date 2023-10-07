using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource footstep_Sound;

    [SerializeField] private AudioClip[] footstep_Clip;
    [HideInInspector] public float step_Distance;
    [HideInInspector] public float volume_Min, volume_Max;

    public float rayCastDistance = 1.87f;
    private float accumulated_Distance;
    private bool hitTerrain;
    private CharacterController character_Controller;
    
    void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();
        character_Controller = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        CheckToPlayFootStepSound();
        RayCastOnGround();
    }

    private void RayCastOnGround()
    {
        Vector3 offset = transform.position + new Vector3(0f, 1f, 0f);
        RaycastHit hit;

        if(Physics.Raycast(offset, Vector3.down ,out hit, rayCastDistance))   
        {
            if (hit.collider.CompareTag("Terrain"))
            {
                hitTerrain = true;
            }
            else
            {
                hitTerrain = false;
            }
        }
        else
        {
            hitTerrain = true;
        }
    }

    void CheckToPlayFootStepSound()
    {
        if(!character_Controller.isGrounded)
        {
            return;
        }
        //provjerava velocity ako se krecemo (velocity je Vector3) 
        if(character_Controller.velocity.sqrMagnitude> 0)
        {
            //accumulated distance je value kolko daleko mozemo ici dok ne pustimo zvuk (footstep sound) npr. hodamo ili trcimo
            accumulated_Distance += Time.deltaTime;

            if(accumulated_Distance > step_Distance) 
            {
                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                if(hitTerrain)
                {
                    footstep_Sound.clip = footstep_Clip[Random.Range(0, 3)];
                }
                else
                {
                    footstep_Sound.clip = footstep_Clip[Random.Range(4, footstep_Clip.Length)];
                } 
                footstep_Sound.Play();
                accumulated_Distance = 0f;

            }       
        }
        else
        {
            accumulated_Distance = 0f;
        }
    }

}
