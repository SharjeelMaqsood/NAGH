using UnityEngine;

public class AnimationContE1 : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false; 
    }
    public void UpdateMovement(float speed)
    {
        // Normalize speed relative to your agent's max speed (e.g. 3.5f)
        float normalizedSpeed = speed / 3.5f;
        animator.SetFloat("Speed", normalizedSpeed, 0.1f, Time.deltaTime);
    }

    public void TriggerAttack()
    {
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");
    }

    public void TriggerHit()
    {
        animator.ResetTrigger("Hit");
        animator.SetTrigger("Hit");
    }

    public void TriggerDie()
    {
        animator.ResetTrigger("Die");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hit");
        animator.SetBool("IsDead", true); // Add this
        animator.SetTrigger("Die");
    }
}