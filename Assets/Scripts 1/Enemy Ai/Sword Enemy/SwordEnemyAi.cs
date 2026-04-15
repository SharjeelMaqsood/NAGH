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
    [SerializeField] private Transform[] patrolPoints;
    private AnimationContE1 animController;

    [Header("Ranges")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 2f;

    [Header("Memory")]
    [SerializeField] private float memoryDuration = 3f;

    private Vector3 lastKnownPosition;
    private float memoryTimer;

    private NavMeshAgent agent;
    private int patrolIndex = 0;

    [SerializeField] private float attackCooldown = 2f;
    private float lastAttackTime;

    private bool isAttacking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponent<AnimationContE1>();

        currentState = State.Patrol;
        GoToNextPatrolPoint();
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

    void Patrol()
    {
        // Each state owns its own movement update — no global override in Update
        animController.UpdateMovement(agent.velocity.magnitude);

        agent.isStopped = false;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPatrolPoint();

        DetectPlayer();
    }

    void Chase()
    {
        animController.UpdateMovement(agent.velocity.magnitude);

        agent.isStopped = false;

        lastKnownPosition = player.position;
        memoryTimer = memoryDuration;

        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            currentState = State.Attack;
        }
    }

    void Search()
    {
        animController.UpdateMovement(agent.velocity.magnitude);

        agent.isStopped = false;

        agent.SetDestination(lastKnownPosition);

        memoryTimer -= Time.deltaTime;

        if (memoryTimer <= 0f)
            currentState = State.Patrol;
    }

    void Attack()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero; // Kill residual sliding momentum

        animController.UpdateMovement(0); // Locked to 0, nothing can override it now

        LookAt(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            currentState = State.Chase;
            isAttacking = false;
            return;
        }

        if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
        {
            animController.TriggerAttack();
            lastAttackTime = Time.time;
            isAttacking = true;

            // Safety fallback: reset isAttacking even if no Animation Event fires
            Invoke(nameof(EndAttack), attackCooldown * 0.9f);
        }
    }

    void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
            currentState = State.Chase;
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[patrolIndex].position);
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
    }

    void LookAt(Vector3 target)
    {
        Vector3 dir = (target - transform.position);
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);
        }
    }

    // Called by Animation Event at the end of the attack clip
    public void EndAttack()
    {
        CancelInvoke(nameof(EndAttack)); // Cancel the fallback if the real event fired
        isAttacking = false;
    }
}