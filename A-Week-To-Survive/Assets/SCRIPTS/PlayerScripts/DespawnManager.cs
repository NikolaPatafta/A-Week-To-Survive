using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnManager : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    private HealthScript healthScript;

    private void Start()
    {
        Debug.Log("Hello");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            healthScript = other.transform.GetComponent<HealthScript>();
            healthScript.TurnOffGameObject();
            enemyManager.LowerEnemyCounter();
            Debug.Log("Disabled : " + other.name);
        }
        
    }
}
