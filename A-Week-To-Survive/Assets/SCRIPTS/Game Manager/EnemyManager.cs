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

    private int spawnableEnemyCount = 5;
    private int currentEnemyCount;
    private int maxSpawnRadius = 50;
    private int minSpawnRadius = 40;
    
    void Awake()
    {
        MakeInstance();
        player = FindObjectOfType<PlayerMovement>().transform;  
    }

    void Start()
    {
        StartCoroutine("SpawnEnemy");
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
        StopCoroutine("CheckToSpawnEnemies");
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

            Debug.Log("Spawned " + agent.name + " at " + randomPosition);

            if (navAgent != null)
            {
                navAgent.Warp(randomPosition);
            }
            currentEnemyCount++;         
        }
        yield return null;
        StartCoroutine("SpawnEnemy");
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
}
