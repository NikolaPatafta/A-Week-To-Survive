using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseDistance = 40f;
    public float attackDistance = 1.5f;

    private NavMeshAgent agent;
    private Transform player;
    //private ObstacleController currentObstacle;
    private ObstacleController currentDoor;

    public LayerMask doorLayer;

    public enum EnemyStates { PATROL, CHASE, ATTACK, ATTACKOBSTACLE }
    private EnemyStates currentState = EnemyStates.PATROL;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetPatrolDestination();
        Debug.Log("Found player: " + player);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyStates.PATROL:
                if (distanceToPlayer <= chaseDistance)
                    currentState = EnemyStates.CHASE;
                PatrolBehavior();
                break;

            case EnemyStates.CHASE:
                if (distanceToPlayer <= attackDistance)
                    currentState = EnemyStates.ATTACK;
                ChaseBehavior();
                break;

            case EnemyStates.ATTACK:
                if (distanceToPlayer > attackDistance)
                    currentState = EnemyStates.CHASE;
                AttackBehavior();
                break;

            case EnemyStates.ATTACKOBSTACLE:
                AttackObstacleBehavior();
                break;
        }
        Debug.Log("currentstate" + currentState);
    }

    private void PatrolBehavior()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetPatrolDestination();
        }
    }

    private void ChaseBehavior()
    {
        agent.SetDestination(player.position);
    }

    private void AttackBehavior()
    {
        agent.SetDestination(player.position);
        
        // Implement your attacking logic here
    }

    private void AttackObstacleBehavior()
    {
        if (currentDoor == null)
        {
            FindNearestReachableDoor();
        }

        if (currentDoor != null)
        {
            agent.SetDestination(currentDoor.transform.position);

            if (Vector3.Distance(transform.position, currentDoor.transform.position) <= attackDistance)
            {
                currentDoor.DamageObstacle();

                currentDoor = null;
                currentState = EnemyStates.CHASE;
            }
        }
    }

    private void SetPatrolDestination()
    {
        //agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        //currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

        float rand_Radius = Random.Range(10, 20);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;
        NavMeshHit navHit;

        //Ako je random position van navigacijskog podrucja (out of world) - kalkulira novu poziciju
        // -1 na kraju ukljucuje sve layere (first person, UI, itd...) 
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);
        agent.SetDestination(navHit.position);
    }

    private void FindNearestReachableDoor()
    {
        Collider[] doors = Physics.OverlapSphere(transform.position, attackDistance, doorLayer);
        float closestDistance = Mathf.Infinity;

        foreach (Collider doorCollider in doors)
        {
            ObstacleController door = doorCollider.GetComponent<ObstacleController>();
            if (door != null)
            {
                float distance = Vector3.Distance(transform.position, door.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    currentDoor = door;
                }
            }
        }
    }
}
