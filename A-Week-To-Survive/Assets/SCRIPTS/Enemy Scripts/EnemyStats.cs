using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private Image healthbar;
    private HealthScript healthamount;
    private float thishealth;

    void Awake()
    {
        healthamount = GetComponent<HealthScript>();
    }
    private void Update()
    {
        thishealth = healthamount.health;
    }
    public void Display_EnemyHealth(float healthValue)
    {
        healthValue /= 100;
        healthbar.fillAmount = healthValue;
    }
}
