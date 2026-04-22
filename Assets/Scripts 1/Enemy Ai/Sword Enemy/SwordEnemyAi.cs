using UnityEngine;
using UnityEngine.AI;

public class SwordEnemyAi : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase,
        Search,
        Attack
    }


    [Header("State")]
    [SerializeField] private State currentState;

    [Header("References")]
    [SerializeField] private Transform player;
    private AnimationContE1 animController;

    [Header("Ranges")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 2f;

    [Header("Roaming")]
    [SerializeField] private float roamRadius = 8f;
    [SerializeField] private float roamDelay = 2f;
    private Vector3 homePosition;


    [Header("Memory")]
    [SerializeField] private float memoryDuration = 3f;

   
    private Vector3 lastKnownPosition;
    private float memoryTimer;
    private float roamTimer;

    private NavMeshAgent agent;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 2f;
    private float lastAttackTime;
    private bool isAttacking;

    void Start()
    {
        homePosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponent<AnimationContE1>();

        currentState = State.Patrol;
        PickRandomRoamPoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase: Chase(); break;
            case State.Search: Search(); break;
            case State.Attack: Attack(); break;
        }
    }

    // ---------------- ROAM (NO PATROL POINTS) ----------------
    void Patrol()
    {
        agent.isStopped = false;

        animController.UpdateMovement(agent.velocity.magnitude);

        roamTimer -= Time.deltaTime;

        if (roamTimer <= 0f && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            PickRandomRoamPoint();
        }

        DetectPlayer();
    }

    void PickRandomRoamPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += homePosition;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }

        roamTimer = roamDelay;
    }

    // ---------------- CHASE ----------------
    void Chase()
    {
        agent.isStopped = false;

        animController.UpdateMovement(agent.velocity.magnitude);

        if (player == null) return;

        lastKnownPosition = player.position;
        memoryTimer = memoryDuration;

        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            currentState = State.Attack;
        }
    }

    // ---------------- SEARCH ----------------
    void Search()
    {
        agent.isStopped = false;

        animController.UpdateMovement(agent.velocity.magnitude);

        agent.SetDestination(lastKnownPosition);

        memoryTimer -= Time.deltaTime;

        if (memoryTimer <= 0f)
        {
            currentState = State.Patrol;
            PickRandomRoamPoint();
        }

        DetectPlayer();
    }

    // ---------------- ATTACK ----------------
    void Attack()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        animController.UpdateMovement(0);

        if (player == null) return;

        LookAt(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange + 0.5f)
        {
            currentState = State.Chase;
            isAttacking = false;
            return;
        }

        if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
        {
            animController.TriggerAttack();
            isAttacking = true;
            lastAttackTime = Time.time;
        }
    }

    // ---------------- DETECT ----------------
    void DetectPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
            currentState = State.Chase;
    }

    // ---------------- LOOK ----------------
    void LookAt(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;

        if (dir == Vector3.zero) return;

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 6f);
    }

    // ---------------- EVENTS ----------------
    public void EndAttack()
    {
        isAttacking = false;
    }

    public void DealDamage()
    {
        float radius = 2f;

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<Health>()?.ChangeHealth(10);
            }
        }
    }
}