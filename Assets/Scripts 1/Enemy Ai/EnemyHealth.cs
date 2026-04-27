using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public bool isDead = false;
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
    private Rigidbody rb;
    private NavMeshAgent agent;

    void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(DeathRoutine());
        }
    }

    IEnumerator DeathRoutine()
    {
        if (isDead) yield break;

        isDead = true;

        if (agent != null)
            agent.isStopped = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        if (animator != null)
        {
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Die");
        }

        GetComponent<SwordEnemyAi>().enabled = false;

        yield return new WaitForSeconds(2.2f);

        Destroy(gameObject);
    }
}