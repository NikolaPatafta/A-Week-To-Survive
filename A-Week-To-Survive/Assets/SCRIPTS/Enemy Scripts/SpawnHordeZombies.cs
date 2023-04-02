using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnHordeZombies : MonoBehaviour
{
    [SerializeField]
    private GameObject findPlayer;
    [SerializeField]
    private GameObject hordeZombieprefab;
    [SerializeField]
    private int addedHealth;
    [SerializeField]
    private DayAndNightSystem dayNight;
    private int maxSpawnRadius = 25;
    private int minSpawnRadius = 20;


    // Start is called before the first frame update
    void Start()
    {
        findPlayer = FindObjectOfType<PlayerAttack>().gameObject;
    }

    public IEnumerator spawnHordeZombies()
    {
        int dayCount = dayNight.day;
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

    }
}
