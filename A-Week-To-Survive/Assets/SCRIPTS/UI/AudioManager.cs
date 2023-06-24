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
        FirstStopDayAudio();
        FirstStopNightAudio();
    }

    void Update()
    {

        if (!uiManager.isPaused)
        {
            if (dayTime.isDay)
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
        dayAudioSource.Pause();
    }
    public void PlayNightAudio()
    {
        nightAudioSource.Play();    
    }

    public void StopNightAudio()
    {
        nightAudioSource.Pause();
    }

    public void FirstStopDayAudio()
    {
        dayAudioSource.Stop();
    }

    public void FirstStopNightAudio()
    {
        nightAudioSource.Stop();
    }

}
