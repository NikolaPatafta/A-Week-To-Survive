using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private GameObject boar_Prefab, zombie_Prefab;

    public Transform[] zombie_SpawnPoints, boar_SpawnPoints;

    [SerializeField]
    private int zombie_Enemy_Count, boar_Enemy_Count;

    private int initial_Cannibal_Count, initial_Boar_Count;

    public float wait_Before_Spawn_Enemies_Time = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        initial_Cannibal_Count = zombie_Enemy_Count;
        initial_Boar_Count= boar_Enemy_Count;

        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void SpawnEnemies()
    {
        SpawnZombies();
        SpawnBoars();
    }

    void SpawnZombies()
    {
        int index = 0;
        for(int i = 0; i < zombie_Enemy_Count; i++)
        {
            if(index >= zombie_SpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(zombie_Prefab, zombie_SpawnPoints[index].position, Quaternion.identity);
            index++;
        }
        zombie_Enemy_Count = 0;
    }
    void SpawnBoars()
    {
        int index = 0;
        for (int i = 0; i < boar_Enemy_Count; i++)
        {
            if (index >= boar_SpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(boar_Prefab, boar_SpawnPoints[index].position, Quaternion.identity);
            index++;
        }
        boar_Enemy_Count = 0;

    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);

        SpawnZombies();
        SpawnBoars();

        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDied(bool zombie)
    {
        if (zombie)
        {
            zombie_Enemy_Count++;

            if(zombie_Enemy_Count > initial_Cannibal_Count) 
            { 
                zombie_Enemy_Count = initial_Cannibal_Count;
            }
        }
        else
        {
            boar_Enemy_Count++;

            if(boar_Enemy_Count > initial_Boar_Count)
            {
                boar_Enemy_Count = initial_Boar_Count;
            }
        }
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }

   
}
