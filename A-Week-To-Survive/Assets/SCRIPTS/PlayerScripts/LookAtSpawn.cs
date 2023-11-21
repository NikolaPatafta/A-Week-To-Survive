using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtSpawn : MonoBehaviour
{
    public GameObject zombiePrefab;
    public AudioSource audioSource;

    private Vector3 lastLookAtPoint = Vector3.zero;
    private float lookAtTimer = 0f;
    private float spawnTimer = 0f;
    private const float requiredLookTime = 10f;
    private const float spawnCooldown = 300f; 
    private const float maxSpawnDistance = 30f; 

    void Update()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (Vector3.Distance(Camera.main.transform.position, hit.point) <= maxSpawnDistance)
                {
                    if (lastLookAtPoint != Vector3.zero && Vector3.Distance(hit.point, lastLookAtPoint) < 0.1f)
                    {
                        lookAtTimer += Time.deltaTime;
                        if (lookAtTimer >= requiredLookTime)
                        {
                            StartCoroutine(SpawnZombie(hit.point));
                            lookAtTimer = 0f;
                            spawnTimer = spawnCooldown;
                        }
                    }
                    else
                    {
                        lookAtTimer = 0f;
                    }

                    lastLookAtPoint = hit.point;
                }
                else
                {
                    lookAtTimer = 0f;
                    lastLookAtPoint = Vector3.zero;
                }
            }
        }

        IEnumerator SpawnZombie(Vector3 spawnLocation)
        {
            audioSource.Play();
            yield return new WaitForSeconds(0.5f);
            Instantiate(zombiePrefab, spawnLocation + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}