using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodScreenEffect : MonoBehaviour
{
    [SerializeField]
    private Image bloodEffectImage;

    [SerializeField]
    private HealthScript healthScript;

    //samo za pregled u inspektoru
    private float currentHealth;


    public void ChangeAlpha()
    {
        currentHealth = healthScript.health;

        currentHealth /= 1000;

        bloodEffectImage.color += new Color (0, 0, 0, currentHealth);
    }

    


}
