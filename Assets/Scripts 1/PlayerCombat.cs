using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    //public int attackDamage = 10;
    public float attackRange = 1.5f;
    public float attackWidth = 1f;
    public float attackCooldown = 1f;
    public Weapon currentWeapon;
    public int baseDamage = 10;

    [Header("References")]
    public Animator animator;
    public Transform attackPoint;

    private bool canAttack = true;

    void Start()
    {
        if (attackPoint == null)
            attackPoint = transform;
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

       
        float hitTime = 0.25f;    
        float totalTime = attackCooldown;

        yield return new WaitForSeconds(hitTime);

        DealDamage();

        yield return new WaitForSeconds(totalTime - hitTime);

        canAttack = true;
    }

    void DealDamage()
    {
        Vector3 center = attackPoint.position + attackPoint.forward * (attackRange / 2f);

        Vector3 halfExtents = new Vector3(
            attackWidth / 2f,
            attackWidth / 2f,
            attackRange / 2f
        );

        Collider[] hits = Physics.OverlapBox(center, halfExtents, attackPoint.rotation);

        int finalDamage = baseDamage;

        if(currentWeapon !=null)
        {
            finalDamage =Mathf.RoundToInt(baseDamage*currentWeapon.damageMultiplier);

        }

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyHealth>()?.TakeDamage(baseDamage);
            }
        }
    }
}