using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Footsteps")]
    public AudioClip walkFootstep;
    public AudioClip runFootstep;

    [Header("Actions")]
    public AudioClip jumpSound;
    public AudioClip swordAttackSound;
    public AudioClip dodgeSound;
    public AudioClip hitSound;

    [Header("Settings")]
    public float volume = 1f;

    [Header("Footstep Timing")]
    public float walkStepDelay = 0.5f;
    public float runStepDelay = 0.3f;

    private float lastStepTime;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    // -------------------------
    // FOOTSTEPS WITH CONTROL
    // -------------------------

    public void PlayWalkFootstep()
    {
        if (Time.time - lastStepTime < walkStepDelay) return;

        PlaySound(walkFootstep);
        lastStepTime = Time.time;
    }

    public void PlayRunFootstep()
    {
        if (Time.time - lastStepTime < runStepDelay) return;

        PlaySound(runFootstep);
        lastStepTime = Time.time;
    }

    // -------------------------
    // ACTIONS
    // -------------------------

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void PlaySwordAttackSound()
    {
        PlaySound(swordAttackSound);
    }

    public void PlayDodgeSound()
    {
        PlaySound(dodgeSound);
    }

    public void PlayHitSound()
    {
        PlaySound(hitSound);
    }

    // -------------------------
    // CORE
    // -------------------------

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.PlayOneShot(clip, volume);
    }
}