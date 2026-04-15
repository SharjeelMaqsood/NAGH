using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
    private Rigidbody rb;

    [Header("Knockback")]
    public float knockbackForce = 3f;
    public Transform player; // assign player here

    void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log(gameObject.name + " took damage: " + damage);

        // Play hit animation
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        // Knockback using player position
        if (rb != null && player != null)
        {
            Vector3 knockDir = (transform.position - player.position).normalized;
            rb.AddForce(knockDir * knockbackForce, ForceMode.Impulse);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died");

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        Destroy(gameObject, 2.6f);
    }
}