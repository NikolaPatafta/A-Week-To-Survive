using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource dayAudioSource;
    public AudioSource nightAudioSource;

    [SerializeField]
    private DayAndNightSystem dayTime;
    [SerializeField]
    private UIManager uiManager;

    void Awake()
    {
        StopDayAudio();
        StopNightAudio();
    }

    void Update()
    {

        if (Time.timeScale != 0)
        {
            if (dayTime.currentHours >= 6 && dayTime.currentHours <= 19)
            {
                if (!dayAudioSource.isPlaying)
                {
                    StopNightAudio();
                    PlayDayAudio();
                }
            }
            else
            {
                if (!nightAudioSource.isPlaying)
                {
                    StopDayAudio();
                    PlayNightAudio();
                }
            }
        }
        else
        {
            StopDayAudio();
            StopNightAudio();
        } 
    }

    public void PlayDayAudio()
    {
        dayAudioSource.Play();
    }

    public void StopDayAudio()
    {
        dayAudioSource.Stop();
    }
    public void PlayNightAudio()
    {
        nightAudioSource.Play();    
    }

    public void StopNightAudio()
    {
        nightAudioSource.Stop();
    }

}
