using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodScreenEffect : MonoBehaviour
{
    [SerializeField] private Image bloodEffectImage;
    [SerializeField] private HealthScript healthScript;
    private float currentHealth;
    private float temphealth;

    private void Start()
    {
        temphealth = healthScript.health;
    }

    public void ChangeAlpha()
    {
        currentHealth = healthScript.health;
        currentHealth /= 1000;

        if (currentHealth < temphealth)
        {    
            bloodEffectImage.color += new Color(0, 0, 0, currentHealth);
            temphealth = currentHealth; 
        }
        else
        {
            bloodEffectImage.color -= new Color(0, 0, 0, currentHealth);
            temphealth = currentHealth;
        }

        
    }

    


}
