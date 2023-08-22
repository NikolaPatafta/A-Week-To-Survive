using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimatior enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;
    private PlayPlayerSound playPlayerSound;
    private Transform player;

    public float health = 100f;
    public bool is_Player, is_Boar, is_Zombie;
    public CameraShake cameraShake;

    private bool is_Dead;
    private bool playerDied;

    private EnemyAudio enemyAudio;

    private PlayerStats player_Stats;
    private EnemyStats enemy_Stats;

    //kontrole za deathscreen
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BloodScreenEffect bloodScreenEffect;

    void Awake()
    {
        is_Dead = false;

        if (is_Boar || is_Zombie)
        {
            enemy_Anim = GetComponent<EnemyAnimatior>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
            enemyAudio = GetComponentInChildren<EnemyAudio>();
            enemy_Stats = GetComponent<EnemyStats>();
            uiManager = FindAnyObjectByType<UIManager>();
            player = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
            bloodScreenEffect = player.transform.GetComponent<BloodScreenEffect>();

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

    public void ApplyDamage(float damage, Transform target)
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
            bloodScreenEffect.ChangeAlpha();
        }
        if (is_Boar || is_Zombie)
        {
            enemy_Stats.Display_EnemyHealth(health);
            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }
        }
        if (health <= 0f)
        {
            uiManager.IncreaseScore();
            CheckWhoDied(target);
            is_Dead = true;
        }
    }

    public void AddHealth(int healthvalue)
    {
        health += healthvalue;
        if(health >= 100)
        {
            health = 100;
        }
        player_Stats.Display_HealthStats(health);
        bloodScreenEffect.ChangeAlpha();
    }

    void CheckWhoDied(Transform target)
    {
        if (!is_Player)
        {
            EnemyManager.instance.LowerEnemyCounter();
            target.transform.GetComponent<BoxCollider>().enabled = false;   
            enemy_Anim.Dead();
            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            StartCoroutine(DeadSound());    
            Invoke("TurnOffGameObject", 5f);
            
        }

        if (is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
            EnemyManager.instance.StopSpawning();
            playPlayerSound.PlayPlayerDeathSound();
            playerDied = true;

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponShooting>().enabled = false;

            //UiManager ako je player dead
            uiManager.isPaused = true;   
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

