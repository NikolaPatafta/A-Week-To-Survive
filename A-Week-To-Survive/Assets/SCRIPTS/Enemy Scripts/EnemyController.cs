using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimatior enemy_Anim;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;

    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;

    //kolko dugo ce se kretati u jednom smjeru, prije nego dodjelimo drugi smjer
    public float patrol_For_This_Time = 15f;

    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;

    public GameObject attack_Point;

    private EnemyAudio enemy_Audio;

    private BoxCollider boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        //stanja enemy_Anim su imena animacija u Inspektoru za animacije
        enemy_Anim = GetComponent<EnemyAnimatior>();
        navAgent = GetComponent<NavMeshAgent>();

        //dohvati playera
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

        //dohvati audio skriptu
        enemy_Audio = GetComponentInChildren<EnemyAudio>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_For_This_Time;

        //kada zombie dolazi do playera - napadni odmah
        attack_Timer = wait_Before_Attack;

        //zapamti vrijednost od chase_distance kako bi je mogli vratiti
        current_Chase_Distance = chase_Distance;
    }


    void Update()
    {
       if(enemy_State == EnemyState.PATROL)
       {
            Patrol();
       } 
       if (enemy_State == EnemyState.CHASE)
       {
            Chase();
       }
       if(enemy_State == EnemyState.ATTACK)
       {
            Attack();
       }
       
    }


    void Patrol()
    {
        navAgent.isStopped= false;
        navAgent.speed = walk_Speed;

        patrol_Timer += Time.deltaTime;

        if(patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();

            patrol_Timer = 0f;

        }
        //ako se enemy krece
        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Walk(true);
        }
        else
        {
            enemy_Anim.Walk(false);
        }

        //testiraj razmak izmedju playera i neprijatelja za chase distance
        if(Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {
            enemy_Anim.Walk(false);

            enemy_State = EnemyState.CHASE;

            //play audio kad nas neprijatelj vidi

            enemy_Audio.Play_ScreamSound();

        }
    }//Patrol



    void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = run_Speed; 

        //postavi poziciju playera kao destinaciju jer lovimo playera
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Run(true);
        }
        else
        {
            enemy_Anim.Run(false);
        }
        //testiraj razmak izmedju playera i neprijatelja za attack razmak
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            //zaustavi prijasnje animacije
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            //ako je igrac van attack ili chase razmaka i upuca neprijatelja, onda ga neprijatelj vidi
            if(chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }
        //igrac izadje van vidika od neprijatelja
        else if (Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            enemy_Anim.Run(false);
            enemy_State = EnemyState.PATROL;

            //resetiraj patrol timer da funkcija moze kalkulirati novu patrol destinaciju
            patrol_Timer = patrol_For_This_Time;

            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }

        }

    }//chase

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;
        if(attack_Timer > wait_Before_Attack)
        {
            enemy_Anim.Attack();
            attack_Timer = 0;

            //psusti attack zvuk
            enemy_Audio.Play_AttackSound();
        }

        //dajemo malu prednost igracu kako bi lakse pobjegao sa ovim + chase_After_Attack_Distance kako neprijatelj nebi odma poceo trcati
        if (Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
            
        }

    }//attack



    //Random radijus za patrol state od zombija
    void SetNewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        //Ako je random position van navigacijskog podrucja (out of world) - kalkulira novu poziciju
        // -1 na kraju ukljucuje sve layere (first person, UI, itd...) 
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);
        navAgent.SetDestination(navHit.position);



    }//SetNewRandomDestination

    void Turn_On_AttackPoint()
    {
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(false);
        }
    }

    public EnemyState Enemy_State
    {
        //dozvoljava
        get; set;
    }
}

