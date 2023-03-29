using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsScreen : MonoBehaviour
{
    public Toggle fullScreenTog, vSyncTog;

    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;

    public Text resolutionLabel;

    //Audio settings https://www.youtube.com/watch?v=tepGwzd-zRg
    public AudioMixer theMixer;
    public Text masterLabel, musicLabel, SFXLabel;
    public Slider masterSlider, musicSlider, SFXslider;

    // Start is called before the first frame update
    void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vSyncTog.isOn = false;
        }
        else
        {
            vSyncTog.isOn = true;   
        }

        bool foundRes = false;
        for(int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;

                UpdateResolutionLabel();
            }
        }
        if(!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedResolution = resolutions.Count - 1;

            UpdateResolutionLabel();
        }

        float vol = 0f;
        theMixer.GetFloat("MasterVol", out vol);
        masterSlider.value = vol;
        theMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        theMixer.GetFloat("SFXVol", out vol);
        SFXslider.value = vol;

        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        SFXLabel.text = Mathf.RoundToInt(SFXslider.value + 80).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ResLeft()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        UpdateResolutionLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count -1)
        {
            selectedResolution = resolutions.Count -1;
        }
        UpdateResolutionLabel();
    }

    public void UpdateResolutionLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " +
        resolutions[selectedResolution].vertical.ToString();
    }

    public void ApplyGraphics()
    {
        Screen.fullScreen= fullScreenTog.isOn;

        if(vSyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullScreenTog.isOn);
    }

    public void SetMasterVolume()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString(); 
        
        theMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }
    public void SetMusicVolume()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }
    public void SetSFXVolume()
    {
        SFXLabel.text = Mathf.RoundToInt(SFXslider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", SFXslider.value);

        PlayerPrefs.SetFloat("SFXVol", SFXslider.value);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;    
}
