using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK,
    ATTACKOBSTACLE
}

public class EnemyController : MonoBehaviour
{ 
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private EnemyDestructible enemyDestructible;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private Transform target;
    public DayAndNightSystem dayAndNightSystem;
    public GameObject attack_Point;
    private AttackScript attackScript;
    private EnemyAudio enemy_Audio;
    private EnemyAnimatior enemy_Anim;
    private NavMeshPath navMeshPath;

    [Header("Obstacles Config:")]
    private ObstacleController currentObstacle;
    public float obstacleAttackDistance = 2f;
    public LayerMask obstacleLayer;
    public LayerMask defaultMask;

    private bool isitDay = false;
    private EnemyState enemy_State;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;

    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1f;
    public float chase_After_Attack_Distance = 2f;
    
    public bool currentState;

    //radijus kretanja od mjesta spawnanja zombija
    [HideInInspector] public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;

    //kolko dugo ce se kretati u jednom smjeru, prije nego dodjelimo drugi smjer
    [HideInInspector] public float patrol_For_This_Time = 15f;
    private float patrol_Timer;
    public float wait_Before_Attack = 2f;
    private float attack_Timer;
    private float attack_Obstacle_Timer;

    


    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimatior>();
        //dohvati playera
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
        enemy_Audio = GetComponentInChildren<EnemyAudio>();
        uIManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        dayAndNightSystem = FindObjectOfType<DayAndNightSystem>();
    }

    void Start()
    {
        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_For_This_Time;

        //kada zombie dolazi do playera - napadni odmah
        attack_Timer = wait_Before_Attack;
        attack_Obstacle_Timer = wait_Before_Attack;

        //zapamti vrijednost od chase_distance kako bi je mogli vratiti
        current_Chase_Distance = chase_Distance;

        navMeshPath = new NavMeshPath();
    }


    void Update()
    {
        if(!uIManager.isPaused) 
        {
            if (enemy_State == EnemyState.PATROL)
            {
                Patrol();
            }
            if (enemy_State == EnemyState.CHASE)
            {
                if (!IsPlayerReachable())
                {
                    AttackObstacleBehavior();
                    enemy_State = EnemyState.ATTACKOBSTACLE;
                }
                else
                {
                    Enemy_State = EnemyState.CHASE;
                    Chase();
                    
                }
                
            }
            if (enemy_State == EnemyState.ATTACK)
            {
                Attack();
            }
            if(enemy_State == EnemyState.ATTACKOBSTACLE)
            {
                AttackObstacleBehavior();
            }
        }
        Debug.Log("Enemy_state: " + enemy_State);
        Debug.Log("Isplayereachable: " + IsPlayerReachable());
    }


    void Patrol()
    {
        ChangeZombieSpeed();
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
        attackScript = attack_Point.GetComponent<AttackScript>();
        attackScript.attackingPlayer = true;
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        attack_Timer += Time.deltaTime;
        if(attack_Timer > wait_Before_Attack)
        {
            enemy_Anim.Attack();
            attack_Timer = 0;
            enemy_Audio.Play_AttackSound();
        }
        //dajemo malu prednost igracu kako bi lakse pobjegao sa ovim + chase_After_Attack_Distance
        //kako neprijatelj nebi odma poceo trcati
        if (Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;   
        }

        

    }//attack

    void AttackObstacle()
    {
        attackScript = attack_Point.GetComponent<AttackScript>();
        attackScript.attackingPlayer = false;
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        attack_Obstacle_Timer += Time.deltaTime;
        enemy_Anim.Run(false);
        if (attack_Obstacle_Timer > wait_Before_Attack)
        {
            
            enemy_Anim.Attack();
            attack_Obstacle_Timer = 0;
            enemy_Audio.Play_AttackSound();
            currentObstacle.DamageObstacle();
            Debug.Log("we are here now");
            Debug.Log("NavAgent status is stopped: " + navAgent.isStopped);
        }
        if(currentObstacle.health <= 0)
        {
            enemy_State = EnemyState.PATROL;
        }
        


    }

    private void AttackObstacleBehavior()
    {
        if(currentObstacle == null)
        {
            Debug.Log("Finding closeset obsticale");
            FindClosestObstacle();
        }

        else if(currentObstacle != null)
        {
            navAgent.SetDestination(currentObstacle.transform.position);
            if(Vector3.Distance(transform.position, currentObstacle.transform.position) < obstacleAttackDistance)
            {
                Debug.Log("hello we are here rn");
                navAgent.isStopped = true;
                AttackObstacle();
            }
        }
        else
        {
            currentObstacle = null;
        }
    }

    private void FindClosestObstacle()
    {
        Collider[] obstacles = Physics.OverlapSphere(transform.position, obstacleAttackDistance, obstacleLayer);
        float closestDistance = Mathf.Infinity;

        foreach(Collider obstacleCollider in obstacles)
        {
            ObstacleController obstacle = obstacleCollider.GetComponent<ObstacleController>();
            if(obstacle != null)
            {
                float distance = Vector3.Distance(transform.position, obstacle.transform.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    currentObstacle = obstacle;
                    Debug.Log("found: " + currentObstacle);
                }
            }
        }
    }

    private bool IsPlayerReachable()
    {
        navAgent.CalculatePath(target.position, navMeshPath);
        if(navMeshPath.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    void ChangeZombieSpeed()
    {
        if (isitDay != dayAndNightSystem.isDay)
        {
            run_Speed /= 2;
            isitDay = true;
        }
        else 
        {
            run_Speed *= 2;
            isitDay = false;
        }

    }




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

    public void CallPlayZombieHurtSound()
    {
        enemy_Audio.PlayZombieHurtSound();
    }

    public EnemyState Enemy_State
    {
        //dozvoljava
        get; set;
    }
}

