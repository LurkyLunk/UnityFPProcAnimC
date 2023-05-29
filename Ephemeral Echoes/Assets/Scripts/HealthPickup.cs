using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 25; // Amount of health to give when picked up
    public AudioClip pickupSound; // Sound to play when the health pickup is picked up

    public int Pickup()
    {
        // Perform any desired visual/audio effects when the health is picked up
        // For example, you can play a sound or show a particle effect

        // Play the pickup sound
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        // Disable the health pickup object
        gameObject.SetActive(false);

        // Return the health amount to the caller
        return healthAmount;
    }
}
