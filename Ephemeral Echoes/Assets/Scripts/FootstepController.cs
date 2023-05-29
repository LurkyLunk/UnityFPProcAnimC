using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array of footstep audio clips
    public AudioClip jumpSound; // Jump sound clip
    public AudioClip landSound; // Land sound clip
    public AudioSource audioSource; // Reference to the audio source component
    public float footstepDelay = 0.5f; // Delay between footstep sounds
    public float footstepVolume = 1f; // Volume of footstep sounds

    private bool isMoving = false; // Flag to track character movement state
    private bool isJumping = false; // Flag to track jump state
    private float footstepTimer = 0f; // Timer for footstep delay

    private void Update()
    {
        // Get reference to the FPController
        FPController fpController = GetComponent<FPController>();

        // Check if the character is moving
        isMoving = fpController.IsCharacterMoving();

        // Check if the character is jumping
        isJumping = fpController.IsCharacterJumping();

        // Play footstep sounds randomly when the character is moving and not jumping
        if (isMoving && !isJumping)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                // Select a random footstep sound from the array
                AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];

                // Play the footstep sound
                audioSource.PlayOneShot(footstepSound, footstepVolume);

                // Reset the footstep timer
                footstepTimer = footstepDelay;
            }
        }
    }

    // Call this method when the character jumps
    public void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            // Play the jump sound
            audioSource.PlayOneShot(jumpSound);
        }
    }

    // Call this method when the character lands
    public void PlayLandSound()
    {
        if (landSound != null)
        {
            // Play the land sound
            audioSource.PlayOneShot(landSound);
        }
    }

    // Call this method to set the jump state
    public void SetJumpState(bool isJumping)
    {
        this.isJumping = isJumping;
    }
}
