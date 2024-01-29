using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHordeZombies : MonoBehaviour
{
    [SerializeField] private GameObject hordeZombieprefab;
    [SerializeField] private int addedHealth;
    [SerializeField] private DayAndNightSystem dayNight;
    public GameObject[] lootPrefab;
    [SerializeField] private Canvas lootCanvas;
    
    private GameObject findPlayer;
    private int maxSpawnRadius = 25;
    private int minSpawnRadius = 20;
    private int remainingZombies = 1;
    
    public int counter = -1;

    void Start()
    {
        findPlayer = FindObjectOfType<PlayerAttack>().gameObject;
    }

    public IEnumerator spawnHordeZombies()
    {
        int dayCount = dayNight.day;
        if(dayCount == 7)
        {
            dayCount = 4;
        }
        if(dayCount == 14)
        {
            dayCount = 10;
        }
        if(dayCount == 21)
        {
            dayCount = 12;
        }
        if(dayCount == 28)
        {
            dayCount = 14;
        }
        if(dayCount == 35)
        {
            dayCount = 14;
        }
        remainingZombies = dayCount;
        yield return new WaitForSeconds(5);
        for(int i = 0; i < dayCount; i++) 
        {
            yield return new WaitForSeconds(1);
            CheckForLocationToSpawn(true);
        }
    } 

    void CheckForLocationToSpawn(bool status)
    {
        float x = Random.Range(findPlayer.transform.position.x + minSpawnRadius, findPlayer.transform.position.x + maxSpawnRadius);
        float z = Random.Range(findPlayer.transform.position.z + minSpawnRadius, findPlayer.transform.position.z + maxSpawnRadius);

        Instantiate(hordeZombieprefab, new Vector3(x, 100, z), Quaternion.identity);
        hordeZombieprefab.gameObject.GetComponent<EnemyController>().chase_Distance = 100f;
    }

    public void ZombieKilled(Transform location)
    {
        remainingZombies--;

        if(remainingZombies <= 0)
        {
            Vector3 offset = new Vector3(location.transform.position.x, location.transform.position.y + 1, location.transform.position.z);
            StartCoroutine(SpawnLootTimer(offset));
        }
    }

    private IEnumerator SpawnLootTimer(Vector3 location)
    {
        yield return new WaitForSeconds(5);
        lootCanvas.gameObject.SetActive(true);
        Instantiate(lootPrefab[counter], location, Quaternion.identity);
        yield return new WaitForSeconds(5);
        lootCanvas.gameObject.SetActive(false);
    }
}
