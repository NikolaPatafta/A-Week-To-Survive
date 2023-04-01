using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHordeZombies : MonoBehaviour
{
    [SerializeField]
    private GameObject findPlayer;
    [SerializeField]
    private GameObject hordeZombieprefab;
    [SerializeField]
    private int addedHealth;
    [SerializeField]
    public int dayCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator spawnHordeZombies()
    {
        for(int i = 0; i < dayCount; i++) 
        {
            Instantiate(hordeZombieprefab);
            yield return null;
        }

    }

   
}
