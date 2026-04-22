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

    public Controller movement;
    public bool canMove = true;
    private bool canUse = true;
    public bool isInvincible = false;
    private bool isDodging = false; 

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
        isDodging = true;

        animator.SetTrigger("Dodge");

        if (effect != null)
            Instantiate(effect, transform.position, Quaternion.identity);

    
        yield return new WaitForSeconds(invincibilityTime);

        isInvincible = false;
        isDodging = false;

        yield return new WaitForSeconds(cooldown);

        canUse = true;
    }


    void OnAnimatorMove()
    {
        if (!isDodging) return;

        
        transform.position += animator.deltaPosition * 0.15f;
    }
}