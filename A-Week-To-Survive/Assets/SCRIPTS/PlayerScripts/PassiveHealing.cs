using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveHealing : MonoBehaviour
{
    private HealthScript playerhealth;
    private PlayerStats stats;
    

    private void Start()
    {
        playerhealth = GetComponent<HealthScript>();
        stats = GetComponent<PlayerStats>();
        stats.Display_HealthStats(playerhealth.health);
        StartCoroutine("HealthCheckCoroutine");
    }

    private IEnumerator HealthCheckCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (playerhealth.health < 100)
            {
                StartCoroutine(Healing());
            }
        }
    }

    private IEnumerator Healing()
    {
        while (playerhealth.health < 100)
        {
            playerhealth.health += 0.01f; 
            playerhealth.health = Mathf.Min(playerhealth.health, 100f); 
            stats.Display_HealthStats(playerhealth.health);
            yield return new WaitForSeconds(2f);
        }
    }
}
