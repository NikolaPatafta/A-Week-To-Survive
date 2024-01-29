using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject doorBlocker;
    [SerializeField] private GameObject[] cutSceneTriggers;
    [SerializeField] private SneakingScript sneakingScript;
    
    private EnemyController[] enemy;
    

    // Start is called before the first frame update
    void Start()
    {
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
                sneakingScript.ClosePicture();
                anyEnemyInCombat = true;
                break;
            }
            else
            {
                sneakingScript.OpenPicture();
            }
        }
        doorBlocker.gameObject.SetActive(anyEnemyInCombat);
        
        for (int i = 0; i < cutSceneTriggers.Length; i++)
        {
            cutSceneTriggers[i].gameObject.SetActive(!anyEnemyInCombat);
        }
        
        yield return new WaitForSeconds(1);
        StartCoroutine(CombatCheck());
    }
}
