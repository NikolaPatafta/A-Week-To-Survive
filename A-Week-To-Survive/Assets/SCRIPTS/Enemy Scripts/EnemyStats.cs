using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private Image healthbar;
    private HealthScript healthScript;
    private float thishealth;

    void Awake()
    {
        healthScript = GetComponent<HealthScript>();
    }
    private void Update()
    {
        float currentHealth = healthScript.health;
        float maxHealth = healthScript.maxHealth;

        float healthValue = currentHealth / maxHealth;

        Display_EnemyHealth(healthValue);
    }
    public void Display_EnemyHealth(float healthValue)
    {
        healthValue= Mathf.Clamp01(healthValue);
        healthbar.fillAmount = healthValue;
    }
}
