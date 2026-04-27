using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 1.5f;
    public float attackWidth = 1f;
    public float attackCooldown = 1f;
    public Weapon currentWeapon;
    public int baseDamage = 10;
    public int heavyDamage = 25;

    [Header("References")]
    public Animator animator;
    public Transform attackPoint;
    private CharacterSoundController soundController;

    private bool canAttack = true;

    void Start()
    {
        soundController = GetComponent<CharacterSoundController>();
        if (attackPoint == null)
            attackPoint = transform;
    }

    public void OnAttack(InputValue value)
    {
        if (!value.isPressed) return;

        if (canAttack)
        {
            StartCoroutine(LightAttack());
        }
    }

    public void OnHeavyAttack(InputValue value)
    {
        if (!value.isPressed) return;

        if (canAttack)
        {
            StartCoroutine(HeavyAttack());
        }
    }

    IEnumerator LightAttack()
    {
        canAttack = false;

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    IEnumerator HeavyAttack()
    {
        canAttack = false;

        animator.SetTrigger("HeavyAttack");

        yield return new WaitForSeconds(attackCooldown * 1.5f);

        canAttack = true;
    }

    bool DealDamage(int damage)
    {
        bool hitSomething = false;

        Vector3 center = attackPoint.position + attackPoint.forward * (attackRange / 2f);

        Vector3 halfExtents = new Vector3(
            attackWidth / 2f,
            attackWidth / 2f,
            attackRange / 2f
        );

        Collider[] hits = Physics.OverlapBox(center, halfExtents, attackPoint.rotation);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyHealth>()?.TakeDamage(damage);
                hitSomething = true;
            }
        }

        return hitSomething;
    }

    public void AnimLightHit()
    {
        int dmg = baseDamage;

        if (currentWeapon != null)
            dmg = Mathf.RoundToInt(baseDamage * currentWeapon.damageMultiplier);

        bool hit = DealDamage(dmg);

        if (hit)
            soundController?.PlayHitConfirmSound();
        else
            soundController?.PlaySwingSound();
    }

    public void AnimHeavyHit()
    {
        int dmg = heavyDamage;

        if (currentWeapon != null)
            dmg = Mathf.RoundToInt(heavyDamage * currentWeapon.damageMultiplier);

        bool hit = DealDamage(dmg);

        if (hit)
            soundController?.PlayHitConfirmSound();
        else
            soundController?.PlaySwingSound();
    }

    public void AnimSwingSound()
    {
        soundController?.PlaySwingSound();
    }
}