using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public int attackDamage = 10;
    public float attackRange = 1.5f;
    public float attackWidth = 1f;
    public float attackCooldown = 1f;

    [Header("References")]
    public Animator animator;
    public Transform attackPoint; // where the sword originates (in front of player)

    private bool canAttack = true;

    void Start()
    {
        if (attackPoint == null)
        {
            attackPoint = transform;
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;

        animator.SetTrigger("Attack");

        // timing when the sword should hit
        yield return new WaitForSeconds(0.2f);

        DealDamage();

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    void DealDamage()
    {
        Vector3 center = attackPoint.position + attackPoint.forward * (attackRange / 2f);

        Vector3 halfExtents = new Vector3(attackWidth / 2f, attackWidth / 2f, attackRange / 2f);

        Collider[] hits = Physics.OverlapBox(center, halfExtents, attackPoint.rotation);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;

        Vector3 center = attackPoint.position + attackPoint.forward * (attackRange / 2f);
        Vector3 size = new Vector3(attackWidth, attackWidth, attackRange);

        Gizmos.matrix = Matrix4x4.TRS(center, attackPoint.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}