using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_Clip;

    private CharacterController character_Controller;

    [HideInInspector]
    public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance;
    // Start is called before the first frame update
    void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        CheckToPlayFootStepSound();
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
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
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
