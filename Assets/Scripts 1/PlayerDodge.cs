using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerDodge : MonoBehaviour
{
    [Header("Dodge Settings")]
    public float invincibilityTime = 1f;
    public float cooldown = 3f;

    [Header("AOE Settings")]
    public float aoeRadius = 3f;
    public int aoeDamage = 20;

    [Header("References")]
    public Animator animator;
    public GameObject effect;

    private bool canUse = true;
    public bool isInvincible = false;

    public void OnDodge(InputValue value)
    {
        if (value.isPressed && canUse)
        {
            StartCoroutine(DodgeAbility());
        }
    }

    IEnumerator DodgeAbility()
    {
        canUse = false;
        isInvincible = true;

        // 🎬 Animation
        if (animator != null)
            animator.SetTrigger("Dodge");

        // ⚡ Effect
        if (effect != null)
            Instantiate(effect, transform.position, Quaternion.identity);

        // 💥 AOE Damage
        Collider[] hits = Physics.OverlapSphere(transform.position, aoeRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyHealth>()?.TakeDamage(aoeDamage);
            }
        }

        // 🛡️ Invincibility duration
        yield return new WaitForSeconds(invincibilityTime);

        isInvincible = false;

        // ⏳ Cooldown
        yield return new WaitForSeconds(cooldown);

        canUse = true;
    }

    // 🧠 Debug AOE radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, aoeRadius);
    }
}