using UnityEngine;
using UnityEngine.AI;

public class AnimationContE1 : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        animator.applyRootMotion = false;
    }

    public void UpdateMovement(float speed)
    {
        if (IsDead()) return;

        float maxSpeed = agent != null ? agent.speed : 3.5f;
        float normalizedSpeed = speed / maxSpeed;

        animator.SetFloat("Speed", normalizedSpeed, 0.1f, Time.deltaTime);
    }

    public void TriggerAttack()
    {
        if (IsDead()) return;

        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");
    }

    public void TriggerHit()
    {
        if (IsDead()) return;

        animator.ResetTrigger("Hit");
        animator.SetTrigger("Hit");
    }

    public void TriggerDie()
    {
        animator.ResetTrigger("Die");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hit");

        animator.SetBool("IsDead", true);
        animator.SetTrigger("Die");
    }

    
    public bool IsAttacking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }

    public bool IsDead()
    {
        return animator.GetBool("IsDead");
    }
}