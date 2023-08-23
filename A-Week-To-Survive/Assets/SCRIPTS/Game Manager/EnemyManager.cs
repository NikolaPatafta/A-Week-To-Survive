using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
   
    [SerializeField] private GameObject[] spawnableEnemy;
    [SerializeField] private Terrain terrain;
    [SerializeField] private NavMeshSurface navMeshSurface;
    public Transform player;

    private int spawnableEnemyCount = 10;
    private int currentEnemyCount;
    private int spawnableBoarCount = 3;
    private int currentBoarCount;
    private int maxSpawnRadius = 60;
    private int minSpawnRadius = 45;
    
    void Awake()
    {
        MakeInstance();
        player = FindObjectOfType<PlayerMovement>().transform;  
    }

    void Start()
    {
        StartCoroutine("SpawnEnemy");
        StartCoroutine("SpawnBoar");
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StopSpawning()
    {
        StopCoroutine("SpawnEnemy");
        StopCoroutine("SpawnBoar");
    }

    private IEnumerator SpawnEnemy()
    {
        if(currentEnemyCount < spawnableEnemyCount)
        {
            yield return new WaitForSeconds(3);
            Vector3 randomPosition = GetRandomPosition();
            randomPosition.y = terrain.SampleHeight(randomPosition);
            GameObject agent = Instantiate(spawnableEnemy[Random.Range(0, spawnableEnemy.Length)], randomPosition, Quaternion.identity);
            NavMeshAgent navAgent = agent.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.Warp(randomPosition);
            }
            currentEnemyCount++;         
        }
        yield return null;
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnBoar()
    {
        if (currentBoarCount < spawnableBoarCount)
        {
            yield return new WaitForSeconds(20);
            Vector3 randomPosition = GetRandomPosition();
            randomPosition.y = terrain.SampleHeight(randomPosition);
            GameObject agent = Instantiate(spawnableEnemy[Random.Range(0, spawnableEnemy.Length)], randomPosition, Quaternion.identity);
            NavMeshAgent navAgent = agent.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.Warp(randomPosition);
            }
            currentBoarCount++;
        }
        yield return null;
        StartCoroutine("SpawnBoar");
    }


    private Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0f;
        randomDirection.Normalize();

        float randomDistance = Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector3 randomPosition = player.transform.position + randomDirection * randomDistance;

        return randomPosition;
    }
    

    public void LowerEnemyCounter()
    {
        currentEnemyCount--;
    }

    public void LowerBoarCounter()
    {
        currentBoarCount--;
    }
}
