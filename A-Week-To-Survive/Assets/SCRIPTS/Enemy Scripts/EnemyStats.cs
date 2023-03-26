using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]private Image healthbar;

    private HealthScript healthamount;

    void Awake()
    {
        healthamount = GetComponent<HealthScript>();
    }

    
    public void Display_EnemyHealth(float healthValue)
    {
        healthValue /= 100f;

        healthbar.fillAmount = healthValue;

    }


}
