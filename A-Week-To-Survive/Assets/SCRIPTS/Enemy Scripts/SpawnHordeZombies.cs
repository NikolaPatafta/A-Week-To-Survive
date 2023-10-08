using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHordeZombies : MonoBehaviour
{
    [SerializeField] private GameObject hordeZombieprefab;
    [SerializeField] private int addedHealth;
    [SerializeField] private DayAndNightSystem dayNight;
    [SerializeField] private GameObject lootPrefab;
    [SerializeField] private Canvas lootCanvas;
    
    private GameObject findPlayer;
    private int maxSpawnRadius = 25;
    private int minSpawnRadius = 20;
    private int remainingZombies = 1;

    void Start()
    {
        findPlayer = FindObjectOfType<PlayerAttack>().gameObject;
    }

    public IEnumerator spawnHordeZombies()
    {
        int dayCount = dayNight.day;
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
        hordeZombieprefab.gameObject.GetComponent<HealthScript>().health += addedHealth;
        hordeZombieprefab.gameObject.GetComponent<EnemyController>().chase_Distance = 100f;
        Debug.Log("Spawned: " + hordeZombieprefab.gameObject.name + " at x: " + x + " z: " + z);
    }

    public void ZombieKilled(Transform location)
    {
        remainingZombies--;

        if(remainingZombies <= 0)
        {
            Vector3 offset = new Vector3(location.transform.position.x, location.transform.position.y + 1, location.transform.position.z);
            StartCoroutine(SpawnLootTimer(offset));
            Debug.Log("Spawned: " + lootPrefab.gameObject.name + " at: " + offset);
        }
    }

    private IEnumerator SpawnLootTimer(Vector3 location)
    {
        yield return new WaitForSeconds(5);
        lootCanvas.gameObject.SetActive(true);
        Instantiate(lootPrefab, location, Quaternion.identity);
        yield return new WaitForSeconds(5);
        lootCanvas.gameObject.SetActive(false);
    }
}
