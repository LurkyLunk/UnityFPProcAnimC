using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 10; // Amount of ammo to give when picked up
    public AudioClip pickupSound; // Sound to play when the ammo pickup is picked up
    public float volume = 10f; // Volume of the pickup sound

    public int Pickup()
    {
        // Perform any desired visual/audio effects when the ammo is picked up
        // For example, you can play a sound or show a particle effect

        // Play the pickup sound
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
        }

        // Disable the ammo pickup object
        gameObject.SetActive(false);

        // Return the ammo amount to the caller
        return ammoAmount;
    }
}
