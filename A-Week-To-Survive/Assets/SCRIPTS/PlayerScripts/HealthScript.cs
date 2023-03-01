using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimatior enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;
    public bool is_Player, is_Boar, is_Cannibal;

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

            //get audio
            enemyAudio = GetComponentInChildren<EnemyAudio>();
            enemy_Stats = GetComponent<EnemyStats>();

        }
        if (is_Player)
        {
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
            return;


        health -= damage;

        if (is_Player)
        {
            //display Health UI
            player_Stats.Display_HealthStats(health);
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
            PlayerDied();

            is_Dead = true;
        }
    }

    void PlayerDied()
    {
        //posto canibal nema animacije, moramo koristiti sljedece komande
        //inace bi koristili isto kao i za Boar
        if (is_Cannibal)
        {

            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-(transform.forward * 20f));

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;

            StartCoroutine(DeadSound());
            Invoke("TurnOffGameObject", 3f);
            
            //zovi Enemy manager i spawnaj zombie
            EnemyManager.instance.EnemyDied(true);


        }

        if (is_Boar)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();

            StartCoroutine(DeadSound());

            //zovi Enemy manager i spawnaj boar
            EnemyManager.instance.EnemyDied(false);
            Invoke("TurnOffGameObject", 3f);
        }

        if (is_Player)
        {
            //turn off enemies ako player umre (deaktiviraj skripte)
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
                Debug.Log("Enemies disabled: " + enemies[i].name);

            }
            EnemyManager.instance.StopSpawning();

            playerDied = true;

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);


            //UiManager ako je player dead
            uiManager.SetActiveHud(false);/*
            if (Cursor.lockState== CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        /*
        if(tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }*/

        }//PlayerDied
    }
        void TurnOffGameObject()
        {
            gameObject.SetActive(false);
        }

        IEnumerator DeadSound()
        {
            yield return new WaitForSeconds(0.3f);
            enemyAudio.Play_DeadSound();
        }

    }

