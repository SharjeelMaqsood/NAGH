using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Footsteps")]
    public AudioClip walkFootstep;
    public AudioClip runFootstep;
    public float footstepVolume = 0.6f;

    [Header("Actions")]
    public AudioClip jumpSound;
    public AudioClip swingSound;
    public AudioClip hitConfirmSound;
    public AudioClip dodgeSound;
    public float actionVolume = 1f;

    [Header("Master Volume")]
    [Range(0f, 1f)]
    public float masterVolume = 1f;

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
    // FOOTSTEPS
    // -------------------------

    public void PlayWalkFootstep()
    {
        if (Time.time - lastStepTime < walkStepDelay) return;

        PlaySound(walkFootstep, footstepVolume);
        lastStepTime = Time.time;
    }

    public void PlayRunFootstep()
    {
        if (Time.time - lastStepTime < runStepDelay) return;

        PlaySound(runFootstep, footstepVolume);
        lastStepTime = Time.time;
    }

    // -------------------------
    // ACTIONS
    // -------------------------

    public void PlayJumpSound() => PlaySound(jumpSound, actionVolume);
    public void PlaySwingSound() => PlaySound(swingSound, actionVolume);
    public void PlayHitConfirmSound() => PlaySound(hitConfirmSound, actionVolume);
    public void PlayDodgeSound() => PlaySound(dodgeSound, actionVolume);

    // -------------------------
    // CORE
    // -------------------------

    private void PlaySound(AudioClip clip, float categoryVolume)
    {
        if (clip == null) return;

        float finalVolume = categoryVolume * masterVolume;
        audioSource.PlayOneShot(clip, finalVolume);
    }
}