using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.AI.Navigation;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
   
    [SerializeField] private GameObject[] spawnableEnemy;
    [SerializeField] private GameObject spawnableBoar;
    [SerializeField] private Terrain terrain;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private UIManager uiManager;
    public Transform player;

    private int spawnableEnemyCount = 15;
    private int currentEnemyCount;
    private int spawnableBoarCount = 3;
    private int currentBoarCount;
    private int maxSpawnRadius = 60;
    private int minSpawnRadius = 45;
    public float maxSlopeAngle = 45;

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
        if (currentEnemyCount < spawnableEnemyCount)
        {
            yield return new WaitForSeconds(4);
            Vector3 randomPosition = GetRandomPosition();
            float terrainHeight = terrain.SampleHeight(randomPosition);
            Vector3 terrainNormal = terrain.terrainData.GetInterpolatedNormal(randomPosition.x / terrain.terrainData.size.x, randomPosition.z / terrain.terrainData.size.z);
            float slopeAngle = Vector3.Angle(Vector3.up, terrainNormal);
            if(slopeAngle <= 45)
            {
                randomPosition.y = terrainHeight;

                NavMeshHit hit;
                if(NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
                {
                    GameObject agent = Instantiate(spawnableEnemy[Random.Range(0, spawnableEnemy.Length)], randomPosition, Quaternion.identity);
                    NavMeshAgent navAgent = agent.GetComponent<NavMeshAgent>();
                    if (navAgent != null)
                    {
                        navAgent.Warp(randomPosition);
                    }
                    currentEnemyCount++;
                }
                else
                {
                    Debug.Log("Could not spawn enemy on NavMesh!");
                }
            }
            else
            {
                Debug.Log("Could not spawn enemy!");
            }
        }
        yield return null;
        
        if(!uiManager.isPaused)
        {
            StartCoroutine("SpawnEnemy");
        }
        
    }

    private IEnumerator SpawnBoar()
    {
        if (currentBoarCount < spawnableBoarCount)
        {
            yield return new WaitForSeconds(25);
            Vector3 randomPosition = GetRandomPosition();
            float terrainHeight = terrain.SampleHeight(randomPosition);
            Vector3 terrainNormal = terrain.terrainData.GetInterpolatedNormal(randomPosition.x / terrain.terrainData.size.x, randomPosition.z / terrain.terrainData.size.z);
            float slopeAngle = Vector3.Angle(Vector3.up, terrainNormal);
            if (slopeAngle <= 45)
            {
                randomPosition.y = terrainHeight;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
                {
                    GameObject agent = Instantiate(spawnableBoar, randomPosition, Quaternion.identity);
                    NavMeshAgent navAgent = agent.GetComponent<NavMeshAgent>();
                    if (navAgent != null)
                    {
                        navAgent.Warp(randomPosition);
                    }
                    currentBoarCount++;
                }
                else
                {
                    Debug.Log("Could not spawn enemy on NavMesh!");
                }
            }
            else
            {
                Debug.Log("Could not spawn enemy!");
            }
        }
        yield return null;

        if (!uiManager.isPaused)
        {
            StartCoroutine("SpawnBoar");
        }  
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
