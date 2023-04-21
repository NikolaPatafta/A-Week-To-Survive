using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimatior enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;
    private Rigidbody rigidbodyrb;
    private PlayPlayerSound playPlayerSound;

    public float health = 100f;
    public bool is_Player, is_Boar, is_Cannibal;
    public CameraShake cameraShake;

    private bool is_Dead;
    private bool playerDied;

    private EnemyAudio enemyAudio;

    private PlayerStats player_Stats;
    private EnemyStats enemy_Stats;

    //kontrole za deathscreen
    private UIManager uiManager;

    void Awake()
    {
        is_Dead = false;
        uiManager = GetComponent<UIManager>();

        if (is_Boar || is_Cannibal)
        {
            enemy_Anim = GetComponent<EnemyAnimatior>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
            rigidbodyrb = GetComponent<Rigidbody>();


            //get audio
            enemyAudio = GetComponentInChildren<EnemyAudio>();
            enemy_Stats = GetComponent<EnemyStats>();

        }
        if (is_Player)
        {
            playPlayerSound = GetComponent<PlayPlayerSound>();  
            player_Stats = GetComponent<PlayerStats>();
        }

    }

    public bool IsDead()
    {
        return playerDied;
    }

    public void ApplyDamage(float damage)
    {

        if (is_Dead)
        {
            return;
        }
        else
        {
            health -= damage;
        }
           
        if (is_Player)
        {
            //display Health UI
            playPlayerSound.PlayPlayerHurtSound();
            player_Stats.Display_HealthStats(health);
            StartCoroutine(cameraShake.Shake());
        }

        if (is_Boar || is_Cannibal)
        {
            enemy_Stats.Display_EnemyHealth(health);
            //ako pogodimo enemy onda postavljamo chase distance na vecu distancu kako
            //bi nas mogli pronaci
            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }

        }
        if (health <= 0f)
        {
            CheckWhoDied();

            is_Dead = true;
        }
    }

    void CheckWhoDied()
    {

        if (!is_Player)
        {
            Debug.Log("animation Triggered:" + enemy_Anim);
            enemy_Anim.Dead();

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            //enemy_Anim.enabled = false;

            StartCoroutine(DeadSound());

            //zovi Enemy manager i spawnaj zombie
            if (is_Cannibal)
            {
                EnemyManager.instance.EnemyDied(true);
            }
            else
            {
                EnemyManager.instance.EnemyDied(false);
            }
            
            Invoke("TurnOffGameObject", 5f);
        }


        if (is_Player)
        {
            //turn off enemies ako player umre (deaktiviraj skripte)
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemies.Length; i++)
            {
                Debug.Log("Enemies disabled: " + enemies[i].name);
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
            EnemyManager.instance.StopSpawning();

            //play player died sound
            playPlayerSound.PlayPlayerDeathSound();

            playerDied = true;

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponShooting>().enabled = false;

            //UiManager ako je player dead
            uiManager.SetActiveHud(false);

        }

    }
        void TurnOffGameObject()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        IEnumerator DeadSound()
        {
            yield return new WaitForSeconds(0.3f);
            enemyAudio.Play_DeadSound();
        }

    }

