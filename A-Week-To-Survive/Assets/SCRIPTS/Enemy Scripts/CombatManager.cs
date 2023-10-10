using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject doorBlocker;  
    
    private DoorRayCast player;
    private EnemyController[] enemy;
    

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<DoorRayCast>();
        StartCoroutine(CombatCheck());   
    }

    // Update is called once per frame
    private IEnumerator CombatCheck()
    {
        EnemyController[] enemy = FindObjectsOfType<EnemyController>();
        bool anyEnemyInCombat = false;
        
        foreach (EnemyController enemyController in enemy)
        {
            if (enemyController.inCombat)
            {
                Debug.Log("Found " + enemyController.name + " in combat.");
                anyEnemyInCombat = true;
                break;
            }
        }
        doorBlocker.gameObject.SetActive(anyEnemyInCombat);
        
        yield return new WaitForSeconds(2);
        StartCoroutine(CombatCheck());
    }
}
